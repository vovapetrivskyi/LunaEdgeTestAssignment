using Serilog;
using System.Text;

namespace LunaEdgeWebAPI.Middlewares
{
	public class LoggingMiddleware
	{
		private readonly RequestDelegate _next;

		public LoggingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			// Log the request
			await LogRequest(context);

			try
			{
				// Swap out the response stream for a memory stream to log the response
				var originalBodyStream = context.Response.Body;
				using (var responseBody = new MemoryStream())
				{
					context.Response.Body = responseBody;

					// Continue processing the request in the pipeline
					await _next(context);

					// Log the response
					await LogResponse(context);

					// Copy the content of the new memory stream (responseBody) to the original stream
					await responseBody.CopyToAsync(originalBodyStream);
				}
			}
			catch (Exception ex)
			{
				// Log the exception
				LogError(ex);
				throw; // Re-throw the exception to ensure the proper status code and behavior is maintained
			}
		}

		private async Task LogRequest(HttpContext context)
		{
			context.Request.EnableBuffering(); // Enable request body buffering to allow reading multiple times
			var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
			await context.Request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length));
			var bodyAsText = Encoding.UTF8.GetString(buffer);

			Log.Information("Request Information: {Method} {Path} {QueryString} {Body}",
				context.Request.Method,
				context.Request.Path,
				context.Request.QueryString,
				bodyAsText);

			context.Request.Body.Position = 0; // Reset body stream position for further processing
		}

		private async Task LogResponse(HttpContext context)
		{
			context.Response.Body.Seek(0, SeekOrigin.Begin);
			var bodyAsText = await new StreamReader(context.Response.Body).ReadToEndAsync();
			context.Response.Body.Seek(0, SeekOrigin.Begin);

			Log.Information("HTTP Response Information: {StatusCode} {Body}",
				context.Response.StatusCode,
				bodyAsText);
		}

		private void LogError(Exception ex)
		{
			Log.Error("An error occurred: {Message} {StackTrace}", ex.Message, ex.StackTrace);
		}
	}
}
