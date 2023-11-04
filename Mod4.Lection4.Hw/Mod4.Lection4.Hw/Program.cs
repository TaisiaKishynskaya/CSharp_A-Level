using Mod4.Lection4.Hw.Context;
using Microsoft.EntityFrameworkCore;

// конфігурація переноситься туди, де налаштовується депенденсі інжекшен (в дагому випадку - веб апі)
var builder = WebApplication.CreateBuilder(args);

// Add services to the container. тут налаштовуємо сервіси, як і що буде інжектатись включно з конфгурацією

// тут додаємо контекст (реєструємо через сервіс), в дженерік вказуємо який саме контекст, лямбда для обшенів, в яких вказуємо провайдер, щоб не вказувати напряму
builder.Services.AddDbContext<EFCoreContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(EFCoreContext))));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
