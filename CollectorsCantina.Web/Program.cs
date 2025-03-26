using CollectorsCantina.Web.Components;
using CollectorsCantina.Web.Helpers;
using CollectorsCantina.Web.Services.Collections;
using CollectorsCantina.Web.Services.Items;
using CollectorsCantina.Web.State;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiUrl = builder.Configuration["ApiClientUrl"];
builder.Services.AddHttpClient("CollectorsCantinaApi", client =>
{
    client.BaseAddress = new Uri(apiUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("Accept", "application/x-www-form-urlencoded");
    client.DefaultRequestHeaders.Add("Accept", "text/plain");
});

builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<ICollectionsService, CollectionsService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IItemsService, ItemsService>();
builder.Services.AddScoped<ApplicationState>();
builder.Services.AddSingleton<IApiHelper, ApiHelper>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
