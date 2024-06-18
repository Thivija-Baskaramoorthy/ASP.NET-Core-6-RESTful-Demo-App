using WebAppDemo;
using Microsoft.EntityFrameworkCore;
using WebAppDemo.Services.StudentService;
using WebAppDemo.Services.SubjectService;
using WebAppDemo.Services.Student_Subject;
// using WebAppDemo.Hubs;
using Microsoft.AspNetCore.SignalR;
using WebAppDemo.Services.HubService;
using WebAppDemo.Services.HubService.ClientConnectionService;
using WebAppDemo.Services.Email_Service;
using WebAppDemo.Helpers.Email;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();


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

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add email configuration
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));


// add cors to make sure it works with all url (FE)
builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();   // make the application use implemented CORS

app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("/notificationHub");


app.Run();
