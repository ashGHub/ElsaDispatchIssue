using Elsa.EntityFrameworkCore.Extensions;
using Elsa.EntityFrameworkCore.Modules.Management;
using Elsa.EntityFrameworkCore.Modules.Runtime;
using Elsa.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.WebHost.UseStaticWebAssets();

const string sqliteConnectionString = "Data Source=App_Data/elsa.db";

services.AddElsa(elsa =>
{
    elsa.UseWorkflowManagement(mgt =>
        mgt.UseEntityFrameworkCore(ef => ef.UseSqlite(sqliteConnectionString))
    );
    elsa.UseWorkflowRuntime(rt =>
        rt.UseEntityFrameworkCore(ef => ef.UseSqlite(sqliteConnectionString))
    );
    elsa.UseWorkflowsApi();
    elsa.UseHttp(http =>
    {
        http.ConfigureHttpOptions = options =>
            builder.Configuration.GetSection("Http").Bind(options);
    });
    elsa.UseIdentity(idt =>
    {
        idt.UseAdminUserProvider();
        idt.TokenOptions = options =>
        {
            options.SigningKey = "super-secret-tamper-free-token-signing-key";
            options.AccessTokenLifetime = TimeSpan.FromDays(1);
        };
    });
    elsa.UseDefaultAuthentication(auth => auth.UseAdminApiKey());
    elsa.AddWorkflowsFrom<Program>();
    elsa.AddActivitiesFrom<Program>();
});

services.AddCors();
services.AddRazorPages();

var app = builder.Build();

app.UseRouting();
app.UseCors();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseBlazorFrameworkFiles();
app.UseWorkflowsApi();
app.UseWorkflows();
app.MapFallbackToPage("/_Host");
app.MapControllers();

await app.RunAsync();
