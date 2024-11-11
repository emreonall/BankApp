using BankApp.Database.Context;
using BankApp.Database.Repositories;
using BankApp.Database.Repositories.BankRepo;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<AppDbContext>(options=> 
// options.UseSqlServer("name=ConnectionStrings:SqlConn"));

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());  //AppDomain.CurrentDomain.GetAssemblies()

builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddScoped<IBankRepository,BankRepository>();

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

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
