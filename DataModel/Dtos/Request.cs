namespace DataModel.Dtos
{
    public class Request
    {
        public string RequestId { get; set; }
        public Dimensions Dimensions { get; set; }
        public Measures Measures { get; set; }
    }
}