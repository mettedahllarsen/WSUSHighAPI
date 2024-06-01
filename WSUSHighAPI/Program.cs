using WSUSHighAPI.Contexts;
using WSUSHighAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Defining a constant string for the CORS policy name
const string allowSpecificOriginPolicy = "AllowSpecificOrigin";

// Configuring CORS (Cross-Origin Resource Sharing) services
builder.Services.AddCors(options =>
{
	// Adding a policy with the name specified in allowSpecificOriginPolicy
	options.AddPolicy(name: allowSpecificOriginPolicy, policy =>
	{
		// Specifying that requests from "https://example.com" are allowed
		policy.WithOrigins("https://example.com")
			  // Allows any HTTP method (GET, POST, PUT, DELETE, etc.)
			  .AllowAnyMethod()
			  // Allows any HTTP header
			  .AllowAnyHeader();
	});
});

builder.Services.AddControllers();

// Adding Swagger generation service for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registering a singleton instance of ComputersRepository
builder.Services.AddSingleton<ComputersRepository>(new ComputersRepository());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Enabling the CORS policy defined earlier as default
app.UseCors(allowSpecificOriginPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();