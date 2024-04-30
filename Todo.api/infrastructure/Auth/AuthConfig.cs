// Copyright (C) TBC Bank. All Rights Reserved.

namespace Todo.api.infrastructure.Auth;

public class AuthConfig
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audiance { get; set; }
    public int ExpInMinutes { get; set; }
}
