using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Contact = Swashbuckle.AspNetCore.Swagger.Contact;

namespace SPEAR.Configuration.StartupConfiguration
{
    public static class SwaggerConfiguration
    {
        private const string RootNamespace = "SPEAR.Configuration.StartupConfiguration";
        private const string Title = "SPEAR API";
        private const string Description =
            "SPEAR API endpoints for private consuption";
        private const string AdminName = "System Administrator";
        private const string AdminUrl = "https://truittluke.com";
        private const string AdminEmail = "dukeh4d@gmail.com";
        private const string LicenseTitle = "SPEAR MC";
        private const string LicenseUrl = "http://www.provalens.com/licenseagreement";
        private const string Version10 = "v1.0";
        private const string Version20 = "v2.0";

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Version20, new Info { Title = Title, Version = Version20, Contact = GetContact(), License = GetLicense(), Description = Description });
                c.SwaggerDoc(Version10, new Info { Title = Title, Version = Version10, Contact = GetContact(), License = GetLicense(), Description = Description });
                c.IgnoreObsoleteActions();

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                    // would mean this action is unversioned and should be included everywhere
                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }

                    //added for removing the {version} parameter from swagger
                    var values = apiDesc.RelativePath.Split('/').Skip(1);
                    if (docName != null && docName.Contains("v1.0"))
                    {
                        apiDesc.RelativePath = docName.Replace(".0", "").Replace(".", "") + "/" + string.Join("/", values); //just for verion 1.0, going to change to v1
                    }
                    else if (docName != null && docName.Contains("v2.0"))
                    {
                        apiDesc.RelativePath = docName.Replace(".0", "").Replace(".", "") + "/" + string.Join("/", values);
                    }
                    else
                    {
                        //apiDesc.RelativePath = docName?.Replace(".", "") + "/" + string.Join("/", values);
                        apiDesc.RelativePath = docName + "/" + string.Join("/", values);
                    }

                    var versionParameter = apiDesc.ParameterDescriptions
                        .SingleOrDefault(p => p.Name == "version");

                    if (versionParameter != null)
                        apiDesc.ParameterDescriptions.Remove(versionParameter);


                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName);
                    }
                    return actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == docName);
                });
                c.OperationFilter<ApiVersionOperationFilter>();
            });

            return services;
        }

        public static IApplicationBuilder RegisterSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/" + Version20 + "/swagger.json", Version20 + " - " + Title);
                c.SwaggerEndpoint("/swagger/" + Version10 + "/swagger.json", Version10 + " - " + Title);
                c.RoutePrefix = "help";
                c.DocumentTitle = Title + " Documentation";
            });
            return app;
        }

        private static Contact GetContact()
        {
            return new Contact
            {
                Email = AdminEmail,
                Name = AdminName,
                Url = AdminUrl
            };
        }

        private static License GetLicense()
        {
            return new License
            {
                Name = LicenseTitle,
                Url = LicenseUrl
            };
        }

    }

    /// <exclude />
    public static class ActionDescriptorExtensions
    {
        /// <exclude />
        public static ApiVersionModel GetApiVersion(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor?.Properties
                .Where((kvp) => ((Type)kvp.Key).Equals(typeof(ApiVersionModel)))
                .Select(kvp => kvp.Value as ApiVersionModel).FirstOrDefault();
        }
    }

    public class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var actionApiVersionModel = context.ApiDescription.ActionDescriptor?.GetApiVersion();
            if (actionApiVersionModel == null)
            {
                return;
            }

            if (actionApiVersionModel.DeclaredApiVersions.Any())
            {
                operation.Produces = operation.Produces
                    .SelectMany(p => actionApiVersionModel.DeclaredApiVersions
                        .Select(version => $"{p};v={version.ToString()}")).ToList();
            }
            else
            {
                operation.Produces = operation.Produces
                    .SelectMany(p => actionApiVersionModel.ImplementedApiVersions.OrderByDescending(v => v)
                        .Select(version => $"{p};v={version.ToString()}")).ToList();
            }
        }
    }

}
