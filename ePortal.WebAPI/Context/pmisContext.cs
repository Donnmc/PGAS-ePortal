using ePortal.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Context;

public partial class pmisContext : DbContext
{
    public pmisContext(DbContextOptions<pmisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<m_vwGetAllEmployee_Minified> m_vwGetAllEmployee_Minified { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<m_vwGetAllEmployee_Minified>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("m_vwGetAllEmployee_Minified");

            entity.Property(e => e.eid);
            entity.Property(e => e.SwipeId);
            entity.Property(e => e.EmpName);
            entity.Property(e => e.EmpFullName);
            entity.Property(e => e.Department);
            entity.Property(e => e.OfficeAbbr);
            entity.Property(e => e.OfficeName);
            entity.Property(e => e.Position);
            entity.Property(e => e.SG);
            entity.Property(e => e.employmentstatus_id);
            entity.Property(e => e.Status);
            entity.Property(e => e.isShifty);
            entity.Property(e => e.isactive);

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}