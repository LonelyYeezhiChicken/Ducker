using Docker_Service.Config;
using Docker_Service.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.DIContainer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var allExeService = services.GetRequiredService<IAllExeService>();

    await allExeService.GetOrCreateAllExe();
}


app.MapGet("/hello", () =>
{
    return "hi hi";
})
.WithName("Hello");


app.MapGet("/exes", async (IDuckerService duckerService) =>
{
    var exes = await duckerService.GetRegisteredExes();
    return exes;
})
.WithName("GetExes");

app.MapPost("/exes/start", async (string exePath, IDuckerService duckerService) =>
{
    await duckerService.StartExe(exePath);
})
.WithName("StartExe");


app.MapGet("/exes/stop", async (int exeId, IDuckerService duckerService) =>
{
    await duckerService.StopExe(exeId);
})
.WithName("StopExe");


await app.RunAsync();
