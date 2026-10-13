using Microsoft.EntityFrameworkCore;
using SelfOrderSystem.Models;

namespace SelfOrderSystem.Data {
    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {
        }

        // CategoriesテーブルをProduct型として問い合わせるためのプロパティ
        public DbSet<Category> Categories => Set<Category>();

        // ProductsテーブルをProduct型として問い合わせるためのプロパティ
        public DbSet<Category> Products => Set<Category>();


    }
}
