﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Mappings;
using smartfinance.Domain.Validators;

namespace smartfinance.Domain
{
    public static class Startup
    {
        public static void AddDomain(this IServiceCollection services)
        {
            #region Validators

            services.AddScoped<IValidator<Account>, AccountValidator>();
            services.AddScoped<IValidator<Category>, CategoryValidator>();
            services.AddScoped<IValidator<Movement>, MovementValidator>();

            #endregion

            #region AutoMappers

            services.AddAutoMapper(typeof(AccountMappingProfile));
            services.AddAutoMapper(typeof(CategoryMappingProfile));
            services.AddAutoMapper(typeof(MovementMappingProfile));

            #endregion

        }
    }
}
