namespace Authentication.Domain.Commands
{
    public class RequestResult
    {
        public RequestResult() { }

        public RequestResult(string message, object data)
        {
            Message = message;
            Data = data;
        }

        public string Message { get; set; }
        public object Data { get; set; }
    }
}
