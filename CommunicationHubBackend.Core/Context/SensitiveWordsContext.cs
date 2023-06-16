using CommunicationHubBackend.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationHubBackend.Core.Context
{
    public partial class SensitiveWordsContext : DbContext
    {
        public SensitiveWordsContext() 
        { 
        }

        public virtual DbSet<TblSensitiveWords> TblSensitiveWords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:comshub.database.windows.net,1433;Initial Catalog=SensitivesWords;Persist Security Info=False;User ID=ComsAdmin;Password=Coms@Flash;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblSensitiveWords>(entity =>
            {
                entity.ToTable("tbl_sensitive_words").HasNoKey();

                entity.Property(e => e.Word)
                    .HasColumnName("word")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
