using System.Collections.Generic;

namespace DataModel
{
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