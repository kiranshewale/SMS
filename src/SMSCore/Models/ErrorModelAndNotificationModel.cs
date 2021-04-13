namespace SMSCore.Models
{
    public class ErrorModelAndNotificationModel
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }

        public bool IsSuccess { get; set; }
        public string SuccessMessage { get; set; }
    }
}
