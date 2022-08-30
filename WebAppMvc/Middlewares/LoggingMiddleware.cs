using WebAppMvc.Models;

namespace WebAppMvc.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private  IBlogRepository _blogRepository;
        public LoggingMiddleware(RequestDelegate next,IBlogRepository  blogRepository)
        {
            _next = next;
            _blogRepository = blogRepository;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            //_blogRepository.AddUser();
            LogConsole(context);
            await LogFile(context);
            await _next(context);

           
        }
        private void LogConsole(HttpContext context)
        {
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }
        private async Task LogFile(HttpContext context)
        {
            string logMess = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";

            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");

            await File.AppendAllTextAsync(logFilePath, logMess);


            
        }
    }
}
