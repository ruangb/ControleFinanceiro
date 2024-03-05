using AutoMapper;
using ControleFinanceiro.Application.Implementation;
using ControleFinanceiro.Application.Interfaces;
using ControleFinanceiro.CrossCutting.DTO;
using ControleFinanceiro.Data.Context;
using ControleFinanceiro.Data.Implementation;
using ControleFinanceiro.Data.Interfaces;
using ControleFinanceiro.Domain.Manager.Implementation;
using ControleFinanceiro.Domain.Manager.Interfaces;
using ControleFinanceiro.Models;
using ControleFinanceiro.WebSite.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(CreditCardDTO), typeof(CreditCard));
builder.Services.AddAutoMapper(typeof(CreditCardDTO), typeof(CreditCardViewModel));
var mapper = new MapperConfiguration(m => m.CreateMap<CreditCardDTO, CreditCardViewModel>().ReverseMap());
mapper.CreateMapper();

builder.Services.AddSingleton<IContext, Context>();
builder.Services.AddScoped<IBaseRepository<CreditCard>, CreditCardRepository>();
builder.Services.AddScoped<IBaseManager<CreditCard>, CreditCardManager>();
builder.Services.AddScoped<IBaseAppService<CreditCardDTO>, CreditCardAppService>();

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
