namespace LibraryManager.SharedKernel.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;

    public static class EnumExtensions
    {
        public static string GetDescription([NotNull] this Enum e)
        {
            var fieldInfo = e.GetType().GetField(e.ToString())!;

            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            
            return e.ToString();
        }
    }
}
