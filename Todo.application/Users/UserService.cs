using System.Globalization;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Todo.application.Abstractions;
using Todo.application.Exceptions.CustomExceptions;
using Todo.application.Exceptions.ErrorMessages;
using Todo.application.Helpers;
using Todo.application.Helpers.Hashing;
using Todo.application.Users.Request;
using Todo.application.Users.Response;
using Todo.domain.Users;


namespace Todo.application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHid _hid;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IHid hid)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _hid = hid;
    }

    public async Task AddAsync(CancellationToken token, UserRequestModel model)
    {
        var existingUser = await _userRepository.GetByUserNameAsync(token, model.UserName).ConfigureAwait(false);
        if (existingUser is not null)
        {
            throw new UserAlreadyExists(ErrorMessages.UserAlreadyExists);
        }

        await _userRepository.AddAsync(token, model.Adapt<User>()).ConfigureAwait(false);
    }


    public async Task<UserResponseModel> GetAsync(CancellationToken token, string id)
    {
        var user = await _userRepository.GetFullAsync(token, _hid.Decode(id)).ConfigureAwait(false)
            ?? throw new NotFound(ErrorMessages.UserNotFound);


        return user.Adapt<UserResponseModel>();
    }

    public async Task RemoveAsync(CancellationToken token, string id)
    {
        var result = await _userRepository.RemoveAsync(token, _hid.Decode(id)).ConfigureAwait(false);
        if (result)
            _unitOfWork.SaveChanges();

        throw new NotFound(ErrorMessages.UserNotFound);
    }

    public async Task Update(CancellationToken token, UserUpdateModel user, string id)
    {
        if (user.Password is not null)
            user.Password = Pwd.Hash(user.Password);

        await GetAsync(token, id).ConfigureAwait(false);
        var oldUser = await _userRepository.GetAsync(token, _hid.Decode(id)).ConfigureAwait(false);

        var newUser = user.Adapt<User>();
        newUser.Id = _hid.Decode(id);
        var transformedUser = Transform<User>.Copy(newUser, oldUser!);
        await _userRepository.Update(transformedUser).ConfigureAwait(false);

        _unitOfWork.SaveChanges();
    }

    public async Task<UserResponseModel> LogIn(CancellationToken token, UserRequestModel user)
    {
        var existingUser = await _userRepository.GetByUserNameAsync(token, user.UserName).ConfigureAwait(false)
            ?? throw new NotFound(ErrorMessages.UserNotFound); ;

        var isValid = true;

        var passwordMatches = Pwd.Verify(user.Password, existingUser.PasswordHash);
        isValid &= passwordMatches;

        return isValid ? existingUser.Adapt<UserResponseModel>() : throw new PasswordNotMatching(ErrorMessages.PasswordNotMatching);
    }

    public async Task Register(CancellationToken token, UserRequestModel user)
    {
        user.Password = Pwd.Hash(user.Password);
        await AddAsync(token, user).ConfigureAwait(false);

        _unitOfWork.SaveChanges();
    }
}
