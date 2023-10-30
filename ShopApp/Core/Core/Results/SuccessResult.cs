using Core.Results.Bases;

namespace Core.Results
{
    /// EN: Generally in service classes to return a successful result from several methods, inherited 
    /// abstract Result class will have the value of IsSuccessful property true.
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {
        }

        public SuccessResult() : base(true, "")
        {
        }
    }
}
