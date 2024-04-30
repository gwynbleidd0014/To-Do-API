// Copyright (C) TBC Bank. All Rights Reserved.

using FluentValidation;
using Todo.application.Todos.Request;

namespace Todo.api.infrastructure.Validations.Todo;

public class TodoRequestValidator : AbstractValidator<TodoRequestModel>
{
    public TodoRequestValidator()
    {
        RuleFor(x => x.Title).MaximumLength(100).NotEmpty();
    }
}
