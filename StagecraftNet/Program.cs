using StagecraftDAL;
using StagecraftDAL.Interface;
using Microsoft.Extensions.Configuration;
using StagecraftDAL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; // מוסיף את IServiceCollection ו- AddHttpClient

using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IUser, UsersService>();
builder.Services.AddScoped<IAdmin, AdminService>();
builder.Services.AddScoped<IFile, FileService>();
builder.Services.AddScoped<ICourse, CourseService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<ITranscriptSegmentService, TranscriptSegmentService>();
builder.Services.AddScoped<IForum, ForumService>();
builder.Services.AddScoped<IPayment, PaymentService>();
builder.Services.AddScoped<IFileUpload, FileUploadService>(); // הוספת השירות להעלאת קבצים
builder.Services.Configure<CloudStorageSettings>(builder.Configuration.GetSection("CloudStorage")); // שימוש במשתנה configuration מהמיקום הנכון
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IFileUpload, FileUploadService>();
builder.Services.Configure<CloudStorageSettings>(builder.Configuration.GetSection("CloudStorage"));


builder.Services.AddScoped<ISignup, SignupService>();
builder.Services.AddScoped<ILogin, LoginService>();
// הוסף את השירות FeedbackService
builder.Services.AddScoped<IFeedback, FeedbackService>();
builder.Services.AddHttpClient();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors("AllowAngularClient");

// הגדרת נתיבי HTTP
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

             
   


