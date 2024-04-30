// Copyright (C) TBC Bank. All Rights Reserved.

using FluentValidation;
using Todo.application.Users.Request;

namespace Todo.api.infrastructure.Validations.User;

public class UserUpdateValidator : AbstractValidator<UserUpdateModel>
{
    public UserUpdateValidator()
    {
        RuleFor(x => x.UserName).MaximumLength(50);
    }
}
