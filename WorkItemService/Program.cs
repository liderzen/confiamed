using WorkItemService.BLL.Interfaces;
using WorkItemService.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Registrar WorkItemRepository
builder.Services.AddScoped<IWorkItemRepository>(provider =>
    new WorkItemRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("UserService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5026/");
});

builder.Services.AddScoped<IWorkItemService, WorkItemService.BLL.Services.WorkItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
