using Microsoft.OpenApi.Models;

namespace smartfinance.Api.Extensions
{
    public static class SwaggerExtension
    {
        private static readonly string version = "v1";
        private static readonly string title = "Smartfinance Api";
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version,
                    new OpenApiInfo
                    {
                        Title = title,
                        Version = version,
                        Description = "API de controle finaceiro.",
                    });

                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});

                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    In = ParameterLocation.Header,
                //    Description = "Insira o token",
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference= new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id ="Bearer"
                //            }
                //        },
                //            Array.Empty<string>()
                //    }
                //});

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
                //xmlPath = Path.Combine(AppContext.BaseDirectory, "CL.Core.Shared.xml");
                //c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint($"./swagger/{version}/swagger.json", $"{title} {version}");
            });
        }
    }
}
