namespace Lukki.Contracts;

public class UniversalResponse<T>
{
    public string Message { get; set; }
    public T? Data { get; set; }

    private UniversalResponse(string message, T? data)
    {
        Message = message;
        Data = data;
    }
    public static UniversalResponse<T> Create(string message, T? data)
    {
        return new UniversalResponse<T>(message, data);
    }
}

