using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Tutorial.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Tutorial.Infrastructure.Extensions;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadOnly;
using FPLSP_Tutorial.Infrastructure.Implements.Repositories.ReadWrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication();

builder.Services.AddLocalization(builder.Configuration);

builder.Services.AddEventBus(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMajorRequestReadOnlyRepository, MajorRequestReadOnlyRepository>();
builder.Services.AddScoped<IMajorRequestReadWriteRepository, MajorRequestReadWriteRepository>();


builder.Services.AddScoped<IUserMajorReadWriteRepository, UserMajorReadWriteRepository>();
builder.Services.AddScoped<IUserMajorReadOnlyRepository, UserMajorReadOnlyRepository>();

builder.Services.AddScoped<IPostReadOnlyRespository, PostReadOnlyRepository>();
builder.Services.AddScoped<IPostReadWriteRepository, PostReadWriteRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();