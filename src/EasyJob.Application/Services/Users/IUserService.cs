﻿using EasyJob.Application.DataTransferObjects;
using EasyJob.Application.Models;
using EasyJob.Domain.Entities.Users;

namespace EasyJob.Application.Services.Users;

public interface IUserService
{
    ValueTask<UserDto> CreateUserAsync(UserForCreationDto userForCreationDto);
    IQueryable<UserDto> RetrieveUsers(QueryParameter queryParameter);
    ValueTask<UserDto> RetrieveUserByIdAsync(Guid userId);
    ValueTask<UserDto> ModifyUserAsync(UserForModificationDto userForModificationDto);
    ValueTask<UserDto> RemoveUserAsync(Guid userId);
}