using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using MyApplication.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
// Have changed the above line to below
// builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => {options.SignIn.RequireConfirmedAccount = false;options.User.RequireUniqueEmail=true;options.SignIn.RequireConfirmedEmail=false;}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// builder.Services.AddSingleton<IEmailSender,EmailSender>();
// builder.Services.AddScoped<IUserClaimPrincipalFactory<IdentityUser>,AdditionalUserClaimsPrincipalFactory();
// builder.Services.Add
// // Add services to the container.
// builder.Services.AddAuthorization(options =>
//     options.AddPolicy("TwoFactorEnabled", x => x.RequireClaim("amr", "mfa")));

// builder.Services.AddControllersWithViews();
// builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
