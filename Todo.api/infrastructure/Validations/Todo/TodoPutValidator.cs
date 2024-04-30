// Copyright (C) TBC Bank. All Rights Reserved.

using FluentValidation;
using Todo.application.Todos.Request;

public class TodoPutValidator : AbstractValidator<TodoPutModel>
{
    public TodoPutValidator()
    {
        RuleFor(x => x.Title).MaximumLength(100).NotEmpty();
        RuleFor(x => x.ComplitionDate).NotEmpty();
    }
}
