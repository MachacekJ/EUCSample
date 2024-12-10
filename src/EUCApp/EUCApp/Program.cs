using EUCApp.Client.Configuration;
using EUCApp.Components;
using EUCApp.Configuration;
using EUCApp.Modules.PatientModule.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddRazorComponents()
  .AddInteractiveServerComponents()
  .AddInteractiveWebAssemblyComponents();


builder.Services.AddAntiforgery(options =>
{
  options.HeaderName = "X-XSRF-TOKEN";
  options.Cookie.Name = "__Host-X-XSRF-TOKEN";
  options.Cookie.SameSite = SameSiteMode.Strict;
  options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddEUCMediatrAndValidation();
builder.Services.AddEUCSharedConfiguration();
builder.Services.AddPatientModule(builder.Configuration);

var app = builder.Build();
var allSupportedLanguages = SupportedLanguage.AllSupportedLanguages.Select(i => i.Id).ToArray();
app.UseRequestLocalization(new RequestLocalizationOptions()
  .SetDefaultCulture("cs-CZ")
  .AddSupportedCultures(allSupportedLanguages)
  .AddSupportedUICultures(allSupportedLanguages));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
  app.UseSwagger();
  app.UseSwaggerUI();
}
else
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}


app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
  .AddInteractiveServerRenderMode()
  .AddInteractiveWebAssemblyRenderMode()
  .AddAdditionalAssemblies(typeof(EUCApp.Client._Imports).Assembly);

app.MapPatientModuleEndpoints();
app.Run();