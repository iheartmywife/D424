using helpmepickmymain.AI;
using helpmepickmymain.Database;
using helpmepickmymain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenAI_API;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HmpmmDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("HelpMePickMyMainDbConnectionString")));

builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("HelpMePickMyMainAuthDbConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ISpecRepository, SpecRepository>();
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<IFactionRepository, FactionRepository>();
builder.Services.AddScoped<IWowClassRepository, WowClassRepository>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped(sp =>
{
    var apiKey = builder.Configuration["OpenAI:ApiKey"];
    var openAiApi = new OpenAIAPI(apiKey);
    return openAiApi;
});

builder.Services.AddScoped<OpenAi>(sp =>
{
    var openAiApi = sp.GetRequiredService<OpenAIAPI>();
    return new OpenAi(openAiApi);
});
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

//saving for future experimentation/development with openAI

//builder.Services.AddHttpClient<OpenAi>(client =>
//{
//    client.BaseAddress = new Uri("https://api.openai.com/v1/");
//});
//builder.Services.AddScoped<OpenAi>(sp =>
//{
//    var httpClient = sp.GetRequiredService<HttpClient>();
//    var openAiApiKey = builder.Configuration["OpenAI:ApiKey"];
//    return new OpenAi(httpClient, openAiApiKey);
//});

//builder.Services.AddHttpClient<OpenAiService>(client =>
//{
//    client.BaseAddress = new Uri("https://api.openai.com/v1/");
//})
//.ConfigureHttpClient((sp, client) =>
//{
//    var openAiApiKey = builder.Configuration["OpenAI:ApiKey"];
//    var openAiService = sp.GetRequiredService<OpenAiService>();
//    openAiService.SetApiKey(openAiApiKey);  // Set the API key after HttpClient is configured
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "test",
    pattern: "test/{action=Test}",
    defaults: new { controller = "Test" });

app.Run();
