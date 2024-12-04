using Microsoft.EntityFrameworkCore;
using BurguerManiaAPI.Models;

namespace BurguerManiaAPI.Context
{
    /// <summary>
    /// Gerencia as entidades e o acesso ao banco de dados.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Representações das tabelas no banco de dados
        public DbSet<UsersModel> Users { get; set; } = null!;
        public DbSet<CategorysModel> Categories { get; set; } = null!;
        public DbSet<ProductsModel> Products { get; set; } = null!;
        public DbSet<OrdersModel> Orders { get; set; } = null!;
        public DbSet<StatusModel> Status { get; set; } = null!;
        public DbSet<OrderProductsModel> OrdersProducts { get; set; } = null!;
        public DbSet<UserOrdersModel> UsersOrders { get; set; } = null!;

        /// <summary>
        /// Configura mapeamentos e relações entre as entidades.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUserEntity(modelBuilder);
            ConfigureCategoryEntity(modelBuilder);
            ConfigureProductEntity(modelBuilder);
            ConfigureOrderEntity(modelBuilder);
            ConfigureStatusEntity(modelBuilder);
            ConfigureOrderProductsEntity(modelBuilder);
            ConfigureUserOrdersEntity(modelBuilder);
        }

        // Configurações específicas para cada entidade
        private void ConfigureUserEntity(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<UsersModel>();
            entity.Property(u => u.Name).HasColumnType("VARCHAR(100)");
            entity.Property(u => u.Email).HasColumnType("VARCHAR(100)").IsRequired();
            entity.Property(u => u.Password).HasColumnType("VARCHAR(255)").IsRequired();
        }

        private void ConfigureCategoryEntity(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<CategorysModel>();
            entity.Property(c => c.Name).HasColumnType("VARCHAR(100)").IsRequired();
            entity.Property(c => c.Description).HasColumnType("TEXT");
            entity.Property(c => c.PathImage).HasColumnType("VARCHAR(255)");
        }

        private void ConfigureProductEntity(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ProductsModel>();
            entity.Property(p => p.Name).HasColumnType("VARCHAR(100)").IsRequired();
            entity.Property(p => p.Price).HasColumnType("DECIMAL(18,2)");
            entity.HasIndex(p => p.Name).IsUnique().HasDatabaseName("IX_Products_NameUnique");

            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureOrderEntity(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<OrdersModel>();
            entity.Property(o => o.Value).HasColumnType("DECIMAL(18,2)");

            entity.HasOne(o => o.Status)
                  .WithMany()
                  .HasForeignKey(o => o.StatusId)
                  .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureStatusEntity(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<StatusModel>();
            entity.Property(s => s.Name).HasColumnType("VARCHAR(50)");

            // Inserindo dados iniciais
            entity.HasData(
                new StatusModel { Id = 1, Name = "EmAndamento" },
                new StatusModel { Id = 2, Name = "Concluido" },
                new StatusModel { Id = 3, Name = "Cancelado" }
            );
        }

        private void ConfigureOrderProductsEntity(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<OrderProductsModel>();
            entity.HasOne(op => op.Product)
                  .WithMany()
                  .HasForeignKey(op => op.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(op => op.Order)
                  .WithMany()
                  .HasForeignKey(op => op.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureUserOrdersEntity(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<UserOrdersModel>();
            entity.HasOne(uo => uo.User)
                  .WithMany()
                  .HasForeignKey(uo => uo.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(uo => uo.Order)
                  .WithMany()
                  .HasForeignKey(uo => uo.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
