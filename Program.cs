using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using gmail.Data;
using gmail.Areas.Identity.Data;
using Microsoft.Extensions.Options;
using gmail.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("gmailDbContextConnection") ?? throw new InvalidOperationException("Connection string 'gmailDbContextConnection' not found.");

builder.Services.AddDbContext<gmailDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<gmailUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<gmailDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<EmailSender>();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    //The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
