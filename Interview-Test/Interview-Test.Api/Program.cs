using Interview_Test.Infrastructure;
using Interview_Test.Middlewares;
using Interview_Test.Repositories;
using Interview_Test.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddTransient<AuthenMiddleware>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<InterviewTestDbContext>(options =>
    {
        options.UseSqlServer(connection,
            sqlOptions =>
            {
                sqlOptions.UseCompatibilityLevel(110);
                sqlOptions.CommandTimeout(30);
                sqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
            });
    }
);

var app = builder.Build();

// Auto Migration on Startup with Retry
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var context = services.GetRequiredService<InterviewTestDbContext>();
    
    var retryCount = 0;
    var maxRetries = 10;
    var retryDelay = TimeSpan.FromSeconds(5);
    
    while (retryCount < maxRetries)
    {
        try
        {
            logger.LogInformation("Attempting to connect to database... (Attempt {Retry}/{MaxRetries})", retryCount + 1, maxRetries);
            
            // ตรวจสอบว่าเชื่อมต่อได้หรือไม่
            if (context.Database.CanConnect())
            {
                logger.LogInformation("Database connection successful. Starting migration...");
                context.Database.Migrate();
                logger.LogInformation("Database migration completed successfully.");
                
                break;
            }
        }
        catch (Exception ex)
        {
            retryCount++;
            if (retryCount >= maxRetries)
            {
                logger.LogError(ex, "Failed to migrate database after {MaxRetries} attempts.", maxRetries);
                throw;
            }
            
            logger.LogWarning(ex, "Database connection failed. Retrying in {Delay} seconds... (Attempt {Retry}/{MaxRetries})", 
                retryDelay.TotalSeconds, retryCount + 1, maxRetries);
            Thread.Sleep(retryDelay);
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseMiddleware<AuthenMiddleware>();
app.UseMvc();
app.Run();
