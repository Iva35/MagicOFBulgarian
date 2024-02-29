using MagicOFBulgarian.Data.Domain;
using MagicOFBulgarian.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicOFBulgarian.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomerUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)   : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FolkloreArea> FolkloreAreas{get; set; }
        public DbSet<GenderClothes> GenderClothes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserAddresses> UserAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Горна част", DisplayOrder = 1 },
                new Category { CategoryId = 2, CategoryName = "Долна част", DisplayOrder = 2 },
                new Category { CategoryId = 3, CategoryName = "Обувки", DisplayOrder = 3 },
                new Category { CategoryId = 4, CategoryName = "Аксесоари", DisplayOrder = 4 }
                );
            modelBuilder.Entity<FolkloreArea>().HasData(
                new FolkloreArea { AreaId = 1, AreaName = "Шопска фолклорна област", DisplayOrder = 1 },
                new FolkloreArea { AreaId = 2, AreaName = "Тракийска фолклорна област", DisplayOrder = 2 },
                new FolkloreArea { AreaId = 3, AreaName = "Родопска фолклорна област", DisplayOrder = 3 },
                new FolkloreArea { AreaId = 4, AreaName = "Пиринска фолклорна област", DisplayOrder = 4 },
                new FolkloreArea { AreaId = 5, AreaName = "Добруджанска фолклорна област", DisplayOrder = 5 },
                new FolkloreArea { AreaId = 6, AreaName = "Северняшка фолклорна област", DisplayOrder = 6 },
                new FolkloreArea { AreaId = 7, AreaName = "Странджанска фолклорна област", DisplayOrder = 7 }
                );
            modelBuilder.Entity<GenderClothes>().HasData(
                new GenderClothes { GenderId = 1, GenderName = "Мъж" },
                new GenderClothes { GenderId = 2, GenderName = "Жена" }
                );
            modelBuilder.Entity<Product>().HasData(
              new Product
              {
                  Id = 1,
                  Title = "Горна част",
                  Description = "Риза",
                  Price = 50.40,
                  Price50 = 25.20,
                  CategoryId = 1,
                  AreaId = 1,
                  GenderId = 1,
                  ImageUrl = "Picture1.jpg"
              },
              new Product 
              {
                  Id = 2,
                  Title = "Долна част",
                  Description = "Потури",
                  Price = 75.90,
                  Price50 = 37.95,
                  CategoryId = 2,
                  AreaId = 2,
                  GenderId = 1,
                  ImageUrl="Picture2.jpeg"
              },
              new Product
              {
                  Id = 3,
                  Title = "Обувки",
                  Description = "Цървули",
                  Price = 80.30,
                  Price50 = 40.15,
                  CategoryId = 3,
                  AreaId = 3,
                  GenderId = 1,
                  ImageUrl = "Picture3.jpg"
              },
              new Product
              {
                  Id = 4,
                  Title = "Аксесоари",
                  Description = "Пояс",
                  Price = 45.10,
                  Price50 = 22.55,
                  CategoryId = 4,
                  AreaId = 4,
                  GenderId = 1,
                  ImageUrl = "Picture4.jpg"
              }
              );
        }

    }
}
