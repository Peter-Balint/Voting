namespace Voting.Blazor.Model
{
    /// <summary>
    /// Perzisztencia elérhetetlenség kivétel típusa.
    /// </summary>
    public class PersistenceUnavailableException : Exception
    {
        public PersistenceUnavailableException(string message) : base(message) { }

        public PersistenceUnavailableException(Exception innerException) : base("Exception occurred.", innerException) { }
    }
}
