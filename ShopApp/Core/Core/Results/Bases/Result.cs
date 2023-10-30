namespace Core.Results.Bases
{
    /// EN: Generally in service classes the results of several methods will be returned as successful or error
    /// results through the classes inherited from this abstract class.
    public abstract class Result
    {
        public bool IsSuccessful { get; }

        public string? Message { get; set; }

        public Result(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}
