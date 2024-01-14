global using Catalog.API.Data.Entities;
global using Catalog.API.Data;
global using Catalog.API.Data.EntityConfigurations;
global using Catalog.API.Repositories;
global using Catalog.API.Repositories.Interfaces;
global using Catalog.API.Services.Interfaces;
global using Catalog.API.Models;
global using Catalog.API.Services;

global using System.ComponentModel;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.IdentityModel.Tokens;
global using Swashbuckle.AspNetCore.Filters;