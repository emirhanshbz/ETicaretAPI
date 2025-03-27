using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Infrastructure.Services.Storage.Local;
using ETicaretAPI.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()  //hangi originden gelen isteklere izin vereceğimizi ayarladık
));


builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices();

builder.Services.AddInfrastructureServices();

//builder.Services.AddStrorage(StorageType.Local);
builder.Services.AddStorage<LocalStorage>();  //local storage'a göre çalışacak
//builder.Services.AddStorage(ETicaretAPI.Infrastructure.Enums.StorageType.Azure);

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

//builder.Services.AddControllers(); builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();


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

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
