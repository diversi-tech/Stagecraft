using StagecraftDAL;
using StagecraftDAL.Interface;
using Microsoft.Extensions.Configuration;
using StagecraftDAL.Services;
using Microsoft.Extensions.DependencyInjection; // ����� �� IServiceCollection �- AddHttpClient
using Microsoft.AspNetCore.Http.Features; // ����� �� ���� FormOptions
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.Logging; // ����� �� ���� �����

var builder = WebApplication.CreateBuilder(args);

// ����� ������� ����� �-DI
builder.Services.AddScoped<IUser, UsersService>();
builder.Services.AddScoped<IAdmin, AdminService>();
builder.Services.AddScoped<IFile, FileService>();
builder.Services.AddScoped<ICourse, CourseService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<ITranscriptSegmentService, TranscriptSegmentService>();


builder.Services.AddScoped<IForum, ForumService>();


builder.Services.AddScoped<ISignup, SignupService>();



builder.Services.AddScoped<IProgressService, ProgressService>();

builder.Services.AddScoped<IPayment, PaymentService>();
builder.Services.AddScoped<IFileUpload, FileUploadService>(); // ����� ������ ������ �����


builder.Services.AddScoped<ILogin, LoginService>();
builder.Services.AddScoped<IFeedback, FeedbackService>();
builder.Services.Configure<CloudStorageSettings>(builder.Configuration.GetSection("CloudStorage"));
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue; // �� ��� ���� ��� �� �� ����
});
builder.Services.AddControllers();
builder.Services.AddHttpClient();
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

// Configure form options for large file uploads
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10485760000; // 100 MB, adjust as needed
    options.MultipartHeadersLengthLimit = 64 * 1024; // Adjust headers length limit if needed
});

// Configure Kestrel server options for large requests and longer timeouts
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 10485760000; // 100 MB, adjust as needed
    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
    serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAngularClient");
app.UseAuthorization();

app.MapControllers();

app.Run();
