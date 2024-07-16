using Microsoft.EntityFrameworkCore;
using PGAS.WebAPI.Entities.portal_v2.Table;
using PGAS.WebAPI.Entities.portal_v2.View;

namespace PGAS.WebAPI.Context
{
    public partial class pgas_eportal_v2Context : DbContext
    {
        public pgas_eportal_v2Context(DbContextOptions<pgas_eportal_v2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<emergency_hotline> emergency_hotline { get; set; }
        public virtual DbSet<emergency_hotline_line> emergency_hotline_line { get; set; }
        public virtual DbSet<emergency_hotline_office> emergency_hotline_office { get; set; }
        public virtual DbSet<external_link_child> external_link_child { get; set; }
        public virtual DbSet<external_link_parent> external_link_parent { get; set; }
        public virtual DbSet<information_system> information_system { get; set; }
        public virtual DbSet<information_system_cluster> information_system_cluster { get; set; }
        public virtual DbSet<ip_phone_directory> ip_phone_directory { get; set; }
        public virtual DbSet<ip_phone_directory_area> ip_phone_directory_area { get; set; }
        public virtual DbSet<ip_phone_directory_line> ip_phone_directory_line { get; set; }
        public virtual DbSet<ip_phone_directory_office> ip_phone_directory_office { get; set; }
        public virtual DbSet<praise_message> praise_message { get; set; }
        public virtual DbSet<vw_clustered_information_system> vw_clustered_information_system { get; set; }
        public virtual DbSet<vw_emergency_hotline> vw_emergency_hotline { get; set; }
        public virtual DbSet<vw_external_link> vw_external_link { get; set; }
        public virtual DbSet<vw_ip_phone_directory> vw_ip_phone_directory { get; set; }
        public virtual DbSet<carousel_image> carousel_image { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<emergency_hotline>(entity =>
            {
                entity.Property(e => e.area)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.mobile_number)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.line).WithMany(p => p.emergency_hotline)
                    .HasForeignKey(d => d.line_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_emergency_hotline_emergency_hotline_line");

                entity.HasOne(d => d.office).WithMany(p => p.emergency_hotline)
                    .HasForeignKey(d => d.office_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_emergency_hotline_emergency_hotline_office");
            });

            modelBuilder.Entity<emergency_hotline_line>(entity =>
            {
                entity.Property(e => e.line)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<emergency_hotline_office>(entity =>
            {
                entity.Property(e => e.office)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.office_abbreviation)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<external_link_child>(entity =>
            {
                entity.HasKey(e => e.id).HasName("PK_external_links");

                entity.Property(e => e.icon)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.link).IsUnicode(false);
                entity.Property(e => e.name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.parent).WithMany(p => p.external_link_child)
                    .HasForeignKey(d => d.parent_id)
                    .HasConstraintName("FK_external_link_child_external_link_parent");
            });

            modelBuilder.Entity<external_link_parent>(entity =>
            {
                entity.HasKey(e => e.id).HasName("PK_external_link_parent1");

                entity.Property(e => e.icon)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.link).IsUnicode(false);
                entity.Property(e => e.name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<information_system>(entity =>
            {
                entity.Property(e => e.abbreviation)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.icon)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.link).IsUnicode(false);
                entity.Property(e => e.name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.platform)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.information_system_cluster).WithMany(p => p.information_system)
                    .HasForeignKey(d => d.information_system_cluster_id)
                    .HasConstraintName("FK_information_system_information_system_cluster");
            });

            modelBuilder.Entity<information_system_cluster>(entity =>
            {
                entity.HasKey(e => e.id).HasName("PK__informat__3213E83F59A53570");

                entity.Property(e => e.abbreviation)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.icon)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ip_phone_directory>(entity =>
            {
                entity.Property(e => e.line_number)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.office_area)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.area).WithMany(p => p.ip_phone_directory)
                    .HasForeignKey(d => d.area_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ip_phone_directory_ip_phone_directory_area");

                entity.HasOne(d => d.line).WithMany(p => p.ip_phone_directory)
                    .HasForeignKey(d => d.line_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ip_phone_directory_ip_phone_directory_line");

                entity.HasOne(d => d.office).WithMany(p => p.ip_phone_directory)
                    .HasForeignKey(d => d.office_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ip_phone_directory_ip_phone_directory_office");
            });

            modelBuilder.Entity<ip_phone_directory_area>(entity =>
            {
                entity.Property(e => e.area)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ip_phone_directory_line>(entity =>
            {
                entity.Property(e => e.line)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ip_phone_directory_office>(entity =>
            {
                entity.Property(e => e.office)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.office_abbreviation)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<praise_message>(entity =>
            {
                entity.HasKey(e => e.id).HasName("PK__praise_m__3213E83FCA56F543");

                entity.Property(e => e.id);
                entity.Property(e => e.date).HasColumnType("datetime");
                entity.Property(e => e.from_eid);
                entity.Property(e => e.to_eid);
                entity.Property(e => e.message).IsUnicode(false);
                entity.Property(e => e.stars);
            });

            modelBuilder.Entity<carousel_image>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("carousel_image");

                entity.Property(e => e.file_name);
                entity.Property(e => e.upload_date);
            });

            modelBuilder.Entity<vw_clustered_information_system>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_clustered_information_system");

                entity.Property(e => e.Cluster_Abbreviation)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Cluster Abbreviation");
                entity.Property(e => e.Cluster_Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Cluster Icon");
                entity.Property(e => e.Cluster_Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Cluster Name");
                entity.Property(e => e.Cluster___Is_Active_).HasColumnName("Cluster - Is Active?");
                entity.Property(e => e.Information_System_Abbreviation)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Information System Abbreviation");
                entity.Property(e => e.Information_System_Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Information System Icon");
                entity.Property(e => e.Information_System_Link)
                    .IsUnicode(false)
                    .HasColumnName("Information System Link");
                entity.Property(e => e.Information_System_Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Information System Name");
                entity.Property(e => e.Information_System_Platform)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Information System Platform");
                entity.Property(e => e.Information_System___Is_Active_).HasColumnName("Information System - Is Active?");
            });

            modelBuilder.Entity<vw_emergency_hotline>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_emergency_hotline");

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Hotline)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.Line)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.Office)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Office_Abbreviation)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Office Abbreviation");
            });

            modelBuilder.Entity<vw_external_link>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_external_link");

                entity.Property(e => e.Child_List_Date_Created).HasColumnName("Child List Date Created");
                entity.Property(e => e.Child_List_Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Child List Icon");
                entity.Property(e => e.Child_List_Link)
                    .IsUnicode(false)
                    .HasColumnName("Child List Link");
                entity.Property(e => e.Child_List_Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Child List Name");
                entity.Property(e => e.Child_List_Order).HasColumnName("Child List Order");
                entity.Property(e => e.Parent_List_Date_Created).HasColumnName("Parent List Date Created");
                entity.Property(e => e.Parent_List_Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Parent List Icon");
                entity.Property(e => e.Parent_List_Link)
                    .IsUnicode(false)
                    .HasColumnName("Parent List Link");
                entity.Property(e => e.Parent_List_Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Parent List Name");
                entity.Property(e => e.Parent_List_Order).HasColumnName("Parent List Order");
            });

            modelBuilder.Entity<vw_ip_phone_directory>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_ip_phone_directory");

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Line)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.Line_Number)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Line Number");
                entity.Property(e => e.Office)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Office_Abbreviation)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Office Abbreviation");
                entity.Property(e => e.Office_Area)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Office Area");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
