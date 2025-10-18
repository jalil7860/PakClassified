using PakClassified.Handler.LocationHandler;
using PakClassified.Handlers.AdvertisementHandler;
using PakClassified.Handlers.AdvertisementHandler.SubCategoryHandler;
using PakClassified.Handlers.UserHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddControllers().AddJsonOptions(x =>
//{
//    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalHost", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Local is allowed
               .AllowAnyHeader() // Allow all headers
               .AllowAnyMethod(); // Allow all HTTP methods
    });
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.WithOrigins("*") // all allowed origins
               .AllowAnyHeader() // Allow all headers
               .AllowAnyMethod(); // Allow all HTTP methods
    });
});
builder.Services.AddSingleton<ICountryHandler, CountryHandler>();
builder.Services.AddSingleton<IProvinceHanlder, ProvinceHandler>();
builder.Services.AddSingleton<ICityHandler, CityHandler>();
builder.Services.AddSingleton<ICityAreaHandler, CityAreaHandler>();
builder.Services.AddSingleton<IAdvertisementCategoryHandler, AdvertisementCategoryHandler>();
builder.Services.AddSingleton<ISubCategoryHandler, SubCategoryHandler>();
builder.Services.AddSingleton<IAdvertisementStatusHandler, AdvertisementStatusHandler>();
builder.Services.AddSingleton<IAdvertisementTypeHandler, AdvertisementTypeHandler>();
builder.Services.AddSingleton<IAdvertisementHandler, AdvertisementHandler>();
builder.Services.AddSingleton<IRoleHandler, RoleHandler>();
builder.Services.AddSingleton<IUserHandler , UserHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();
app.UseCors("LocalHost");

app.UseAuthorization();

app.MapControllers();

app.Run();
