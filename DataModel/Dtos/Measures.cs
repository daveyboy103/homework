using System.Collections.Generic;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable once UnusedAutoPropertyAccessor.Global
// ReSharper disable once CollectionNeverUpdated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace DataModel.Dtos
{
    public class Measures
    {
        public IList<string> KeyColumns { get; set; }
        public IList<string> ValueColumns { get; set; }
        public IList<RowMeasure> Rows { get; set; }
    }
}