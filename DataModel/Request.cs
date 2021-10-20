using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json.Linq;

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

        public void LoadFromJson(JsonDocument document)
        {
            
        }
    }
}