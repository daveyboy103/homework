using System.Collections.Generic;
// ReSharper disable UnusedAutoPropertyAccessor.Global. Deserialized from JSON
// ReSharper disable ClassNeverInstantiated.Global. Deserialized from JSON
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable once ClassNeverInstantiated.Global

namespace DataModel.Dtos
{
    public class Dimensions
    {
        private IDictionary<string, int> _dictionary;
        public IList<string> Columns { get; set; }

        public IDictionary<string, int> KeyIndices
        {
            get
            {
                if (_dictionary != null) return _dictionary;
                _dictionary = new Dictionary<string, int>();

                for (int j = 0; j < Columns.Count; j++)
                {
                    if (!_dictionary.ContainsKey(Columns[j]))
                        _dictionary.Add(Columns[j], j);
                }

                return _dictionary;
            }
        }

        public IList<Row> Rows { get; set; }
    }
}