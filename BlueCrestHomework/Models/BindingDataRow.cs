using System;
using System.Collections.Generic;

namespace BlueCrestHomework.Models
{
    public class BindingDataRow
    {
        private readonly double _pnl;

        public BindingDataRow(Dictionary<string,string> colDataForDimension, bool pnlReported = true)
        {
            Fund = colDataForDimension["fundreference"];
            Desk = colDataForDimension["desk"];
            Strategy = colDataForDimension["strat"];
            PnlReported = pnlReported;
        }

        public string Fund { get; init; }
        public string Desk { get; init; }
        public string Strategy { get; init; }

        public double Pnl
        {
            get => Math.Round(_pnl, 2);
            init => _pnl = value;
        }

        public bool PnlReported { get; init; }
    }
}