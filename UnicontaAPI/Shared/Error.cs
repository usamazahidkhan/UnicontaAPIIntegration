namespace UnicontaAPI
{
    public class Error
    {
        public Error(object description) => this.Description = description;

        public Error(string code, object description)
        {
            this.Code = code;
            this.Description = description;
        }

        public string Code { get; }
        public object Description { get; }
    }
}