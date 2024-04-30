using Microsoft.EntityFrameworkCore;
using Todo.application.Users;
using Todo.infrastructure.Repositories.Users;
using Todo.persistance.Context;
using Todo.persistance;
using HashidsNet;
using Todo.application.Abstractions;
using Todo.application.Helpers;
using Todo.application.SubTasks;
using Todo.application.Todos;
using Todo.application;
using Todo.infrastructure.Repositories.SubTasks;
using Todo.infrastructure.Repositories.Todos;
using Todo.infrastructure.UnitOfWork;
using Mapster;
using Todo.application.Users.Request;
using Todo.domain.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<TodoContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ConnectionStrings.DefaultConnection))));

builder.Services.AddSingleton<IHashids>(_ => new Hashids(builder.Configuration.GetValue<string>("HashIds:Salt"), builder.Configuration.GetValue<int>("HashIds:Length")));
builder.Services.AddSingleton<IHid, Hid>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
TypeAdapterConfig<UserRequestModel, User>
    .NewConfig()
    .Map(des => des.PasswordHash, src => src.Password);

builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddScoped<ITaskRepository, SubTaskRepository>();
builder.Services.AddScoped<ITaskService, SubTaskService>();


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
