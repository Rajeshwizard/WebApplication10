using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApplication10.Models.ViewModel;

namespace WebApplication10.Models
{
    public partial class ChinaDBContext : DbContext
    {
        public ChinaDBContext()
        {
        }

        public ChinaDBContext(DbContextOptions<ChinaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LoginUsers> LoginUsers { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=WINDOWS-2UND25Q\\MSSQLSERVER01;Database=ChinaDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginUsers>(entity =>
            {
                entity.HasKey(e => e.UserID)
                    .HasName("PK__LoginUse__1788CCAC4C9717F6");

                entity.Property(e => e.UserID).HasColumnName("UserID");

               

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });


            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
