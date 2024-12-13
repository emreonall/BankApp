using BankApp.Database.Context;
using BankApp.Database.Repositories;
using BankApp.Database.Repositories.BankRepo;
using BankApp.Database.Repositories.CompanyRepo;
using BankApp.Database.Repositories.CurrencyRepo;
using BankApp.Database.Repositories.ExchangeRateRepo;
using BankApp.Database.Repositories.ProcessTypeRepo;
using BankApp.Database.Repositories.TransactionRepo;
using BankApp.Database.Repositories.TransactionTypeRepo;
using BankApp.Database.Validators;
using BankApp.Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true).AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


builder.Services.AddDbContext<AppDbContext>();


//builder.Services.AddAutoMapper(typeof(BankApp.Database.MapProfiles).Assembly);  //
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));



builder.Services.AddScoped<IBankRepository, BankRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IProcessTypeRepository, ProcessTypeRepository>();
builder.Services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddScoped<IValidator<Currency>, CurrencyValidator>();
builder.Services.AddScoped<IValidator<Company>, CompanyValidator>();
builder.Services.AddScoped<IValidator<ProcessType>, ProcessTypeValidator>();
builder.Services.AddScoped<IValidator<TransactionType>, TransacitonTypeValidator>();
builder.Services.AddScoped<IValidator<ExchangeRate>, ExchangeRateValidator>();


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
