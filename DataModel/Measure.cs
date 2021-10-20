using System.Collections.Generic;

namespace DataModel
{
    public class Measure
    {
        public IEnumerable<string> KeyColumns { get; } = new List<string>();
        public IEnumerable<string> ValueColumns { get; } = new List<string>();
        public IEnumerable<MeasureRow> Rows { get; } = new List<MeasureRow>();

        public void AddKeyColumn(string column)
        {
            (KeyColumns as List<string>)?.Add(column);
        }

        public void AddValueColumn(string column)
        {
            (ValueColumns as List<string>)?.Add(column);
        }

        public MeasureRow AddRow(MeasureRow row)
        {
            (Rows as List<MeasureRow>)?.Add(row);
            return row;
        }
    }
}