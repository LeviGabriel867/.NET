using Dws.Note_one.Api.Persistence.Context;
using Dws.Note_one.Api.Persistence.Repositories;
using Dws.Note_one.Api.Persistence.Repositories.IRepositories; 
using Dws.Note_one.Api.Services.IServices; 
using Microsoft.EntityFrameworkCore;
using Dws.Note_one.Api.Persistence.Repositories; 
using Dws.Note_one.Api.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("groceries-api-in-memory"); 
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>(); 
builder.Services.AddScoped<IProductService, ProductService>();    
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();            
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); 

app.MapControllers();

app.Run();