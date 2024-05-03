namespace LeaveManagement4.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate request;

        public ExceptionMiddleware(RequestDelegate request)
        {
            this.request = request;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await request(httpContext);
            }
            catch (Exception ex)
            {
                
                    httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Not found");
            }
        }
    }
}
