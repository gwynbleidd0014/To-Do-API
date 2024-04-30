// Copyright (C) TBC Bank. All Rights Reserved.

using FluentValidation;
using Todo.application.SubTasks.Request;

public class TaskPutValidator : AbstractValidator<SubTaskPutModel>
{
    public TaskPutValidator()
    {
        RuleFor(x => x.Title).MaximumLength(100).NotEmpty();
    }
}
