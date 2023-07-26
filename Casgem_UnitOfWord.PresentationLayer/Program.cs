using Casgem_UnitOfWord.EntityLayer.Concrete;
using Casgem_UnitOfWork.BusinessLayer.Abstract;
using Casgem_UnitOfWork.BusinessLayer.Concrete;
using Casgem_UnitOfWork.DataAccessLayer.Abstract;
using Casgem_UnitOfWork.DataAccessLayer.EntityFramework;
using Casgem_UnitOfWork.DataAccessLayer.UnitOfWork.Abstract;
using Casgem_UnitOfWork.DataAccessLayer.UnitOfWork.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<Context>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerDal, EfCustomerDal>();
builder.Services.AddScoped<ICustomerService, CustomerManager>();

builder.Services.AddScoped<ICustomerProcessDal, EfCustomerProcessDal>();
builder.Services.AddScoped<ICustomerProcessService, CustomerProcessManager>();

builder.Services.AddScoped<IUnitOfWorkDal, UnitOfWorkDal>();

builder.Services.AddControllersWithViews();

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
