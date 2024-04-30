// Copyright (C) TBC Bank. All Rights Reserved.

using Todo.application.Exceptions.Abstractions;

namespace Todo.application.Exceptions.CustomExceptions;

public class UserAlreadyExists : Exception, ICustomException, IConflict
{
    public UserAlreadyExists(string msg) : base(msg) { }
}
