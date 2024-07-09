using StagecraftDAL.Interface;
using StagecraftDAL.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IUser, UsersService>();
builder.Services.AddScoped<IAdmin, AdminService>();
builder.Services.AddScoped<IFile, FileService>();
builder.Services.AddScoped<ICourse, CourseService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<ITranscriptSegmentService, TranscriptSegmentService>();

// Add services to the container.

// Add services to the container.
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
 


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

             
   


