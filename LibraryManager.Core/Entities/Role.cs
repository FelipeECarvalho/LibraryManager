namespace LibraryManager.Core.Entities
{
    using LibraryManager.Core.Enums;

    public class Role : BaseEntity
    {
        [Obsolete("EntityFrameworkCore constructor")]
        private Role()
        {
        }

        public Role(string name, RoleType roleType)
        {
            Name = name;
            RoleType = roleType;
        }

        public string Name { get; private set; }

        public RoleType RoleType { get; set; }
    }
}
