using CollegeApi.MongoSettings;
using CollegeApi.Services.Interface;
using CollegeApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DbModel>(builder.Configuration.GetSection("MongoDBConfig"));
// Register DbContext as a singleton service
builder.Services.AddSingleton<DbContext>();

builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ICollegeService, CollegeService>();
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
