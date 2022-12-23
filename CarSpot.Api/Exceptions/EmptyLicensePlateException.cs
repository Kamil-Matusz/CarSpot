namespace CarSpot.Api.Exceptions
{
    public class EmptyLicensePlateException: CustomException
    {
        public EmptyLicensePlateException() : base("License plate is empty")
        {
        }

    }
}
