public class BaseResponse<T>
{
    public BaseResponse()
    {
        this.Messages = new List<string>();
    }

    public int ActionStatusCode { get; set; }
    public List<string> Messages { get; set; }
    public T Data { get; set; }

}