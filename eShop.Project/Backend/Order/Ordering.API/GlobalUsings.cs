//DataAccess Layer
global using Ordering.DataAccess.Entities;
global using Ordering.DataAccess.Infrastructure;
global using Ordering.DataAccess.Repositories;

//Domain Layer
global using Ordering.Domain.Models;

//Core Layer
global using Ordering.Core.Abstractions.Repositories;
global using Ordering.Core.Abstractions.Services;

//Application Layer
global using Ordering.Application.Services;
global using Ordering.Application.Infrastructure.Settings;
global using Ordering.Application.Infrastructure.Exceptions;

//API Layer
global using Ordering.API.Requests;
global using Ordering.API.Infrastructure.Configurations;
global using Ordering.API.Infrastructure.Exceptions;
global using Ordering.API.Infrastructure.Validations;

//Common
global using Helpers;
global using Helpers.Extensions;
global using Helpers.Abstractions;

//Packages
global using Microsoft.EntityFrameworkCore.Migrations;
global using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using AutoMapper;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Serilog;
global using Newtonsoft.Json;