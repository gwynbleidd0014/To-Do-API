using HashidsNet;
using Mapster;
using Todo.application.SubTasks.Request;
using Todo.application.SubTasks.Response;
using Todo.application.Todos.Response;
using Todo.application.Users.Request;
using Todo.application.Users.Response;
using Todo.domain.SubTasks;
using Todo.domain.Todos;
using Todo.domain.Users;

namespace Todo.api.infrastructure.Exstensions;

public static class MapsterConfiguration
{
    private  static  IHashids s_hashId;
    public static void ConfigureMapster(this IApplicationBuilder builder)
    {
        s_hashId = builder.ApplicationServices.GetRequiredService<IHashids>();
        //User Maping
        TypeAdapterConfig<UserRequestModel, User>
            .NewConfig()
            .Map(des => des.PasswordHash, src => src.Password);
        TypeAdapterConfig<UserUpdateModel, User>
            .NewConfig()
            .Map(des => des.PasswordHash, src => src.Password);
        TypeAdapterConfig<User, UserResponseModel>
            .NewConfig()
            .Map(des => des.Id, src => Encode(src.Id))
            .Map(des => des.Todos, src => src.Todos.Adapt<List<TodoResponseModel>>());
        //Todo Mapping
        TypeAdapterConfig<ToDo, TodoResponseModel>
            .NewConfig()
            .Map(des => des.Id, src => Encode(src.Id))
            .Map(des => des.OwnerId, src => Encode(src.OwnerId));


        //SubTask Mapping
        TypeAdapterConfig<SubTask, SubTaskResponseModel>
            .NewConfig()
            .Map(des => des.Id, src => Encode(src.Id))
            .Map(des => des.TodoId, src => Encode(src.ToDoId));
        TypeAdapterConfig<SubTaskRequestModel, SubTask>
            .NewConfig()
            .Map(des => des.ToDoId, src => Decode(src.TodoId));
    }

    private static int Decode(string str)
    {
        var rawId = s_hashId.Decode(str);

        if (rawId.Length == 0)
            return -1;

        return rawId[0];
    }

    private static string Encode(int id)
    {
        return s_hashId.Encode(id);
    }
}
