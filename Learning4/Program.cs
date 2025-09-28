using Learning4.data;
using Learning4.Filters;
using Learning4.Services.Account;
using Learning4.Services.Coupons;
using Learning4.Services.Leaves;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'Learning4Context' not found.")));

builder.Services.AddDbContextFactory<CouponDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CouponConnection") ?? throw new InvalidOperationException("Connection string 'Learning4Context' not found."),
    sqlOptions =>
    {
        sqlOptions.CommandTimeout(60); // Set timeout to 60 seconds
    }));
builder.Services.AddDbContext<LeavesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CouponConnection") ?? throw new InvalidOperationException("Connection string 'Learning4Context' not found."),
    sqlOptions =>
    {
        sqlOptions.CommandTimeout(60); // Set timeout to 60 seconds
    })
);



builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<CouponCatchFilter>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

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
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
