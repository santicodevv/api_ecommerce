using ApiEcommerce.Interfaces.User;
using ApiEcommerce.Mapping;
using ApiEcommerce.Repository;
using ApiEcommerce.Repository.IRepository;
using ApiEcommerce.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = builder.Configuration.GetConnectionString("ConexionSql");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper( cg => {}, typeof(CategoryProfile) );
builder.Services.AddAutoMapper( cg => {}, typeof(ProductProfile) );
builder.Services.AddAutoMapper( cg => {}, typeof(UserProfile) );

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();