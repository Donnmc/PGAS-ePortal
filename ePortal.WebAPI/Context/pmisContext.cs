using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Entities.db_pmis.pmis.Table;
using PGAS.WebAPI.Entities.db_pmis.pmis.View;

namespace Portal.WebAPI.Context
{
    public partial class pmisContext : DbContext
    {
        public pmisContext(DbContextOptions<pmisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<vw_pgas_employees> vw_pgas_employees { get; set; }
        public virtual DbSet<eportalUserDTO> eportalUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<vw_pgas_employees>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_pgas_employees");

                entity.Property(e => e.eid);
                entity.Property(e => e.SwipeID);
                entity.Property(e => e.EmployeeName);
                entity.Property(e => e.Position);
                entity.Property(e => e.OfficeAbbr);
                entity.Property(e => e.OfficeName);
                entity.Property(e => e.SG);
                entity.Property(e => e.Status);
                entity.Property(e => e.isactive);
                entity.Property(e => e.Telephone);
                entity.Property(e => e.EmailAdd);
                entity.Property(e => e.Cause);
                entity.Property(e => e.AppointCoverage);

            });

            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<eportalUserDTO>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("eportalUser");

                entity.Property(e => e.eid);
                entity.Property(e => e.SwipEmployeeID);
                entity.Property(e => e.username);
                entity.Property(e => e.passcode);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

