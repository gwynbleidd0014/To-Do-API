// Copyright (C) TBC Bank. All Rights Reserved.

using FluentValidation;
using Todo.application.Users.Request;

namespace Todo.api.infrastructure.Validations.User;

public class UserRequestValidator : AbstractValidator<UserRequestModel>
{
    public UserRequestValidator()
    {
        RuleFor(x => x.UserName).MaximumLength(50).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
