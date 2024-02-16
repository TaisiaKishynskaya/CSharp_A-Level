//MVC
global using WebApp.Models;
global using WebApp.Requests;
global using WebApp.Responses;
global using WebApp.Services.Abstractions;
global using WebApp.Services;
global using WebApp.Infrastructure.Configurations;
global using WebApp.Infrastructure.Settings;
global using WebApp.Infrastructure.Exceptions;

//Common
global using Helpers;
global using Settings;
global using Helpers.Abstractions;

//Packages
global using Microsoft.AspNetCore.Mvc;
global using System.Diagnostics;
global using Microsoft.AspNetCore.Authentication.Cookies;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authorization;
global using IdentityModel.Client;
global using Newtonsoft.Json;
global using System.Security.Claims;
global using Microsoft.Extensions.Options;
global using Serilog;