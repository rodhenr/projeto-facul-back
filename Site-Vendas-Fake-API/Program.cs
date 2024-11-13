using System.Text.Json.Serialization;
using Site_Vendas_Fake_API;
using Site_Vendas_Fake_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Built-in Services
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddHttpClient();
builder.Services.AddCors(
    opt => opt.AddPolicy(name: "Front", 
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }
));

// Custom Services
builder.Services.AddCustomServices(builder.Configuration);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<CurrentUserAccessor>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

// Built-in
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Front");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();