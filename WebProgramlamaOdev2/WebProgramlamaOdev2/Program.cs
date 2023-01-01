using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebProgramlamaOdev2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDefaultIdentity<IdentityUser>
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login/";
            options.AccessDeniedPath = "/Account/Forbidden/";
        });
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RegisterModel",
//         policy => policy.RequireRole("Admin"));
//});
//builder.Services.AddAuthentication(RegisterModel model);
//builder.Services.AddAuthentication(RegisterModel.AuthenticationScheme)
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
//        options => builder.Configuration.Bind("JwtSettings", options))
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
//        options => builder.Configuration.Bind("CookieSettings", options));
builder.Services.ConfigureApplicationCookie(options =>
{
    //options.LoginPath = "/account/login"; //sessioný tanýma yetkigerektiðinde gidilecek alan
    //options.LogoutPath = "/account/logout"; //sessiondan çýkma
    //options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true; //sessin def olarak 20 dk bunu true yaparsak her istekte 20 dk tekrar baþlar
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.Cookie.Name = "WebProgramlama.cookie";
    //AddAuthentication(Action < AuthenticationOptions > configureOptions);

});

var app = builder.Build();
//builder.Services.AddDbContext<OdevContext>(options => options.UseNpgsql("Host=localhost;Database=WebProje;Username=postgres;Password=12345"));

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
app.UseAuthentication();

app.UseAuthorization(); 


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Urun}/{action=UrunListeleAdmin}/{id?}");

app.Run();
