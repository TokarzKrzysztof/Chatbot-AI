using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ErrorHandlingMiddleware.StartupExtensions
{
    public static class ErrorHandlingMiddlewareExtension
    {
        public static void AddErrorHandlingMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
