using ePortal.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Context;

public partial class pmisContext : DbContext
{
    public pmisContext(DbContextOptions<pmisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ePortal_employee> ePortal_employee { get; set; }
    public virtual DbSet<eportalUser> eportalUser { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ePortal_employee>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ePortal_employee");

            entity.Property(e => e.eid);
            entity.Property(e => e.SwipeID);
            entity.Property(e => e.EmployeeName);
            entity.Property(e => e.Position);
            entity.Property(e => e.OfficeAbbr);
            entity.Property(e => e.OfficeName);
            entity.Property(e => e.SG);
            entity.Property(e => e.Status);
            entity.Property(e => e.isactive);

        });

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<eportalUser>(entity =>
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