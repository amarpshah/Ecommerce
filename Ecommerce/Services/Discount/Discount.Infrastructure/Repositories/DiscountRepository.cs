using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration configuration;

        public DiscountRepository(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }


        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            await using var con = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await con.ExecuteAsync("INSERT INTO COUPON (ProductName, Description, Amount) VALUES(@prodName, @prodDesc, @amt) ", 
                new { prodName = coupon.ProductName, prodDesc = coupon.Description, amt = coupon.Amount});

            if (affected == 0)
                return false;
            else
                return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            await using var con = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await con.ExecuteAsync("Delete FROM COUPON WHERE ProductName =@prodName ",
                new { prodName = productName});

            if (affected == 0)
                return false;
            else
                return true;

        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            await using var con = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await con.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM COUPON Where ProductName = @productName", new { ProductName = productName });
            
            if (coupon == null)
            {
                return  new Coupon { Amount=0, ProductName="No Discount", Description="No Discount Available" };
            }

            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var con = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await con.ExecuteAsync("UPDATE COUPON SET ProductName =@prodName , Description = @prodDesc, Amount = @amt WHERE Id = @id",
                new { prodName = coupon.ProductName, prodDesc = coupon.Description, amt = coupon.Amount , id = coupon.Id});

            if (affected == 0)
                return false;
            else
                return true;
        }
    }
}
