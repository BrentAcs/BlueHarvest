using BlueHarvest.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

var logger = Log.Logger = new LoggerConfiguration()
   .ReadFrom.Configuration(builder.Configuration)
   .CreateLogger();

Log.Verbose($"BlueHarvest API starting up on environment: '{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}'.");
Log.Debug($"BlueHarvest API starting up on environment: '{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}'.");
Log.Information(
   $"BlueHarvest API starting up on environment: '{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}'.");

// Add services to the container.
builder.Host.UseSerilog();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
   .AddEndpointsApiExplorer()
   .AddSwaggerGen()
   .AddApiVersioning(options =>
   {
      options.ReportApiVersions = true;
      options.UseApiBehavior = false;
   })
   .AddVersionedApiExplorer(options =>
   {
      options.GroupNameFormat = "'v'VVV";
      options.SubstituteApiVersionInUrl = true;
   })
   .AddBlueHarvestMongo(builder.Configuration)
   .AddBlueHarvestCommon()
   ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
