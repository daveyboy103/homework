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
            void AddPnlSubComponent(RowMeasureItem rowMeasureItem, IDictionary<string, double> dictionary)
            {
                if (!rowMeasureItem.Key.StartsWith(Constants.PnlMatcher)) return;
                if (ContainsTwoDots(rowMeasureItem.Key))
                    dictionary.Add(rowMeasureItem.Key, rowMeasureItem.Value);
            }

            var rowMeasureItems = list as RowMeasureItem[] ?? list.ToArray();
            List<BindingDataRow> dataRows = new();
            double totalPnl = 0;
            string currentDimension = null;
            var pnlSubComponents = new Dictionary<string, double>();

            foreach (var rowMeasureItem in rowMeasureItems)
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
                        PnlSubComponents = pnlSubComponents
                    };
                    
                    dataRows.Add(bindingDataRow);
                }
                
                AddPnlSubComponent(rowMeasureItem, pnlSubComponents);
                
                currentDimension = rowMeasureItem.DimensionId;
            }

            return new RequestBinding(dataRows, totalPnl){ RequestId = requestId };
        }
        public static IEnumerable<RowMeasureItem> ToEnumerableOfMeasures(this Request request)
        {
            return request.Dimensions.Rows.Join(request.Measures.Rows, 
                row => row.DimensionId,
                rowMeasure => rowMeasure.DimensionId,
                (_, rowMeasure) => new RowMeasureItem
                {
                    DimensionId = rowMeasure.DimensionId,
                    Key = rowMeasure.Keys.First(),
                    Value = rowMeasure.Measures.First(),
                    Fund = request.Dimensions.Rows.Where
                            ((row1, _) => row1.DimensionId == rowMeasure.DimensionId).First()
                        .Dimensions[request.Dimensions.KeyIndices[Constants.Fund]],
                    Strategy = request.Dimensions.Rows.Where
                            ((row1, _) => row1.DimensionId == rowMeasure.DimensionId).First()
                        .Dimensions[request.Dimensions.KeyIndices[Constants.Strategy]],
                    Desk = request.Dimensions.Rows.Where
                            ((row1, _) => row1.DimensionId == rowMeasure.DimensionId).First()
                        .Dimensions[request.Dimensions.KeyIndices[Constants.Desk]]
                });
        }

        private static bool ContainsTwoDots(string stringToTest)
        {
            var dotCount = stringToTest.Count(c => c == '.');
            return dotCount <= 2;
        }
    }
}