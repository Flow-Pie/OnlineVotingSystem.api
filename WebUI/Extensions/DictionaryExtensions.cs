namespace WebUI.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<K, V> SetItem<K, V>(this Dictionary<K, V> dict, K key, V value)
        {
            var newDict = new Dictionary<K, V>(dict);
            newDict[key] = value;
            return newDict;
        }

        public static Dictionary<K, V> RemoveItem<K, V>(this Dictionary<K, V> dict, K key)
        {
            var newDict = new Dictionary<K, V>(dict);
            newDict.Remove(key);
            return newDict;
        }
    }
}
