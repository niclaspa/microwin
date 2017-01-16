namespace Microwin.Config
{
    public interface IAppSettingsReader
    {
        string ReadString(string key, bool throwIfNullOrWhiteSpace = false);

        string[] ReadStringArray(
            string key, 
            bool throwIfNullOrWhiteSpace = false, 
            bool trimValues = true, 
            bool includeEmptyValues = false);

        bool ReadBool(string key, bool throwOnError = false);
    }
}
