using System.Collections.Generic;
using BlueCrestHomework.Extensions;

namespace BlueCrestHomework.Models
{
    public class RequestBinding
    {
        public RequestBinding()
        {
            Rows = new List<BindingDataRow>();
        }
        
        public RequestBinding(IEnumerable<BindingDataRow> rows, double totalPnl)
        {
            Rows = rows;
            TotalPnl = totalPnl;
        }
        public string RequestId { get; init; }
        public IEnumerable<BindingDataRow> Rows { get; } 
        public double TotalPnl { get; set; }
        public bool ShowDetails { get; set; }
    }
}