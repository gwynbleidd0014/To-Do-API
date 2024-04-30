// Copyright (C) TBC Bank. All Rights Reserved.

using FluentValidation;
using Todo.application.SubTasks.Request;

namespace Todo.api.infrastructure.Validations.SubTask;

public class TaskRequestValidator : AbstractValidator<SubTaskRequestModel>
{
    public TaskRequestValidator()
    {
        RuleFor(x => x.Title).MaximumLength(100).NotEmpty();
        RuleFor(x => x.TodoId).NotEmpty();
    }
}
