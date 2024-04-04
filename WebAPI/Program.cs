using Business.Abstract;
using Business.Concrete;
using Core.DependencyResolvers;
using Core.Utilities.IoC;
using Core.Extensions;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Autofac;
using Business.DependencyResolvers;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//IoC --> Business --> AutofacBusinessModule
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});

// Add services to the container.

//CoreModule implemantasyonu
builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//IoC
//builder.Services.AddSingleton<IHouseListingService, HouseListingManager>();
//builder.Services.AddSingleton<IHouseListingDal, EfHouseListingDal>();
//builder.Services.AddSingleton<IListingService, ListingManager>();
//builder.Services.AddSingleton<IListingDal, EfListingDal>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Yazdýðýmýz exception extension ý ekliyoruz
app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
