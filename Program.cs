using WebAppDemo;
using Microsoft.EntityFrameworkCore;
using WebAppDemo.Services.StudentService;
using WebAppDemo.Services.SubjectService;
using WebAppDemo.Services.Student_Subject;
// using WebAppDemo.Hubs;
using Microsoft.AspNetCore.SignalR;
using WebAppDemo.Services.HubService;
using WebAppDemo.Services.ClientConnectionService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();

// Add Application Db Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});  

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IStudentSubjectService, StudentSubjectService>();
builder.Services.AddSingleton<IClientConnectionService,ClientConnectionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapHub<NotificationHub>("/notificationHub");


app.Run();
