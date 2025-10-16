using System;
using System.Collections.Generic;
using LegislationMigration.Models.NewEntities;
using Microsoft.EntityFrameworkCore;

namespace LegislationMigration.Data;

public partial class NewDbContext : DbContext
{
    public NewDbContext()
    {
    }

    public NewDbContext(DbContextOptions<NewDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<IssuingAuthorityType> IssuingAuthorityTypes { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Legislation> Legislations { get; set; }

    public virtual DbSet<LegislationStatusType> LegislationStatusTypes { get; set; }

    public virtual DbSet<LegislationType> LegislationTypes { get; set; }

    public virtual DbSet<Source> Sources { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-OON0NBG\\SQLEXPRESS;Initial Catalog=New_TEC.Datalake.PreProduction;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__Articles");

            entity.Property(e => e.ArticleId).HasColumnName("Article_Id");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Aisummary)
                .HasMaxLength(1500)
                .HasColumnName("AISummary");
            entity.Property(e => e.ArticleBody).HasColumnName("Article_Body");
            entity.Property(e => e.ArticleNumberText).HasColumnName("Article_Number_Text");
            entity.Property(e => e.ArticleTitle)
                .HasMaxLength(255)
                .HasColumnName("Article_Title");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.HasRelatedArticles).HasColumnName("Has_Related_Articles");
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.OldArticleBody).HasColumnName("Old_Article_Body");

