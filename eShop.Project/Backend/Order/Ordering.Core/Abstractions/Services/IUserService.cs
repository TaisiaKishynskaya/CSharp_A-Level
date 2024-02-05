﻿using Ordering.Domain.Models;
using System.Security.Claims;

namespace Ordering.Core.Abstractions.Services;

public interface IUserService
{
    Task<IEnumerable<User>> Get(int page, int size);
    Task<User> GetUserById(string userId);
    string GetActiveUserId(ClaimsPrincipal userClaims);
    Task<User> Add(ClaimsPrincipal userClaims);
}