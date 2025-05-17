namespace LibraryManager.Persistence
{
    using System.Reflection;

    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
