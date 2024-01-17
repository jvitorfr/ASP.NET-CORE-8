using App.Broker;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Hubs;
using App.Repository;
using App.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:5135")
                          .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                      });
});

;

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BaseDbContext>();

builder.Services.AddScoped<IQueueRepository, QueueRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddSignalR();

//builder.Services.AddDbContext<BaseDbContext>(options =>
//           options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnectionString")));
var app = builder.Build();

var consumer = new Consumer();
Task task = Task.Run(() => consumer.Consume("export"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ExportHub>("/Export/Download");
app.Run();



