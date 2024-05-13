using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);


var keys = WebPush.VapidHelper.GenerateVapidKeys();
System.Diagnostics.Debug.WriteLine(keys.PrivateKey);
System.Diagnostics.Debug.WriteLine(keys.PublicKey);
//aXH4Dc9rB_1fF0iLKRNXdQUIOsALMkrlZgjBQg8tO2U
//BPodXwNKsIZ5HyRyFm6Xx4WmSeoq8zZJnjakgJUgaXbz1lvKnTfYBS5rqBpcFUnlm-1tTSE-122i5qjb4Br6Z7o

// Add services to the container.

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
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
