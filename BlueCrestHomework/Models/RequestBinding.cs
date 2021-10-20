using System.Collections.Generic;
using BlueCrestHomework.Extensions;

namespace BlueCrestHomework.Models
{
    public class RequestBinding
    {
        public string RequestId { get; init; }
        public IEnumerable<string> Headers { get; } = new List<string>();
        public IEnumerable<BindingDataRow> Rows { get; } = new List<BindingDataRow>();
        public double TotalPnl { get; set; }
    }
}