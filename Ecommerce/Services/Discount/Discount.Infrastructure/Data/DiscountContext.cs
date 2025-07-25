using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Infrastructure.Data
{
    public class DiscountContext : IDiscountContext
    {
        public DiscountContext(IConfiguration configuration)
        {
            //This requires Configuration.Binder nuget package to build
            var client = new NpgsqlConnection (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));


        }

    }
}
