namespace DatasetLoader.Api.Abstractions
{
    public class ValidationError
    {
        public string Key { get; set; }
        
        public string Error { get; set; }

        public ValidationError(string key, string error)
        {
            Key = key;
            Error = error;
        }
    }
}