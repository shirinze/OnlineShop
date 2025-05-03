namespace OnlineShop.Features;

public class BaseResult<T> : BaseResult
{
    public T? Value { get; set; }
    public void SetValue(T? value)
    {
        Value = value;
    }
}
public class BaseResult
{
    public bool IsSuccess { get; set; }
    public string Message { get;private set; } = string.Empty;

    public static BaseResult Success(string message="Done")
    {
        var result = new BaseResult();
        result.Ok(message);
        return result;

    }
    public static BaseResult Fail(string message)
    {
        var result = new BaseResult();
        result.Error(message);
        return result;
    }
    public static BaseResult<T> Success<T>(T value,string message="Done")
    {
        var result = new BaseResult<T>();
        result.SetValue(value);
        result.Ok(message);
        return result;
    }
    private void Error(string message)
    {
        Message = message;
        IsSuccess = false;
    }
    private void Ok(string message)
    {
        Message = message;
        IsSuccess = true;
    }
}
