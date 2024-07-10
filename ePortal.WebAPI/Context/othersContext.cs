using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Entities.db_others.dts.View;

namespace PGAS.WebAPI.Context
{
    public partial class othersContext : DbContext
    {
        public othersContext(DbContextOptions<othersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<eportal_docs_viewDTO> eportal_docs_view { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<eportal_docs_viewDTO>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("eportal_docs_view");

                entity.Property(e => e.title);
                entity.Property(e => e.description);
                entity.Property(e => e.OfficeName);
                entity.Property(e => e.doctype);
                entity.Property(e => e.docdate);
                entity.Property(e => e.docid);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

