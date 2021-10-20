using System.Collections.Generic;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable once UnusedAutoPropertyAccessor.Global
// ReSharper disable once CollectionNeverUpdated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace DataModel.Dtos
{
    public class RowMeasure
    {
        public string DimensionId { get; set; }
        public IList<string> Keys { get; set; }
        public IList<double> Measures { get; set; }
    }
}