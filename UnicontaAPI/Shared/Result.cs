namespace UnicontaAPI
{
    public class Result
    {
        public bool IsSucess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<Error> Errors { get; set; }

        public static Result Success(object data = null, string message = default)
        {
            return new()
            {
                IsSucess = true,
                Message = message,
                Data = data,
                Errors = []
            };
        }

        public static Result Failed(List<Error> errors, object data = null, string message = null)
        {
            return new()
            {
                IsSucess = false,
                Message = message,
                Data = data,
                Errors = errors
            };
        }

        public static Result Failed(Error error, object data = null, string message = null)
            => Failed([error], data, message);
    }
}