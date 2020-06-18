using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data.Entity;
using RealEstate.Data.Enum;
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
            modelBuilder.Entity<Direction>().HasData(
                new Direction() { Id = 1, DirectionName = "Đông" },
                new Direction() { Id = 2, DirectionName = "Tây" },
                new Direction() { Id = 3, DirectionName = "Nam" },
                new Direction() { Id = 4, DirectionName = "Bắc" },
                new Direction() { Id = 5, DirectionName = "Đông Bắc" },
                new Direction() { Id = 6, DirectionName = "Tây Bắc" },
                new Direction() { Id = 7, DirectionName = "Đông Nam" },
                new Direction() { Id = 8, DirectionName = "Tây Nam" }
                );
            modelBuilder.Entity<EvaluationStatus>().HasData(
                new EvaluationStatus() { Id = 1, EvaluationStatusName = "Đã Thẩm Định" },
                new EvaluationStatus() { Id = 2, EvaluationStatusName = "Chưa Thẩm Định" }
                );
            modelBuilder.Entity<LegalPaper>().HasData(
                new LegalPaper() { Id = 1, TypeOfLegalPapers = "Sổ Hồng" },
                new LegalPaper() { Id = 2, TypeOfLegalPapers = "Sổ Đỏ" },
                new LegalPaper() { Id = 3, TypeOfLegalPapers = "Giấy Tay" },
                new LegalPaper() { Id = 4, TypeOfLegalPapers = "Giấy Tờ Hợp Lệ" },
                new LegalPaper() { Id = 5, TypeOfLegalPapers = "Đang Hợp Thức Hóa" },
                new LegalPaper() { Id = 6, TypeOfLegalPapers = "Chủ Quyền Tư Nhân" },
                new LegalPaper() { Id = 7, TypeOfLegalPapers = "Hợp Đồng" }
                );
            modelBuilder.Entity<TypeOfProperty>().HasData(
                new TypeOfProperty() { Id = 1, TypeOfPropertyName = "Chung Cư/ Căn Hộ" },
                new TypeOfProperty() { Id = 2, TypeOfPropertyName = "Nhà Riêng" },
                new TypeOfProperty() { Id = 3, TypeOfPropertyName = "Đất Nền" }
                );
            var roleId = new Guid("DA131B40-8F65-4040-B257-AA9A2EF7BE3E");
            var adminId = new Guid("6B51BD7E-D928-409F-8404-C535BD73D04C");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin123"),
                SecurityStamp = string.Empty,
                FullName = "Tôn Võ Thủy Tiên",
                Address = "85/4 Nguyễn Thế Truyện, phường Tân Sơn Nhì, quận Tân Phú",
                Gender = Gender.Female,
                IdentityNumber = "0123456789"
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
