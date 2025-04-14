using LoteriaWeb.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Configuração de logs - apenas console
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Conexão com o banco de dados
builder.Services.AddDbContext<LoteriaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Localização com arquivos .resx
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// Idiomas suportados
var supportedCultures = new[]
{
    new CultureInfo("pt-BR"),
    new CultureInfo("en-US"),
    new CultureInfo("es-ES"),
    new CultureInfo("fr-FR"),
    new CultureInfo("it-IT"),
    new CultureInfo("de-DE"),
    new CultureInfo("ru-RU"),
    new CultureInfo("ja-JP"),
    new CultureInfo("ko-KR"),
    new CultureInfo("zh-CN"),
    new CultureInfo("zh-TW"),
    new CultureInfo("ar-SA"),
    new CultureInfo("hi-IN"),
    new CultureInfo("tr-TR"),
    new CultureInfo("vi-VN"),
    new CultureInfo("id-ID"),
    new CultureInfo("th-TH"),
    new CultureInfo("pl-PL"),
    new CultureInfo("uk-UA"),
    new CultureInfo("ro-RO"),
    new CultureInfo("sv-SE"),
    new CultureInfo("nl-NL"),
    new CultureInfo("cs-CZ"),
    new CultureInfo("fi-FI")
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

var app = builder.Build();

// Aplica localização
app.UseRequestLocalization(localizationOptions);

// Middleware padrão
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


