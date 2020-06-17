using Microsoft.EntityFrameworkCore;
using RealEstate.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeOfTransaction>().HasData(
                new TypeOfTransaction() { Id = 1, TypeOfTransactionName = "Nhà Đất Bán" },
                new TypeOfTransaction() { Id = 2, TypeOfTransactionName = "Nhà Đất Cho Thuê" }
                );
            modelBuilder.Entity<TypeOfProperty>().HasData(
                new TypeOfProperty() { Id = 1, TypeOfPropertyName = "Chung Cư/ Căn Hộ" },
                new TypeOfProperty() { Id = 2, TypeOfPropertyName = "Nhà Riêng" },
                new TypeOfProperty() { Id = 3, TypeOfPropertyName = "Đất Nền" }
                );
        }
    }
}
