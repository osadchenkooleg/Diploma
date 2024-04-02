using Hellang.Middleware.ProblemDetails;
using VideoHosting_Back_end.Configurations;
using VideoHosting_Back_end.ExceptionHandlers;
using VideoHosting_Back_end.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.ConfigureProblemDetails();

builder.Services.ConfigureFluentValidation<Program>();

builder.Services.ConfigureSwagger();
builder.Services.ConfigureDatabase(builder.Configuration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseItToSeedSqlServer();
}

app.UseHttpsRedirection();

//app.UseExceptionHandler();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseProblemDetails();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
