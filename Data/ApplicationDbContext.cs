using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using TFBackend.Models;

namespace TFBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<BBProject> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ProjectSkill> ProjectSkills { get; set; } = null!;
        public DbSet<ProjectStaff> ProjectStaff { get; set; }
        public DbSet<StaffSkills> StaffSkills { get; set; }
        public DbSet<Cert> Certs { get; set; }

        public DbSet<CertCategory> CertCategories { get; set; }

        public DbSet<StaffCert> StaffCerts { get; set; }
        public DbSet<CalendarProjectStaff> CalendarProjectStaff { get; set; }
        //configure relationships

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            
             modelBuilder.Entity<CalendarProjectStaff>()
               .HasOne(c => c.Project)
              .WithMany(p => p.CalendarProjectStaff)
             .HasForeignKey(c => c.ProjectId);

             modelBuilder.Entity<CalendarProjectStaff>()
             .HasOne(c => c.Staff)
              .WithMany(p => p.CalendarProjectStaff)
              .HasForeignKey(c => c.StaffId);


            //Project and Department - one to many
            modelBuilder.Entity<BBProject>()
                .HasOne<Department>(p => p.Department)
                .WithMany(d => d.Projects)
                .HasForeignKey(p => p.DepartmentId);

            modelBuilder.Entity<BBProject>()
                     .HasOne<Staff>(p => p.Manager)
                     .WithMany(d => d.Projects)
                     .HasForeignKey(p => p.ManagerId);


            //Project and Location - one to many
            modelBuilder.Entity<BBProject>()
                .HasOne<Location>(p => p.Location)
                .WithMany(l => l.Projects)
                .HasForeignKey(p => p.LocationId);

            //Project and Client - one to many
            modelBuilder.Entity<BBProject>()
                .HasOne<Client>(p => p.client)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.ClientId);





            //Project and Staff - many to many
            modelBuilder.Entity<ProjectStaff>()
                .HasKey(ps => new { ps.ProjectId, ps.StaffId });
             modelBuilder.Entity<CalendarProjectStaff>()
             .HasKey(psc => new { psc.ProjectId, psc.StaffId, psc.Date });
            modelBuilder.Entity<ProjectStaff>()
                .HasOne(p => p.Project)
                .WithMany(ps => ps.ProjectStaff)
                .HasForeignKey(s => s.ProjectId);
            modelBuilder.Entity<ProjectStaff>()
                .HasOne(s => s.Staff)
                .WithMany(ps => ps.ProjectStaff)
                .HasForeignKey(s => s.StaffId);

            //Project and Skills - many to many
            modelBuilder.Entity<ProjectSkill>()
                .HasKey(ps => new { ps.ProjectId, ps.SkillId });
            modelBuilder.Entity<ProjectSkill>()
                .HasOne(p => p.Project)
                .WithMany(ps => ps.ProjectSkill)
                .HasForeignKey(p => p.ProjectId);
            modelBuilder.Entity<ProjectSkill>()
                .HasOne(s => s.Skill)
                .WithMany(ps => ps.ProjectSkill)
                .HasForeignKey(s => s.SkillId);

            //Staff and Client - many to many
            modelBuilder.Entity<StaffCert>()
                .HasKey( sc => new {sc.StaffId , sc.CertId });
            modelBuilder.Entity<StaffCert>()
                .HasOne(cs => cs.Staff )
                .WithMany(cs => cs.StaffCerts)
                .HasForeignKey(cs => cs.StaffId);
            modelBuilder.Entity<StaffCert>()
                .HasOne(cs => cs.Cert)
                .WithMany(cs => cs.Staffs)
                .HasForeignKey(cs => cs.CertId);
            modelBuilder.Entity<Cert>()
               .HasOne<CertCategory>(cs => cs.CertCategory)
               .WithMany(cs => cs.Certs)
               .HasForeignKey(cs => cs.CertCategoryId);
            //Staff and Skills - many to many
            modelBuilder.Entity<StaffSkills>()
                .HasKey(ss => new { ss.StaffId, ss.SkillId });
            modelBuilder.Entity<StaffSkills>()
                .HasOne(p => p.Staff)
                .WithMany(ps => ps.StaffSkills)
                .HasForeignKey(s => s.StaffId);
            modelBuilder.Entity<StaffSkills>()
                .HasOne(s => s.Skill)
                .WithMany(ps => ps.StaffSkills)
                .HasForeignKey(s => s.SkillId);

            //Staff and Role - one to many
            modelBuilder.Entity<Staff>()
                .HasOne<Role>(s => s.Role)
                .WithMany(r => r.Staffs)
                .HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<Role>().HasData(
            new Role { Id = ((int)RoleEnum.Roles.Staff), Name = "Staff" },
            new Role { Id = ((int)RoleEnum.Roles.Manager), Name = "Manager" }
        );


        }
    }
}
