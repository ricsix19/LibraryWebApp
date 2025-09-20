using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LibraryWebApp;
using System.Net.Http.Headers;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp =>
{
    var client = new HttpClient(new AuthMessageHandler(sp))
    {
        BaseAddress = new Uri("http://localhost:5078/")
    };
    return client;
});

await builder.Build().RunAsync();

public class AuthMessageHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;
    public AuthMessageHandler(IServiceProvider serviceProvider)
    {
        InnerHandler = new HttpClientHandler();
        _localStorage = serviceProvider.GetRequiredService<ILocalStorageService>();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _localStorage.GetItemAsStringAsync("authToken");
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
        }
        return await base.SendAsync(request, cancellationToken);
    }
}