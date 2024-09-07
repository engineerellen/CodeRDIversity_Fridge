using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Repository.Context;
using Repository.Interface;
using Repository;
using Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Services.DIP;

    public class Init
    {
        public static void Configure(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<GeladeiraContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemService, ItemService>();
        }
    }