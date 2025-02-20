using ETicaretAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()  //hangi originden gelen isteklere izin vereceğimizi ayarladık
));

builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer(); //?

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ETicaretAPI.API v1");
        c.RoutePrefix = "swagger";
    });
    app.MapOpenApi();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
