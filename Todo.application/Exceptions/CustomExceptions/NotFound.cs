// Copyright (C) TBC Bank. All Rights Reserved.

using Todo.application.Exceptions.Abstractions;

namespace Todo.application.Exceptions.CustomExceptions;

public class NotFound : Exception, ICustomException, INotFound
{
    public NotFound(string msg) : base(msg) { }
}
