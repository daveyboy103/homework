using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataModel.Dtos
{
    public class Row
    {
        public string DimensionId { get; set; }
        public IList<string> Dimensions { get; set; }
    }

    public class Dimensions
    {
        public IList<string> Columns { get; set; }
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