            entity.HasOne(d => d.Legislation).WithMany(p => p.Articles)
                .HasForeignKey(d => d.LegislationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Articles_Legislations");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CategoryNameAr)
                .HasMaxLength(250)
                .HasColumnName("Category_Name_Ar");
            entity.Property(e => e.CategoryNameEn)
                .HasMaxLength(250)
                .HasColumnName("Category_Name_En");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValue("CSP")
                .HasColumnName("Created_by");
            entity.Property(e => e.DisplayNameAr)
                .HasMaxLength(10)
                .HasColumnName("DisplayName_AR");
            entity.Property(e => e.DisplayNameEn)
                .HasMaxLength(10)
                .HasColumnName("DisplayName_En");
            entity.Property(e => e.LanguageId)
                .HasDefaultValue(2)
                .HasColumnName("Language_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_at");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_by");
            entity.Property(e => e.ParentId).HasColumnName("Parent_Id");
            entity.Property(e => e.SourceId).HasColumnName("Source_Id");

            entity.HasOne(d => d.Language).WithMany(p => p.Categories)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category_Languages");

            entity.HasOne(d => d.Source).WithMany(p => p.Categories)
                .HasForeignKey(d => d.SourceId)
                .HasConstraintName("FK_Category_Source");
        });

        modelBuilder.Entity<IssuingAuthorityType>(entity =>
        {
            entity.HasKey(e => e.IssuingAuthorityId);

            entity.Property(e => e.IssuingAuthorityId)
                .ValueGeneratedNever()
                .HasColumnName("Issuing_Authority_Id");
            entity.Property(e => e.AuthorityNameAr)
                .HasMaxLength(250)
                .HasColumnName("Authority_Name_Ar");
            entity.Property(e => e.AuthorityNameEn)
                .HasMaxLength(250)
                .HasColumnName("Authority_Name_En");
            entity.Property(e => e.AuthorityRank).HasColumnName("Authority_Rank");
            entity.Property(e => e.Jurisdiction).HasMaxLength(150);
            entity.Property(e => e.SourceId)
                .HasDefaultValue(1)
                .HasColumnName("Source_Id");

            entity.HasOne(d => d.Source).WithMany(p => p.IssuingAuthorityTypes)
                .HasForeignKey(d => d.SourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IssuingAuthorityTypes_Source");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("PK__Language__B7596FF394419887");

            entity.Property(e => e.LanguageId)
                .ValueGeneratedNever()
                .HasColumnName("Language_Id");
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(10)
                .HasColumnName("Language_Code");
            entity.Property(e => e.LanguageNameEn)
                .HasMaxLength(100)
                .HasColumnName("Language_Name_En");
        });

        modelBuilder.Entity<Legislation>(entity =>
        {
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Aisummary)
                .HasMaxLength(2500)
                .HasColumnName("AISummary");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_by");
            entity.Property(e => e.DateOfIssuance)
                .HasColumnType("datetime")
                .HasColumnName("Date_Of_Issuance");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(10)
                .HasColumnName("Display_Name");
            entity.Property(e => e.HijriDate)
                .HasMaxLength(250)
                .HasColumnName("Hijri_Date");
            entity.Property(e => e.IssuingAuthorityId).HasColumnName("Issuing_Authority_Id");
            entity.Property(e => e.JobId).HasColumnName("Job_Id");
            entity.Property(e => e.JobStatus).HasColumnName("Job_Status");
            entity.Property(e => e.LanguageId).HasColumnName("Language_Id");
            entity.Property(e => e.LegislationTypeId).HasColumnName("Legislation_Type_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_at");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_by");
            entity.Property(e => e.OfficialGazetteNumber)
                .HasMaxLength(250)
                .HasColumnName("Official_Gazette_Number");
            entity.Property(e => e.ParentLegislationId).HasColumnName("Parent_Legislation_Id");
            entity.Property(e => e.PdfUrl)
                .HasMaxLength(650)
                .HasColumnName("PDF_URL");
            entity.Property(e => e.SourceFileName)
                .HasMaxLength(250)
                .HasColumnName("Source_File_Name");
            entity.Property(e => e.SourceId).HasColumnName("Source_Id");
            entity.Property(e => e.StatusId).HasColumnName("Status_Id");
            entity.Property(e => e.SubCategoryId).HasColumnName("Sub_Category_Id");
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.Version).HasColumnType("decimal(5, 1)");

            entity.HasOne(d => d.Category).WithMany(p => p.Legislations)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Legislations_Category");

            entity.HasOne(d => d.IssuingAuthority).WithMany(p => p.Legislations)
                .HasForeignKey(d => d.IssuingAuthorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Legislations_IssuingAuthorityTypes");

            entity.HasOne(d => d.Language).WithMany(p => p.Legislations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Legislations_Languages");

            entity.HasOne(d => d.LegislationType).WithMany(p => p.Legislations)
                .HasForeignKey(d => d.LegislationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Legislations_LegislationTypes");

            entity.HasOne(d => d.Source).WithMany(p => p.Legislations)
                .HasForeignKey(d => d.SourceId)
                .HasConstraintName("FK_Legislations_DataSource");

            entity.HasOne(d => d.Status).WithMany(p => p.Legislations)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Legislations_LegislationStatusTypes");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Legislations)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK_Legislations_SubCategory");
        });

        modelBuilder.Entity<LegislationStatusType>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.HasIndex(e => e.StatusNameEn, "Unique_LegislationStatusTypes").IsUnique();

            entity.Property(e => e.StatusId).HasColumnName("Status_Id");
            entity.Property(e => e.StatusDescription)
                .HasMaxLength(250)
                .HasColumnName("Status_Description");
            entity.Property(e => e.StatusNameAr)
                .HasMaxLength(250)
                .HasColumnName("Status_Name_Ar");
            entity.Property(e => e.StatusNameEn)
                .HasMaxLength(250)
                .HasColumnName("Status_Name_En");
        });

        modelBuilder.Entity<LegislationType>(entity =>
        {
            entity.Property(e => e.LegislationTypeId)
                .ValueGeneratedNever()
                .HasColumnName("Legislation_Type_Id");
            entity.Property(e => e.Lookup).HasMaxLength(1500);
            entity.Property(e => e.NameAr)
                .HasMaxLength(150)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("Name_En");
            entity.Property(e => e.SourceId)
                .HasDefaultValue(1)
                .HasColumnName("Source_Id");

            entity.HasOne(d => d.Source).WithMany(p => p.LegislationTypes)
                .HasForeignKey(d => d.SourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegislationTypes_Source");
        });

        modelBuilder.Entity<Source>(entity =>
        {
            entity.HasKey(e => e.SourceId).HasName("PK__Source");

            entity.ToTable("Source");

            entity.Property(e => e.SourceId)
                .ValueGeneratedNever()
                .HasColumnName("Source_Id");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_AR");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_EN");
            entity.Property(e => e.SourceUrl)
                .HasMaxLength(255)
                .HasColumnName("Source_Url");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.ToTable("SubCategory");

            entity.Property(e => e.SubCategoryId).HasColumnName("SubCategory_Id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CategoryNameAr)
                .HasMaxLength(250)
                .HasColumnName("Category_Name_Ar");
            entity.Property(e => e.CategoryNameEn)
                .HasMaxLength(250)
                .HasColumnName("Category_Name_En");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValue("CSP")
                .HasColumnName("Created_by");
            entity.Property(e => e.LanguageId)
                .HasDefaultValue(2)
                .HasColumnName("Language_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_at");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_by");
            entity.Property(e => e.SourceId).HasColumnName("Source_Id");

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubCategory_Category");

            entity.HasOne(d => d.Language).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubCategory_Languages");

            entity.HasOne(d => d.Source).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.SourceId)
                .HasConstraintName("FK_SubCategory_Source");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
