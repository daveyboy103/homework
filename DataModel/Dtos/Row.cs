using System.Collections.Generic;
// ReSharper disable UnusedAutoPropertyAccessor.Global. Deserialized from JSON
// ReSharper disable ClassNeverInstantiated.Global. Deserialized from JSON
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace DataModel.Dtos
{
    public class Row
    {
        public string DimensionId { get; set; }
        public IList<string> Dimensions { get; set; }
    }
}