using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ITMCollegeAPI.Models
{
    public partial class ITMCollegeContext : DbContext
    {
        public ITMCollegeContext()
        {
        }

        public ITMCollegeContext(DbContextOptions<ITMCollegeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Admission> Admissions { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<Faculty> Facultys { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Field> Fields { get; set; }
        public virtual DbSet<OpSubject> OpSubjects { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<SpeSubject> SpeSubjects { get; set; }
        public virtual DbSet<Stream> Streams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=DESKTOP-PJOFLNP\\SQLEXPRESS;database=ITMCollege;uid=sa;pwd=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Admission>(entity =>
            {
                entity.HasIndex(e => e.RegNum, "UQ__Admissio__34C6A0A63676178A")
                    .IsUnique();

                entity.Property(e => e.AdmissionId).HasColumnName("AdmissionID");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExCenter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExClass)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ExEnrollNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ExField)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExMarks).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ExOutOfDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ExStream)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExUniversity)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MotherName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PerAddress)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RegNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ResAddress)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Sport)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StreamId).HasColumnName("StreamID");

                //entity.HasOne(d => d.Field)
                //    .WithMany(p => p.Admissions)
                //    .HasForeignKey(d => d.FieldId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__Admission__Field__46E78A0C");

                //entity.HasOne(d => d.Stream)
                //    .WithMany(p => p.Admissions)
                //    .HasForeignKey(d => d.StreamId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__Admission__Strea__45F365D3");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.StreamId).HasColumnName("StreamID");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Courses__FieldID__3D5E1FD2");

                //entity.HasOne(d => d.Stream)
                //    .WithMany(p => p.Courses)
                //    .HasForeignKey(d => d.StreamId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__Courses__StreamI__3C69FB99");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepId)
                    .HasName("PK__Departme__DB9CAA7FE7935230");

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.Property(e => e.DepName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Image).IsUnicode(false);
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

                entity.Property(e => e.FacilityName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.Property(e => e.FacultyId).HasColumnName("FacultyID");

                entity.Property(e => e.Degree)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DepId).HasColumnName("DepID");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.FalcultyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.Faculties)
                    .HasForeignKey(d => d.DepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Facultys__DepID__5070F446");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Field>(entity =>
            {
                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StreamId).HasColumnName("StreamID");

                entity.HasOne(d => d.Stream)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.StreamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Fields__StreamID__398D8EEE");
            });

            modelBuilder.Entity<OpSubject>(entity =>
            {
                entity.HasKey(e => e.SubjectId)
                    .HasName("PK__OpSubjec__AC1BA388D4B0D430");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");

                entity.Property(e => e.EmergencyAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyPhone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.OpSubjectId).HasColumnName("OpSubjectID");

                entity.Property(e => e.RegNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SpeSubjectId).HasColumnName("SpeSubjectID");

                entity.HasOne(d => d.OpSubject)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.OpSubjectId)
                    .HasConstraintName("FK__Registrat__OpSub__4BAC3F29");

                //entity.HasOne(d => d.RegNumNavigation)
                //    .WithMany(p => p.Registrations)
                //    .HasPrincipalKey(p => p.RegNum)
                //    .HasForeignKey(d => d.RegNum)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__Registrat__RegNu__49C3F6B7");

                entity.HasOne(d => d.SpeSubject)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.SpeSubjectId)
                    .HasConstraintName("FK__Registrat__SpeSu__4AB81AF0");
            });

            modelBuilder.Entity<SpeSubject>(entity =>
            {
                entity.HasKey(e => e.SubjectId)
                    .HasName("PK__SpeSubje__AC1BA3886B80C675");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.SpeSubjects)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SpeSubjec__Field__403A8C7D");
            });

            modelBuilder.Entity<Stream>(entity =>
            {
                entity.Property(e => e.StreamId).HasColumnName("StreamID");

                entity.Property(e => e.StreamName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
