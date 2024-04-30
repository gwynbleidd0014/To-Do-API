// Copyright (C) TBC Bank. All Rights Reserved.

using Todo.application.Exceptions.Abstractions;

namespace Todo.application.Exceptions.CustomExceptions;

public class PasswordNotMatching : Exception, ICustomException, IBadRequest
{
    public PasswordNotMatching(string msg) : base(msg) { }
}
