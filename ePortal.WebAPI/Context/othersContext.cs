using ePortal.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ePortal.WebAPI.Context;

public partial class othersContext : DbContext
{
    public othersContext(DbContextOptions<othersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<t_doc_office_vw> t_doc_public_vw { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<t_doc_office_vw>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("t_doc_office_vw");

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
