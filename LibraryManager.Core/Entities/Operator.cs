namespace LibraryManager.Core.Entities
{
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
