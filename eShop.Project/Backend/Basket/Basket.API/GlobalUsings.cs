//Core Layer
global using Basket.Core.Abstractions;

//Application Layer
global using Basket.Application.Services;

//API Layer
global using Basket.API.Requests;
global using Basket.API.Responses;
global using Basket.API.Infrastructure.Configurations;
global using Basket.API.Infrastructure.Exceptions;

//Common
global using Settings;
global using Helpers;
global using Helpers.Extensions;

//Packages
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using StackExchange.Redis;
global using System.Security.Claims;
global using FluentValidation.AspNetCore;
global using FluentValidation;
global using Serilog;
global using Newtonsoft.Json;

