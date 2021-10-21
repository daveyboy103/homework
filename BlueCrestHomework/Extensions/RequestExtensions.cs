using System.Collections.Generic;
using System.Linq;
using BlueCrestHomework.Models;
using DataModel.Dtos;

// ReSharper disable StringLiteralTypo

namespace BlueCrestHomework.Extensions
{
    public static class RequestExtensions
    {
        public static RequestBinding ToBindingObject(this IEnumerable<RowMeasureItem> list, string requestId)
        {
            void AddPnlSubComponent(RowMeasureItem rowMeasureItem, Dictionary<string, double> dictionary)
            {
                if (rowMeasureItem.Key.StartsWith(Constants.PnlMatcher))
                {
                    if (ContainsTwoDots(rowMeasureItem.Key))
                        dictionary.Add(rowMeasureItem.Key, rowMeasureItem.Value);
                }
            }

            var rowMeasureItems = list as RowMeasureItem[] ?? list.ToArray();
            List<string> allDimensions = new(rowMeasureItems.Select(x => x.DimensionId).Distinct());
            List<BindingDataRow> dataRows = new();
            double totalPnl = 0;
            string currentDimension = null;
            var pnlSubComponents = new Dictionary<string, double>();

            foreach (RowMeasureItem rowMeasureItem in rowMeasureItems)
            {
                currentDimension ??= rowMeasureItem.DimensionId;
                
                if(currentDimension != rowMeasureItem.DimensionId)
                    pnlSubComponents = new Dictionary<string, double>();
                
                if (rowMeasureItem.Key == Constants.Pnl)
                {
                    totalPnl += rowMeasureItem.Value;
                    var bindingDataRow = new BindingDataRow
                    {
                        DimensionId = rowMeasureItem.DimensionId,
                        Fund = rowMeasureItem.Fund,
                        Strategy = rowMeasureItem.Strategy,
                        Desk = rowMeasureItem.Desk,
                        Pnl = rowMeasureItem.Value,
                        PnlReported = true,
                        PnlSubComponents = pnlSubComponents
                    };
                    
                    dataRows.Add(bindingDataRow);
                }
                
                AddPnlSubComponent(rowMeasureItem, pnlSubComponents);
                
                currentDimension = rowMeasureItem.DimensionId;
            }

            return new RequestBinding(dataRows, totalPnl){ RequestId = requestId};
        }
        public static IEnumerable<RowMeasureItem> ToEnumerableOfMeasures(this Request request)
        {
            return request.Dimensions.Rows.Join(request.Measures.Rows, 
                row => row.DimensionId,
                rowMeasure => rowMeasure.DimensionId,
                (row, rowMeasure) => new RowMeasureItem
                {
                    DimensionId = rowMeasure.DimensionId,
                    Key = rowMeasure.Keys.First(),
                    Value = rowMeasure.Measures.First(),
                    Fund = request.Dimensions.Rows.Where
                            ((row1, i) => row1.DimensionId == rowMeasure.DimensionId).First()
                        .Dimensions[request.Dimensions.KeyIndices[Constants.Fund]],
                    Strategy = request.Dimensions.Rows.Where
                            ((row1, i) => row1.DimensionId == rowMeasure.DimensionId).First()
                        .Dimensions[request.Dimensions.KeyIndices[Constants.Strategy]],
                    Desk = request.Dimensions.Rows.Where
                            ((row1, i) => row1.DimensionId == rowMeasure.DimensionId).First()
                        .Dimensions[request.Dimensions.KeyIndices[Constants.Desk]]
                });
        }
        

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
                    }
                    
                    if (measure.Keys.First().StartsWith(Constants.PnlMatcher))
                    {
                        if(ContainsTwoDots(measure.Keys.First()))
                            pnlSubComponents.Add(measure.Keys.First(), measure.Measures.First());
                    }
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