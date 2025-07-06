using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aok_s.Areas.Identity.Data;

public class aok_sIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public aok_sIdentityDbContext(DbContextOptions<aok_sIdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<Semester> Semesters { get; set; } = default!;
    public DbSet<Department> Departments { get; set; } = default!;
    public DbSet<Major> Majors { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Department>()
            .HasOne(d => d.Semester)
            .WithMany(s => s.Departments)
            .HasForeignKey(d => d.SemesterId);

        builder.Entity<Major>()
            .HasOne(m => m.Department)
            .WithMany(d => d.Majors)
            .HasForeignKey(m => m.DepartmentId);

        builder.Entity<ClassMajor>()
            .HasKey(cm => new { cm.ClassId, cm.MajorId });

        builder.Entity<ClassMajor>()
            .HasOne(cm => cm.Class)
            .WithMany(c => c.ClassMajors)
            .HasForeignKey(cm => cm.ClassId);

        builder.Entity<ClassMajor>()
            .HasOne(cm => cm.Major)
            .WithMany(m => m.ClassMajors)
            .HasForeignKey(cm => cm.MajorId);
    }
}
