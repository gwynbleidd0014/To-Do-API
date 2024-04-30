// Copyright (C) TBC Bank. All Rights Reserved.

using Todo.application.Exceptions.Abstractions;

namespace Todo.application.Exceptions.CustomExceptions;

public class EmptyNotAllowed : Exception, ICustomException, IBadRequest
{
    public EmptyNotAllowed(string msg) : base(msg) { }
}
