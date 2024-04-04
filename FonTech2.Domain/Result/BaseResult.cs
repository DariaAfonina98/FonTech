namespace FonTech2.Domain.Result;

public class BaseResult
{
    public bool IsSuccess => ErrorMessage==null;
    public string? ErrorMessage { get; set; }
    public int? ErrorCode { get; set; }
}

public class BaseResult<T> : BaseResult
{
    public BaseResult(string errorMassage, int errorCode, T data)
    {
        ErrorMessage = errorMassage;
        ErrorCode = errorCode;
        Data = data;
    }
    public BaseResult(){}
    public T? Data { get; set; }
}