namespace Citizator.Model
{
    public class ApiConfiguration
    {
        private readonly string key;
        public ApiConfiguration(string key)
        {
            this.key = key;
        }
        public string Key
        {
            get { return key; }
        }
    }
}
