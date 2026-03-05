using System.Reflection;

namespace NicoGetCookie.Prop
{
    public class Ver
    {
        public static readonly string Version = "0.0.1.02";
        public static readonly string VerDate = "2026/03/05";

        public static string GetFullVersion()
        {
            return GetAssemblyName() + " Ver " + Version + "(" + VerDate + ")";
        }

        public static string GetAssemblyName()
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();
            return assembly.Name;
        }
    }
}
