using System.Linq;
using System.Reflection;

public static class ExtensionMethods
{
    public static void CopyPropertiesTo<T>(this T source, T dest)
    {
        var plist = from prop in typeof(T).GetProperties() where prop.CanRead && prop.CanWrite select prop;

        foreach (PropertyInfo prop in plist)
        {
            prop.SetValue(dest, prop.GetValue(source, null), null);
        }
    }
}