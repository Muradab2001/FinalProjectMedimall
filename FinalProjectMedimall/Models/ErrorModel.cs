namespace FinalProjectMedimall.Models
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public override string ToString()
        {
            return $"{StatusCode}\n{Message}";
        }
    }

}
