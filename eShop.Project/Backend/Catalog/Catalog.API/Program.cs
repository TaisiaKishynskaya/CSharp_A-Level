var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CatalogDb");
builder.Services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Catalog.API")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();

builder.Services.AddScoped<IGenericRepository<CatalogTypeEntity>, CatalogTypeRepository>();
builder.Services.AddScoped<IGenericRepository<CatalogBrandEntity>, CatalogBrandRepository>();
builder.Services.AddScoped<IGenericRepository<CatalogItemEntity>, CatalogItemRepository>();

builder.Services.AddScoped<ICatalogTypeService, CatalogTypeService>();
builder.Services.AddScoped<ICatalogBrandService, CatalogBrandService>();
builder.Services.AddScoped<ICatalogItemService, CatalogItemService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

CatalogDbInitializer.EnsureDatabaseCreated(app.Services);

app.Run();
