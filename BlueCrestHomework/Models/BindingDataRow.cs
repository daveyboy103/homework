using System;
using System.Collections.Generic;

namespace BlueCrestHomework.Models
{
    public class BindingDataRow
    {
        private readonly double _pnl;

        public BindingDataRow()
        {
            
        }
        public BindingDataRow(Dictionary<string,string> colDataForDimension, 
            bool pnlReported = true,
            IDictionary<string, double> pnlSubComponents = null)
        {
            Fund = colDataForDimension[Constants.Fund];
            Desk = colDataForDimension[Constants.Desk];
            Strategy = colDataForDimension[Constants.Strategy];
            PnlReported = pnlReported;
            PnlSubComponents = pnlSubComponents;
        }

        public string DimensionId { get; init; }
        public string Fund { get; init; }
        public string Desk { get; init; }
        public string Strategy { get; init; }

        public double Pnl
        {
            get => Math.Round(_pnl, 2);
            init => _pnl = value;
        }

        public bool PnlReported { get; init; }
        
        public IDictionary<string, double> PnlSubComponents { get; init;  } 
    }
}