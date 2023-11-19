using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
//app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

//app.UseRouting();

//app.MapRazorPages();
//app.MapControllers();
//app.MapFallbackToFile("index.html");

//ctx.Request.Host.Port == 5001 ||
//ctx.Request.Path == "/"

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/FirstApp") || ctx.Request.Host.Equals("FirstApp.com"), first =>
    {
        first.Use((ctx, nxt) =>
        {
            ctx.Request.Path = "/FirstApp" + ctx.Request.Path;
            return nxt();
        });

        first.UsePathBase("/FirstApp");
        first.UseBlazorFrameworkFiles("/FirstApp");
        first.UseStaticFiles();
        first.UseStaticFiles("/FirstApp");
        first.UseRouting();

        first.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("/FirstApp/{*path:nonfile}", "FirstApp/index.html");
        });
    });
//ctx.Request.Host.Port == 5002 ||
app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/SecondApp") || ctx.Request.Host.Equals("SecondApp.com"), first =>
{
    first.Use((ctx, nxt) =>
    {
        ctx.Request.Path = "/SecondApp" + ctx.Request.Path;
        return nxt();
    });

    first.UsePathBase("/SecondApp");
    first.UseBlazorFrameworkFiles("/SecondApp");
    first.UseStaticFiles();
    first.UseStaticFiles("/SecondApp");
    first.UseRouting();

    first.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("/SecondApp/{*path:nonfile}", "SecondApp/index.html");
    });
});

app.MapGet("/api", () => "hello Api");

app.Run();
