using Microsoft.EntityFrameworkCore;
using BurguerManiaAPI.Context;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Mvc;

using BurguerManiaAPI.Services.User;
using BurguerManiaAPI.Services.UserOrder;
using BurguerManiaAPI.Services.Product;
using BurguerManiaAPI.Services.Category;
using BurguerManiaAPI.Services.Order;
using BurguerManiaAPI.Services.OrderProduct;

using BurguerManiaAPI.Interfaces.User;
using BurguerManiaAPI.Interfaces.UserOrder;
using BurguerManiaAPI.Interfaces.Product;
using BurguerManiaAPI.Interfaces.Category;
using BurguerManiaAPI.Interfaces.Order;
using BurguerManiaAPI.Interfaces.OrderProduct;

var builder = WebApplication.CreateBuilder(args);

// Configura os serviços da aplicação.
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // Personaliza a resposta de erro 400 para mostrar mensagens detalhadas de validação.
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(e => e.Value != null && e.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray() ?? Array.Empty<string>()
                );

            var result = new
            {
                status = 400,
                errors = errors
            };

            return new BadRequestObjectResult(result);
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Configura os créditos da API no Swagger
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Burguer Mania API",
        Version = "v1",
        Description = "API desenvolvida por Igor Damasceno ",

    });
});

// Registro dos serviços com injeção de dependência
builder.Services.AddScoped<UserInterface, UserService>();
builder.Services.AddScoped<UserOrderInterface, UserOrderService>();
builder.Services.AddScoped<ProductInterface, ProductService>();
builder.Services.AddScoped<CategoryInterface, CategoryService>();
builder.Services.AddScoped<OrderInterface, OrderService>();
builder.Services.AddScoped<IOrderProductInterface, OrderProductService>();

// Configuração da conexão com o MySQL
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Adiciona o DbContext com suporte a MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Exibe mensagens de erro detalhadas em desenvolvimento
}

// Configuração do Swagger para documentação da API
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Mapeia os controllers para endpoints HTTP
app.Run();
