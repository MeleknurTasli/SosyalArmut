public class CustomBadRequest : IActionResult
{
    public CustomBadRequest(ActionContext context)
    {

    }

    public Task ExecuteResultAsync(ActionContext context)
    {
        BaseResponse<string> responseBody = new BaseResponse<string>();
        responseBody.ActionStatusCode = -1;

        if (context != null && context.ModelState != null)
        {
            for (int i = 0; i < context.ModelState.Count; i++)
            {
                try
                {
                    string message = String.Format("{0} hatasına dikkat ediniz.", context.ModelState.Values.ElementAt(i).Errors[i].ErrorMessage);
                    responseBody.Messages.Add(message);
                }
                catch { break; }
            }
        }

        context.HttpContext.Response.StatusCode = 400;
        context.HttpContext.Response.WriteAsJsonAsync(responseBody);

        return context.HttpContext.Response.CompleteAsync();
    }
}