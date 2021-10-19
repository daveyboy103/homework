using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataModel
{
    public class Request
    {
        public IEnumerable<Dimension> Dimensions { get; } = new List<Dimension>();
        public IEnumerable<Measure> Measures { get; } = new List<Measure>();

        public Dimension AddDimension(Dimension dimension)
        {
            (Dimensions as List<Dimension>)?.Add(dimension);
            return dimension;
        }

        public Measure AddMeasure(Measure measure)
        {
            (Measures as List<Measure>)?.Add(measure);
            return measure;
        }
    }

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

    public class MeasureRow
    {
        public string DimensionId { get; init; }
        public IEnumerable<string> Keys { get; } = new List<string>();
        public IEnumerable<decimal> Measures { get; } = new List<decimal>();

        public void AddKey(string key)
        {
            (Keys as List<string>)?.Add(key);
        }
        
        public void AddMeasure(decimal measure)
        {
            (Measures as List<decimal>)?.Add(measure);
        }
    }

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

    public class Measures
    {
        public IEnumerable<KeyColumn> KeyColumns { get; } = new List<KeyColumn>();
        public IEnumerable<ValueColumn> ValueColumns { get; } = new List<ValueColumn>();
    }

    public class ValueColumn
    {
    }

    public class KeyColumn
    {
    }

    public class Row
    {
        public string DimensionId { get; init; }
        public IEnumerable<string> Dimensions { get; } = new List<string>();

        public void AddDimension(string dimension)
        {
            (Dimensions as List<string>)?.Add(dimension);
        }
    }
}