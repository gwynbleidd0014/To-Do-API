// Copyright (C) TBC Bank. All Rights Reserved.

using Todo.application.Exceptions.Abstractions;

namespace Todo.application.Exceptions.CustomExceptions;

public class InvalidStatus : Exception, ICustomException, IBadRequest
{
    public InvalidStatus(string msg) : base(msg) { }
}
