namespace UescColcicAPI.Middlewares
{
    public class ResponseAppendMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseAppendMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            
            context.Response.Headers.Append("X-App-Name", "ColcicAPIUesc");

            
            context.Response.Headers.Append("X-APP-API-VERSION", "0.1");


            await _next.Invoke(context);
        }
    }
}
