using Serilog;

using WebApp;

var builder = WebApplication.CreateBuilder(args);


var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ObsceneWordsOption>(builder.Configuration.GetSection(nameof(ObsceneWordsOption)));
builder.Services.Configure<DirtyWordsOption>(builder.Configuration.GetSection(nameof(DirtyWordsOption)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();