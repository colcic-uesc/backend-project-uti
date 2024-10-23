using System.IO;

namespace UescColcicAPI.Middlewares
{
    public class EventLogMiddleware
    {
        private readonly RequestDelegate _next;

        public EventLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // 1. Capturar as informações da requisição
            var clientIp = context.Connection.RemoteIpAddress?.ToString();
            var hasJwtToken = context.Request.Headers.ContainsKey("Authorization");
            var requestTime = DateTimeOffset.Now;
            var method = context.Request.Method;
            var url = context.Request.Path;

            // Iniciar o cronômetro para medir o tempo de processamento
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            
            await _next.Invoke(context);

            // Parar o cronômetro 
            stopwatch.Stop();
            var processingTime = stopwatch.ElapsedMilliseconds;

            //  Conteúdo do log
            string logEntry = $"Data/Hora: {requestTime}, IP do Cliente: {clientIp}, " +
                              $"Possui Token JWT: {hasJwtToken}, Método: {method}, URL: {url}, " +
                              $"Tempo de Processamento: {processingTime}ms";

            // Definir o caminho do arquivo de log
            string logFilePath = "Logs/event_log.txt"; 
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)); 

            // Escrever o log no arquivo
            using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
            {
                await writer.WriteLineAsync(logEntry);
            }
        }
    }
}
