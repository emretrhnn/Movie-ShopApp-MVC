using Core.Results.Bases;

namespace Core.Results
{
    /// EN: Generally in service classes to return an error result from several methods, inherited 
    /// abstract Result class will have the value of IsSuccessful property false.
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message)
        {
        }

        public ErrorResult() : base(false, "")
        {
        }
    }
}
