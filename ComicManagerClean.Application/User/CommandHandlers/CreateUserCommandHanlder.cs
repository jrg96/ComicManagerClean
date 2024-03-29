﻿using ComicManagerClean.Application.Abstractions;
using ComicManagerClean.Application.Services.Security;
using ComicManagerClean.Application.User.Commands;
using ComicManagerClean.Domain.Repositories;
using ComicManagerClean.Domain.Repositories.Commands;
using ComicManagerClean.Domain.Repositories.Queries;
using ComicManagerClean.Domain.Shared;
using ComicManagerClean.Domain.Shared.Enums;
using Mapster;

namespace ComicManagerClean.Application.User.CommandHandlers;

public class CreateUserCommandHanlder : ICommandHandler<CreateUserCommand>
{
    private readonly IPasswordSecurityService _passwordSecurityService;
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHanlder(IUserCommandRepository userCommandRepository, IUserQueryRepository userQueryRepository, IUnitOfWork unitOfWork, IPasswordSecurityService passwordSecurityService)
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
        _unitOfWork = unitOfWork;
        _passwordSecurityService = passwordSecurityService;
    }

    public async Task<CommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.User user = await _userQueryRepository.GetUserByEmail(request.Email);

        // If user already exists, fail the command
        if (user != null)
        {
            return new CommandResult(false, new Error("CM-001", "User with that email already exists in database"));
        }

        // If everything looks good, encrypt password
        (string hashedPassword, byte[] salt) = _passwordSecurityService.EncryptPassword(request.Password);

        Domain.Entities.User newUser = request.Adapt<Domain.Entities.User>();
        newUser.Id = Guid.NewGuid();
        newUser.Password = hashedPassword;
        newUser.Salt = salt;
        newUser.Role = RolesEnum.User; // By default any new user will be a normal user

        await _userCommandRepository.Add(newUser);
        await _unitOfWork.SaveChangesAsync();
        return new CommandResult(true, Error.None);
    }
}
