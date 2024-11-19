namespace BankApp.Database.Repositories
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; } = default!;
        public List<string> Errors { get; set; } = new List<string>();

        // Başarılı bir sonuç için  metod
        public static Result<T> Success(T data, string message = "")
        {
            return new Result<T> { IsSuccess = true, Data = data, Message = message };
        }

        // Başarısız bir sonuç için 
        public static Result<T> Failure(List<string> errors, string message = "", T data = default!)
        {
            return new Result<T> { IsSuccess = false, Errors = errors, Message = message, Data = data };
        }
    }
}
