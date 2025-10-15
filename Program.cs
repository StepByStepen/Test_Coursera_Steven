using WebApplication1.Middleware;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// baca env var juga (mis. Auth__ApiKey)
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI store sederhana (in-memory)
builder.Services.AddSingleton<IUserStore, InMemoryUserStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware custom
app.UseRequestLogging();   // logging tiap request
app.UseApiKeyAuth();       // auth via header X-API-Key

app.UseAuthorization();

// endpoints
app.MapControllers();
app.MapGet("/", () => Results.Ok(new { name = "WebApplication1", version = "1.0.0" }));
app.MapGet("/health", () => Results.Ok("OK"));

app.Run();
