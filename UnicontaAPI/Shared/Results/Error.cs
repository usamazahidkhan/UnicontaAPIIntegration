namespace UnicontaAPI.Shared
{
    public readonly struct Error(string key, IEnumerable<object> values)
    {
        public Error(string key, object value) : this(key, [value])
        {
            Key = key;
        }

        public string Key { get; } = key;
        public IEnumerable<object> Values { get; } = values;
    }
}