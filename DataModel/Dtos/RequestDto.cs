using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataModel.Dtos
{
    public class Row
    {
        public string DimensionId { get; set; }
        public IList<string> Dimensions { get; set; }
    }
    
    public class RowMeasureItem
    {
        public string DimensionId { get; init; }
        public string Key { get; init; }
        public double Value { get; init; }
        
        public string Fund { get; init; }
        public string Strategy { get; init; }
        public string Desk { get; init; }
    }

    public class Dimensions
    {
        private IDictionary<string, int> _dictionary;
        public IList<string> Columns { get; set; }

        public IDictionary<string, int> KeyIndices
        {
            get
            {
                _dictionary = new Dictionary<string, int>();
                
                for (int j = 0; j < Columns.Count; j++)
                {
                    if(!_dictionary.ContainsKey(Columns[j]))
                        _dictionary.Add(Columns[j], j);
                }

                return _dictionary;
            }
        }

        public IList<Row> Rows { get; set; }
    }

    public class RowMeasure
    {
        public string DimensionId { get; set; }
        public IList<string> Keys { get; set; }
        public IList<double> Measures { get; set; }
    }

    public class Measures
    {
        public IList<string> KeyColumns { get; set; }
        public IList<string> ValueColumns { get; set; }
        public IList<RowMeasure> Rows { get; set; }
    }

    public class Request
    {
        public string RequestId { get; set; }
        public Dimensions Dimensions { get; set; }
        public Measures Measures { get; set; }
    }
}