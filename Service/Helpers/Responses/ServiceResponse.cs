
namespace Service.Helpers.Responses
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; } = [];
    }
}
