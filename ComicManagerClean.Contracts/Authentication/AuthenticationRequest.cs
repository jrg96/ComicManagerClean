namespace ComicManagerClean.Contracts.Authentication;

public record AuthenticationRequest(
    string Email,
    string Password
);

