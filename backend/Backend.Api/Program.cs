using Backend.Database.StartupExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Backend.ErrorHandlingMiddleware.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
	// do not throw error on request when some data is missing, (implicit [Required])
	options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
	// allow to return null from requests
	options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
}).AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
	
builder.Services.AddCors(policyBuilder =>
	policyBuilder.AddDefaultPolicy(policy =>
		policy.WithOrigins("http://localhost:4200")
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials())
);

builder.AddDatabase();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

// custom app extensions
app.AddErrorHandlingMiddleware();

app.MapControllers();

app.Run();
