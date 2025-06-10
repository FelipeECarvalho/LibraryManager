namespace LibraryManager.Core.Entities
{
    /// <summary>
    /// User responsible for making changes within the library scope
    /// </summary>
    public class Operator : User
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private Operator()
            : base()
        {
        }

        public IList<string> Permissions { get; private set; }
    }
}
