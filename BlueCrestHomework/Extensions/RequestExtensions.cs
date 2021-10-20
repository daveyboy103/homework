using System.Collections.Generic;
using System.Linq;
using BlueCrestHomework.Models;
using DataModel.Dtos;

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
                var rowMeasures = GetRowMeasures(request, dimension);
                bool pnlAdded = false;
                var pnlSubComponents = new Dictionary<string, double>();
                foreach (RowMeasure measure in rowMeasures)
                {
                    double pnl = 0;
                    if (measure.Keys.Contains("pnl"))
                    {
                        pnl = measure.Measures.First();
                        totalPnl += pnl;
                        (ret.Rows as List<BindingDataRow>)?.Add(new BindingDataRow(colDataForDimension, pnlSubComponents: pnlSubComponents)
                        {
                            Pnl = pnl
                        });
                        pnlAdded = true;
                    }
                    
                    if (measure.Keys.First().StartsWith("pnl."))
                    {
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

        private static IEnumerable<RowMeasure> GetRowMeasures(Request request, string dimension)
        {
            var rowMeasures = request.Measures.Rows.Where(x => x.DimensionId == dimension);

            double cumulativePnl = 0;
            foreach (RowMeasure measure in rowMeasures)
            {
                if (measure.Keys.Contains("pnl"))
                {
                    cumulativePnl += measure.Measures.First();
                }
            }

            return rowMeasures;
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
                x.Equals("fundreference") ||
                x.Equals("desk") ||
                x.Equals("strat")).ToList();
            cols.Add("pnl");
            
            for (int i = 0; i < request.Dimensions.Columns.Count(); i++)
            {
                if (request.Dimensions.Columns[i] == "fundreference") columnKeys.Add("fundreference", i);
                if (request.Dimensions.Columns[i] == "desk") columnKeys.Add("desk", i);
                if (request.Dimensions.Columns[i] == "strat") columnKeys.Add("strat", i);
            }
            
            return columnKeys;
        }
    }
}