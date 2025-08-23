namespace BloodDonor.Mvc.Exceptions
{
    public class RepositoryOpearationFailedException: Exception
    {
        public RepositoryOpearationFailedException()
        {
        }
        public RepositoryOpearationFailedException(string message) : base(message)
        {
        }
        public RepositoryOpearationFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
