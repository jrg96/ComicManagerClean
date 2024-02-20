﻿namespace ComicManagerClean.Contracts.Authentication;

public record SignUpRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);