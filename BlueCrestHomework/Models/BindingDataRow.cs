using System.Collections.Generic;

namespace BlueCrestHomework.Models
{
    public class BindingDataRow
    {
        public BindingDataRow(){}

        public BindingDataRow(IReadOnlyDictionary<string, string> colDataForDimension,
            IReadOnlyDictionary<string, double> pnlSubComponents = null)
        {
            Fund = colDataForDimension[Constants.Fund];
            Desk = colDataForDimension[Constants.Desk];
            Strategy = colDataForDimension[Constants.Strategy];
            PnlSubComponents = pnlSubComponents;
        }

        /// <summary>
        /// Not used in UI but aids debugging
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string DimensionId { get; init; }
        public string Fund { get; init; }
        public string Desk { get; init; }
        public string Strategy { get; init; }
        public double Pnl { get; init; }
        public IReadOnlyDictionary<string, double> PnlSubComponents { get; init;  } 
    }
}