using JasperFx;
using Marten;
using Padel.App.Entities;
using Serilog;

namespace Padel.App;

public static class Services
{
    public static void AddDataBase(this IServiceCollection services, IConfiguration configuration, bool IsDevelopment)
    {

        services.AddMarten(options =>
        {
            // Establish the connection string to your Marten database
            options.Connection(configuration.GetConnectionString("DefaultConnection")!);

            // Specify that we want to use STJ as our serializer
            options.UseSystemTextJsonForSerialization();

            options.Schema.For<Player>().SoftDeleted()
            .Metadata(m =>
            {
                m.SoftDeletedAt.MapTo(x => x.DeletedAt);
                m.IsSoftDeleted.MapTo(x => x.Deleted);
                m.CreatedAt.MapTo(x => x.CreatedAt);
            });


            if (IsDevelopment)
            {
                options.AutoCreateSchemaObjects = AutoCreate.All;
            }
            else
            {
                options.AutoCreateSchemaObjects = AutoCreate.None;
            }
        });

    }

    public static void AddAppLogger(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSerilog((services, lc) => lc
            .ReadFrom.Configuration(configuration)
            .ReadFrom.Services(services));
    }

    public static void AddFeatures(this IServiceCollection services)
    {
        
    }

    public static void AddTools(this IServiceCollection services)
    {
        services.AddMcpServer()
            .WithHttpTransport()
            .WithToolsFromAssembly();

    }
}
