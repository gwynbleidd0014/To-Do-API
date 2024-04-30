using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using HashidsNet;
using Todo.api.infrastructure.Middlewares;
using Todo.application;
using Todo.application.Abstractions;
using Todo.application.Helpers;
using Todo.application.SubTasks;
using Todo.application.Todos;
using Todo.application.Users;
using Todo.infrastructure.Repositories.SubTasks;
using Todo.infrastructure.Repositories.Todos;
using Todo.infrastructure.Repositories.Users;
using Todo.infrastructure.UnitOfWork;

namespace Todo.api.infrastructure.Exstensions
{
    public static class ConfigureCustomServices
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<RequestResponseLoggerMiddleware>();
            services.AddScoped<GlobalErrorHandlerMiddleware>();

            services.AddSingleton<IHashids>(_ => new Hashids(configuration.GetValue<string>("HashIds:Salt"), configuration.GetValue<int>("HashIds:Length")));
            services.AddSingleton<IHid, Hid>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ITodoService, TodoService>();

            services.AddScoped<ITaskRepository, SubTaskRepository>();
            services.AddScoped<ITaskService, SubTaskService>();

        }

    }
}
