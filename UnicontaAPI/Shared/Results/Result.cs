namespace UnicontaAPI.Shared
{
    public struct Result
    {
        public bool IsSucess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public Dictionary<string, IEnumerable<object>> Errors { get; set; }

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

        public static Result Failed(
            Dictionary<string, IEnumerable<object>> errors,
            object data = null,
            string message = null)
        {
            return new()
            {
                IsSucess = false,
                Message = message,
                Data = data,
                Errors = errors
            };
        }

        public static Result Failed(
            List<Error> errors,
            object data = null,
            string message = null)
        {
            return Failed(
                errors: errors.ToDictionary(x => x.Key, x => x.Values),
                data: data,
                message: message
            );
        }

        public static Result Failed(
            Error error,
            object data = null,
            string message = null)
            => Failed([error], data, message);
    }
}