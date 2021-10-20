using System;
using System.Collections.Generic;
using System.Linq;
using BlueCrestHomework.Models;
using DataModel.Dtos;
// ReSharper disable StringLiteralTypo

namespace BlueCrestHomework.Extensions
{
    public static class RequestExtensions
    {
        public static RequestBinding ToBindingObject(this Request request)
        {
            var ret = new RequestBinding{ RequestId = request.RequestId };
            
            List<string> allDimensions = new List<string>(request.Dimensions.Rows.Select(x => x.DimensionId));
            double totalPnl = 0;

            foreach (string dimension in allDimensions)
            {
                var columnKeys = GetColumnKeys(request);
                var colDataForDimension = GetColumnDataForDimension(request, dimension, columnKeys);
                var rowMeasures = GetRowMeasuresForDimension(request, dimension);
                bool pnlAdded = false;
                var pnlSubComponents = new Dictionary<string, double>();
                foreach (RowMeasure measure in rowMeasures)
                {
                    if (measure.Keys.Contains(Constants.Pnl))
                    {
                        var pnl = measure.Measures.First();
                        totalPnl += pnl;
                        (ret.Rows as List<BindingDataRow>)?.Add(new BindingDataRow(colDataForDimension, pnlSubComponents: pnlSubComponents)
                        {
                            Pnl = pnl
                        });
                        pnlAdded = true;
                    }
                    
                    if (measure.Keys.First().StartsWith(Constants.PnlMatcher))
                    {
                        if(ContainsTwoDots(measure.Keys.First()))
                            pnlSubComponents.Add(measure.Keys.First(), measure.Measures.First());
                    }
                }

                if (!pnlAdded)
                {
                    (ret.Rows as List<BindingDataRow>)?.Add(new BindingDataRow(colDataForDimension, false)
                    {
                        Pnl = 0,
                    });
                }
            }

            ret.TotalPnl = totalPnl;

            return ret;
        }

        private static bool ContainsTwoDots(string stringToTest)
        {
            int dotCount = 0;
            foreach (char c in stringToTest)
            {
                if (c == '.')
                    dotCount++;
            }

            return dotCount <= 2;
        }

        private static IEnumerable<RowMeasure> GetRowMeasuresForDimension(Request request, string dimension)
        {
            return request.Measures.Rows.Where(x => x.DimensionId == dimension);
        }

        private static Dictionary<string, string> GetColumnDataForDimension(Request request, string dimension, Dictionary<string, int> columnKeys)
        {
            var rows = request.Dimensions.Rows.Where(x => x.DimensionId == dimension);

            Dictionary<string, string> colDataForDimension = new();

            foreach (Row row in rows)
            {
                foreach (KeyValuePair<string, int> keyValuePair in columnKeys)
                {
                    colDataForDimension.Add(keyValuePair.Key, row.Dimensions[keyValuePair.Value]);
                }
            }

            return colDataForDimension;
        }

        private static Dictionary<string, int> GetColumnKeys(Request request)
        {
            Dictionary<string, int> columnKeys = new Dictionary<string, int>();

            List<string> cols = request.Dimensions.Columns.Where(x =>
                x.Equals(Constants.Fund) ||
                x.Equals(Constants.Desk) ||
                x.Equals(Constants.Strategy)).ToList();
            cols.Add(Constants.Pnl);
            
            for (int i = 0; i < request.Dimensions.Columns.Count(); i++)
            {
                if (request.Dimensions.Columns[i] == Constants.Fund) columnKeys.Add(Constants.Fund, i);
                if (request.Dimensions.Columns[i] == Constants.Desk) columnKeys.Add(Constants.Desk, i);
                if (request.Dimensions.Columns[i] == Constants.Strategy) columnKeys.Add(Constants.Strategy, i);
            }
            
            return columnKeys;
        }
    }
}