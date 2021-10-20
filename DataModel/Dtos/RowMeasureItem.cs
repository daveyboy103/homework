namespace DataModel.Dtos
{
    public class RowMeasureItem
    {
        public string DimensionId { get; init; }
        public string Key { get; init; }
        public double Value { get; init; }
        
        public string Fund { get; init; }
        public string Strategy { get; init; }
        public string Desk { get; init; }
    }
}