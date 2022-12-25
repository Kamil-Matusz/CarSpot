namespace CarSpot.Api.Exceptions
{
    public sealed class InvalidBookerNameException : CustomException
    {
        public InvalidBookerNameException() : base("Employee name is invalid.")
        {
        }
    }
}
