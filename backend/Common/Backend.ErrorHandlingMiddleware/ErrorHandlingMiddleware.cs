
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Backend.ErrorHandlingMiddleware
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				var response = context.Response;

				var result = JsonSerializer.Serialize(new { statusCode = response.StatusCode, message = ex.Message });
				await response.WriteAsync(result);
			}
		}
	}
}
