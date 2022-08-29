using AlwaysForum.Extensions;
using AlwaysForum.Hubs;
using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddDbContext<ForumDbContext>(config =>
    config.UseSqlServer(builder.Configuration.GetConnectionString("ForumDatabase")));
builder.Services.AddIdentity();
builder.Services.AddServices();
builder.Services.AddSignalR();

var app = builder.Build();

if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.SeedDatabase();

app.MapHub<ChatHub>("/chathub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
