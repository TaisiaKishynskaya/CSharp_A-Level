//BFF
global using BFF.Web.Responses;
global using BFF.Web.Requests;
global using BFF.Web.Services.Abstractions;
global using BFF.Web.Services;
global using BFF.Web.Infrastructure.Configurations;
global using BFF.Web.Infrastructure.Settings;
global using BFF.Web.Infrastructure.Exceptions;
global using BFF.Web.Infrastructure.Validations;

//Common
global using Helpers;
global using Settings;
global using Helpers.Extensions;
global using Helpers.Abstractions;

//Packages
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using Newtonsoft.Json;
global using System.Security.Claims;
global using Microsoft.Extensions.Options;
global using Serilog;
global using FluentValidation;
global using FluentValidation.AspNetCore;