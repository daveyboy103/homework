using System.Collections.Generic;

namespace DataModel
{
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
}