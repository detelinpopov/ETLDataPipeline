using Core.Enums;

namespace Core.Models.Operations
{
    public class ErrorModel
    {
        public required string ErrorMessage { get; set; }

        public ErrorType ErrorType {  get; set; }
    }
}
