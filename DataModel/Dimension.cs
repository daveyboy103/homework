using System.Collections.Generic;

namespace DataModel
{
    public class Dimension
    {
        public string DimensionId { get; init; }
        public IEnumerable<string> Columns { get; } = new List<string>();
        public IEnumerable<Row> Rows { get; } = new List<Row>();

        public void AddColumn(string column)
        {
            (Columns as List<string>)?.Add(column);
        }

        public Row AddRow(Row row)
        {
            (Rows as List<Row>)?.Add(row);
            return row;
        }
    }
}