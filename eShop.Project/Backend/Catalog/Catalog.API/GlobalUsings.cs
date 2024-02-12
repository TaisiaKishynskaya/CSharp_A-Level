//DataAccess Layer
global using Catalog.DataAccess.Entities;
global using Catalog.DataAccess.Repositories;
global using Catalog.DataAccess.Infrastructure;

//Core Layer
global using Catalog.Core.Abstractions.Repositories;
global using Catalog.Core.Abstractions.Services;

//Domain Layer
global using Catalog.Domain.Models;

//API Layer
global using Catalog.API.Requests;
global using Catalog.API.Responses;
global using Catalog.API.Infrastructure.Exceptions;
global using Catalog.API.Infrastructure.Mapping;
global using Catalog.API.Infrastructure.Configurations;

//Aplication Layer
global using Catalog.Application.Services;
global using Catalog.Application.Infrastructure.Mapping;
global using Catalog.Application.Infrastructure.Exceptions;

//Packages
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using AutoMapper;
global using FluentValidation;
global using Newtonsoft.Json;
global using FluentValidation.AspNetCore;
global using Serilog;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.OpenApi.Models;


