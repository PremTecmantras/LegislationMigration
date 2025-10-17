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

    public virtual DbSet<AmendedLegislation> AmendedLegislations { get; set; }

    public virtual DbSet<AmendedLegislationsSilver> AmendedLegislationsSilvers { get; set; }

    public virtual DbSet<AmendmentType> AmendmentTypes { get; set; }

    public virtual DbSet<Appendix> Appendices { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleSilver> ArticleSilvers { get; set; }

    public virtual DbSet<ArticleStatusType> ArticleStatusTypes { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<ConflictIntensityType> ConflictIntensityTypes { get; set; }

    public virtual DbSet<ConflictIntensityTypesNew> ConflictIntensityTypesNews { get; set; }

    public virtual DbSet<ConflictStatusType> ConflictStatusTypes { get; set; }

    public virtual DbSet<ConflictType> ConflictTypes { get; set; }

    public virtual DbSet<DlparabicDatum> DlparabicData { get; set; }

    public virtual DbSet<DlpenglishDatum> DlpenglishData { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Domain> Domains { get; set; }

    public virtual DbSet<DomainSilver> DomainSilvers { get; set; }

    public virtual DbSet<DomainType> DomainTypes { get; set; }

    public virtual DbSet<DraftLegislation> DraftLegislations { get; set; }

    public virtual DbSet<DraftStatusType> DraftStatusTypes { get; set; }

    public virtual DbSet<ExtractedImage> ExtractedImages { get; set; }

    public virtual DbSet<ExtractedTable> ExtractedTables { get; set; }

    public virtual DbSet<ExtractedTableCell> ExtractedTableCells { get; set; }

    public virtual DbSet<FailedAmendedLegislation> FailedAmendedLegislations { get; set; }

    public virtual DbSet<FailedLegislationRelation> FailedLegislationRelations { get; set; }

    public virtual DbSet<FederalMetaArabic> FederalMetaArabics { get; set; }

    public virtual DbSet<FederalMetaArabicOld> FederalMetaArabicOlds { get; set; }

    public virtual DbSet<FederalMetaEnglish> FederalMetaEnglishes { get; set; }

    public virtual DbSet<FederalMetaEnglishOld> FederalMetaEnglishOlds { get; set; }

    public virtual DbSet<FileMapping> FileMappings { get; set; }

    public virtual DbSet<IssuingAuthorityType> IssuingAuthorityTypes { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<LegilslationConflictComment> LegilslationConflictComments { get; set; }

    public virtual DbSet<Legislation> Legislations { get; set; }

    public virtual DbSet<LegislationConflict> LegislationConflicts { get; set; }

    public virtual DbSet<LegislationMapping> LegislationMappings { get; set; }

    public virtual DbSet<LegislationSilver> LegislationSilvers { get; set; }

    public virtual DbSet<LegislationStatusType> LegislationStatusTypes { get; set; }

    public virtual DbSet<LegislationType> LegislationTypes { get; set; }

    public virtual DbSet<LegislationVersion> LegislationVersions { get; set; }

    public virtual DbSet<LegislationsDataSourceRefTable> LegislationsDataSourceRefTables { get; set; }

    public virtual DbSet<OwnershipInvolvement> OwnershipInvolvements { get; set; }

    public virtual DbSet<ReferencedLegislation> ReferencedLegislations { get; set; }

    public virtual DbSet<ReferencedLegislationType> ReferencedLegislationTypes { get; set; }

    public virtual DbSet<ReferencedLegislationsSilver> ReferencedLegislationsSilvers { get; set; }

    public virtual DbSet<Source> Sources { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-OON0NBG\\SQLEXPRESS;Initial Catalog=New_TEC.Datalake.PreProduction;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AmendedLegislation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LinkedLegislations");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AmendmentText)
                .HasMaxLength(2500)
                .HasColumnName("Amendment_Text");
            entity.Property(e => e.AmendmentTypeId).HasColumnName("Amendment_Type_Id");
            entity.Property(e => e.AmendmentUri)
                .HasMaxLength(250)
                .HasColumnName("Amendment_URI");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.ModifiedArticleId).HasColumnName("Modified_Article_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.ModifiedLegislationId).HasColumnName("Modified_Legislation_Id");
            entity.Property(e => e.OriginalArticleId).HasColumnName("Original_Article_Id");
            entity.Property(e => e.OriginalArticleNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Original_Article_Number");
            entity.Property(e => e.OriginalLegislationId).HasColumnName("Original_Legislation_Id");
            entity.Property(e => e.OriginalLegislationTitle)
                .HasMaxLength(1500)
                .HasColumnName("Original_Legislation_Title");

            entity.HasOne(d => d.AmendmentType).WithMany(p => p.AmendedLegislations)
                .HasForeignKey(d => d.AmendmentTypeId)
                .HasConstraintName("FK_AmendedLegislations_AmendmentTypes");

            entity.HasOne(d => d.OriginalArticle).WithMany(p => p.AmendedLegislations)
                .HasForeignKey(d => d.OriginalArticleId)
                .HasConstraintName("FK_AmendedLegislations_Articles");

            entity.HasOne(d => d.OriginalLegislation).WithMany(p => p.AmendedLegislations)
                .HasForeignKey(d => d.OriginalLegislationId)
                .HasConstraintName("FK_AmendedLegislations_Legislations");
        });

        modelBuilder.Entity<AmendedLegislationsSilver>(entity =>
        {
            entity.ToTable("AmendedLegislationsSilver");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AmendmentText).HasColumnName("Amendment_Text");
            entity.Property(e => e.AmendmentTypeId).HasColumnName("Amendment_Type_Id");
            entity.Property(e => e.AmendmentUri)
                .HasMaxLength(250)
                .HasColumnName("Amendment_URI");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.ModifiedArticleId).HasColumnName("Modified_Article_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.ModifiedLegislationId).HasColumnName("Modified_Legislation_Id");
            entity.Property(e => e.OriginalArticleId).HasColumnName("Original_Article_Id");
            entity.Property(e => e.OriginalArticleNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Original_Article_Number");
            entity.Property(e => e.OriginalLegislationId).HasColumnName("Original_Legislation_Id");
            entity.Property(e => e.OriginalLegislationTitle)
                .HasMaxLength(1500)
                .HasColumnName("Original_Legislation_Title");

            entity.HasOne(d => d.AmendmentType).WithMany(p => p.AmendedLegislationsSilvers)
                .HasForeignKey(d => d.AmendmentTypeId)
                .HasConstraintName("FK_AmendedLegislationsSilver_AmendmentType");

            entity.HasOne(d => d.OriginalArticle).WithMany(p => p.AmendedLegislationsSilvers)
                .HasForeignKey(d => d.OriginalArticleId)
                .HasConstraintName("FK_AmendedLegislationsSilver_Article");

            entity.HasOne(d => d.OriginalLegislation).WithMany(p => p.AmendedLegislationsSilvers)
                .HasForeignKey(d => d.OriginalLegislationId)
                .HasConstraintName("FK_AmendedLegislationsSilver_Legislation");
        });

        modelBuilder.Entity<AmendmentType>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.LookupKeywords).HasMaxLength(650);
            entity.Property(e => e.NameAr)
                .HasMaxLength(250)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(250)
                .HasColumnName("Name_En");
        });

        modelBuilder.Entity<Appendix>(entity =>
        {
            entity.HasKey(e => e.AppendixId).HasName("PK__Appendix__E4FB21E98C6049F0");

            entity.ToTable("Appendix");

            entity.Property(e => e.AppendixId)
                .ValueGeneratedNever()
                .HasColumnName("Appendix_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");

            entity.HasOne(d => d.Legislation).WithMany(p => p.Appendices)
                .HasForeignKey(d => d.LegislationId)
                .HasConstraintName("FK_Appendix_Legislations");
        });

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

        modelBuilder.Entity<ArticleSilver>(entity =>
        {
            entity.HasKey(e => e.ArticleId);

            entity.ToTable("ArticleSilver");

            entity.Property(e => e.ArticleId)
                .ValueGeneratedNever()
                .HasColumnName("Article_Id");
            entity.Property(e => e.Aisummary).HasColumnName("AISummary");
            entity.Property(e => e.ArticleBody).HasColumnName("Article_Body");
            entity.Property(e => e.ArticleNumber).HasColumnName("Article_Number");
            entity.Property(e => e.ArticleTitle).HasColumnName("Article_Title");
            entity.Property(e => e.CreatedAt)
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

            entity.HasOne(d => d.Legislation).WithMany(p => p.ArticleSilvers)
                .HasForeignKey(d => d.LegislationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleSilver_LegislationSilver");
        });

        modelBuilder.Entity<ArticleStatusType>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ArticleStatusAr)
                .HasMaxLength(250)
                .HasColumnName("Article_Status_Ar");
            entity.Property(e => e.ArticleStatusEn)
                .HasMaxLength(250)
                .HasColumnName("Article_Status_En");
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

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId)
                .ValueGeneratedNever()
                .HasColumnName("Comment_Id");
            entity.Property(e => e.AppendixId).HasColumnName("Appendix_Id");
            entity.Property(e => e.ArticleId).HasColumnName("Article_Id");
            entity.Property(e => e.CommentBody).HasColumnName("Comment_Body");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");

            entity.HasOne(d => d.Appendix).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AppendixId)
                .HasConstraintName("FK_Comment_Appendix");

            entity.HasOne(d => d.Article).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ArticleId)
                .HasConstraintName("FK_Comment_Articles");

            entity.HasOne(d => d.Legislation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.LegislationId)
                .HasConstraintName("FK_Comment_Legislations");
        });

        modelBuilder.Entity<ConflictIntensityType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Conflict__3214EC2756D99074");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.IntensityAr)
                .HasMaxLength(255)
                .HasColumnName("Intensity_Ar");
            entity.Property(e => e.IntensityEn)
                .HasMaxLength(255)
                .HasColumnName("Intensity_En");
        });

        modelBuilder.Entity<ConflictIntensityTypesNew>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Conflict__3214EC276D47433B");

            entity.ToTable("ConflictIntensityTypes_New");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.IntensityAr)
                .HasMaxLength(255)
                .HasColumnName("Intensity_Ar");
            entity.Property(e => e.IntensityEn)
                .HasMaxLength(255)
                .HasColumnName("Intensity_En");
        });

        modelBuilder.Entity<ConflictStatusType>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.NameAr)
                .HasMaxLength(250)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(250)
                .HasColumnName("Name_En");
        });

        modelBuilder.Entity<ConflictType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ConflictCategory");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ConflictDefinitionAr)
                .HasMaxLength(250)
                .HasColumnName("Conflict_Definition_Ar");
            entity.Property(e => e.ConflictDefinitionEn)
                .HasMaxLength(250)
                .HasColumnName("Conflict_Definition_En");
            entity.Property(e => e.TypeName)
                .HasMaxLength(250)
                .HasColumnName("Type_Name");
        });

        modelBuilder.Entity<DlparabicDatum>(entity =>
        {
            entity.ToTable("DLPArabicData");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DateOfIssuance)
                .HasMaxLength(50)
                .HasColumnName("Date_of_Issuance");
            entity.Property(e => e.Description1)
                .HasMaxLength(550)
                .HasColumnName("Description_1");
            entity.Property(e => e.Description2)
                .HasMaxLength(500)
                .HasColumnName("Description_2");
            entity.Property(e => e.EnglishUrl)
                .HasMaxLength(1300)
                .HasColumnName("English_URL");
            entity.Property(e => e.HtmlUrl)
                .HasMaxLength(1350)
                .HasColumnName("HTML_URL");
            entity.Property(e => e.IssuingAuthority)
                .HasMaxLength(150)
                .HasColumnName("Issuing_Authority");
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.OfficialGazetteNo)
                .HasMaxLength(50)
                .HasColumnName("Official_Gazette_No");
            entity.Property(e => e.PdfUrl)
                .HasMaxLength(1650)
                .HasColumnName("PDF_URL");
            entity.Property(e => e.PdffileName)
                .HasMaxLength(550)
                .HasColumnName("PDFFileName");
            entity.Property(e => e.PrimaryMenu)
                .HasMaxLength(500)
                .HasColumnName("Primary_Menu");
            entity.Property(e => e.SecondaryMenu)
                .HasMaxLength(500)
                .HasColumnName("Secondary_Menu");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Translated).HasMaxLength(250);
            entity.Property(e => e.Type).HasMaxLength(150);

            entity.HasOne(d => d.Legislation).WithMany(p => p.DlparabicData)
                .HasForeignKey(d => d.LegislationId)
                .HasConstraintName("FK_DLPArabicData_Legislations");
        });

        modelBuilder.Entity<DlpenglishDatum>(entity =>
        {
            entity.ToTable("DLPEnglishData");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ArabicLegislation)
                .HasMaxLength(700)
                .HasColumnName("Arabic_Legislation");
            entity.Property(e => e.DateOfIssuance).HasColumnName("Date_of_Issuance");
            entity.Property(e => e.Description1)
                .HasMaxLength(250)
                .HasColumnName("Description_1");
            entity.Property(e => e.Description2)
                .HasMaxLength(300)
                .HasColumnName("Description_2");
            entity.Property(e => e.IssuingAuthority)
                .HasMaxLength(100)
                .HasColumnName("Issuing_Authority");
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.OfficialGazetteNo).HasColumnName("Official_Gazette_No");
            entity.Property(e => e.PdffileName)
                .HasMaxLength(250)
                .HasColumnName("PDFFileName");
            entity.Property(e => e.PrimaryMenu)
                .HasMaxLength(100)
                .HasColumnName("Primary_Menu");
            entity.Property(e => e.SecondaryMenu)
                .HasMaxLength(100)
                .HasColumnName("Secondary_Menu");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.ViewAsHtml)
                .HasMaxLength(250)
                .HasColumnName("View_as_HTML");
            entity.Property(e => e.ViewAsPdf)
                .HasMaxLength(250)
                .HasColumnName("View_as_PDF");

            entity.HasOne(d => d.Legislation).WithMany(p => p.DlpenglishData)
                .HasForeignKey(d => d.LegislationId)
                .HasConstraintName("FK_DLPEnglishData_Legislations");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FileName)
                .HasMaxLength(250)
                .HasColumnName("File_Name");
            entity.Property(e => e.MetaData)
                .HasMaxLength(250)
                .HasColumnName("Meta_Data");
        });

        modelBuilder.Entity<Domain>(entity =>
        {
            entity.Property(e => e.DomainId)
                .ValueGeneratedNever()
                .HasColumnName("Domain_Id");
            entity.Property(e => e.ArticleId).HasColumnName("Article_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DomainName)
                .HasMaxLength(250)
                .HasColumnName("Domain_Name");
            entity.Property(e => e.DomainTypeId).HasColumnName("Domain_Type_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.Rationale).HasMaxLength(1500);

            entity.HasOne(d => d.Article).WithMany(p => p.Domains)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Domains_Article");

            entity.HasOne(d => d.DomainType).WithMany(p => p.Domains)
                .HasForeignKey(d => d.DomainTypeId)
                .HasConstraintName("FK_Domains_DomainTypes");
        });

        modelBuilder.Entity<DomainSilver>(entity =>
        {
            entity.HasKey(e => e.DomainId);

            entity.ToTable("DomainSilver");

            entity.Property(e => e.DomainId)
                .ValueGeneratedNever()
                .HasColumnName("Domain_Id");
            entity.Property(e => e.ArticleId).HasColumnName("Article_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DomainName)
                .HasMaxLength(250)
                .HasColumnName("Domain_Name");
            entity.Property(e => e.DomainTypeId).HasColumnName("Domain_Type_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.Rationale).HasMaxLength(1500);

            entity.HasOne(d => d.Article).WithMany(p => p.DomainSilvers)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DomainSilver_ArticleSilver");

            entity.HasOne(d => d.DomainType).WithMany(p => p.DomainSilvers)
                .HasForeignKey(d => d.DomainTypeId)
                .HasConstraintName("FK_DomainSilver_DomainTypes");
        });

        modelBuilder.Entity<DomainType>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DomainName)
                .HasMaxLength(550)
                .HasColumnName("Domain_Name");
            entity.Property(e => e.LanguageId).HasColumnName("Language_Id");

            entity.HasOne(d => d.Language).WithMany(p => p.DomainTypes)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DomainTypes_Languages");
        });

        modelBuilder.Entity<DraftLegislation>(entity =>
        {
            entity.HasKey(e => e.DraftLegislationId).HasName("PK_LegislationDrafts");

            entity.Property(e => e.DraftLegislationId)
                .ValueGeneratedNever()
                .HasColumnName("Draft_Legislation_Id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.DraftContent).HasColumnName("Draft_Content");
            entity.Property(e => e.DraftNumber).HasColumnName("Draft_Number");
            entity.Property(e => e.DraftStatusId).HasColumnName("Draft_Status_Id");
            entity.Property(e => e.DraftTitle)
                .HasMaxLength(250)
                .HasColumnName("Draft_Title");
            entity.Property(e => e.FinalizedAt)
                .HasColumnType("datetime")
                .HasColumnName("Finalized_At");
            entity.Property(e => e.InitiatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Initiated_At");
            entity.Property(e => e.IssuingAuthorityId).HasColumnName("Issuing_Authority_Id");
            entity.Property(e => e.LanguageId).HasColumnName("Language_Id");
            entity.Property(e => e.LegislationTypeId).HasColumnName("Legislation_Type_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.Notes).HasMaxLength(1500);
            entity.Property(e => e.SourceFileName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Source_File_Name");
            entity.Property(e => e.SubCategoryId).HasColumnName("Sub_Category_Id");
            entity.Property(e => e.VersionId).HasColumnName("Version_Id");

            entity.HasOne(d => d.Category).WithMany(p => p.DraftLegislations)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_DraftLegislations_Category");

            entity.HasOne(d => d.DraftStatus).WithMany(p => p.DraftLegislations)
                .HasForeignKey(d => d.DraftStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegislationDrafts_Status");

            entity.HasOne(d => d.IssuingAuthority).WithMany(p => p.DraftLegislations)
                .HasForeignKey(d => d.IssuingAuthorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DraftLegislations_IssuingAuthorityTypes");

            entity.HasOne(d => d.Language).WithMany(p => p.DraftLegislations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DraftLegislations_Languages");

            entity.HasOne(d => d.LegislationType).WithMany(p => p.DraftLegislations)
                .HasForeignKey(d => d.LegislationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DraftLegislations_LegislationTypes");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.DraftLegislations)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK_DraftLegislations_SubCategory");

            entity.HasOne(d => d.Version).WithMany(p => p.DraftLegislations)
                .HasForeignKey(d => d.VersionId)
                .HasConstraintName("FK_LegislationDrafts_LegislationVersions");
        });

        modelBuilder.Entity<DraftStatusType>(entity =>
        {
            entity.HasKey(e => e.DraftStatusId);

            entity.Property(e => e.DraftStatusId)
                .ValueGeneratedNever()
                .HasColumnName("Draft_Status_Id");
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

        modelBuilder.Entity<ExtractedImage>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.Property(e => e.ImageId)
                .ValueGeneratedNever()
                .HasColumnName("Image_Id");
            entity.Property(e => e.AppendixId).HasColumnName("Appendix_Id");
            entity.Property(e => e.ArticleId).HasColumnName("Article_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.ImageOrder).HasColumnName("Image_Order");
            entity.Property(e => e.ImageSourceFileName)
                .HasMaxLength(250)
                .HasColumnName("Image_Source_File_Name");
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");

            entity.HasOne(d => d.Appendix).WithMany(p => p.ExtractedImages)
                .HasForeignKey(d => d.AppendixId)
                .HasConstraintName("FK_ExtractedImages_Appendix");

            entity.HasOne(d => d.Article).WithMany(p => p.ExtractedImages)
                .HasForeignKey(d => d.ArticleId)
                .HasConstraintName("FK_ExtractedImages_Articles");

            entity.HasOne(d => d.Legislation).WithMany(p => p.ExtractedImages)
                .HasForeignKey(d => d.LegislationId)
                .HasConstraintName("FK_ExtractedImages_Legislations");
        });

        modelBuilder.Entity<ExtractedTable>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("PK__Extracte__BAB7E6766818B8C6");

            entity.ToTable("ExtractedTable");

            entity.Property(e => e.TableId)
                .ValueGeneratedNever()
                .HasColumnName("Table_Id");
            entity.Property(e => e.AppendixId).HasColumnName("Appendix_Id");
            entity.Property(e => e.ArticleId).HasColumnName("Article_Id");
            entity.Property(e => e.Content).HasMaxLength(2500);
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.NumCols).HasColumnName("Num_Cols");
            entity.Property(e => e.NumRows).HasColumnName("Num_Rows");
            entity.Property(e => e.TableHeading)
                .HasMaxLength(250)
                .HasColumnName("Table_Heading");
            entity.Property(e => e.TableOrder).HasColumnName("Table_Order");

            entity.HasOne(d => d.Appendix).WithMany(p => p.ExtractedTables)
                .HasForeignKey(d => d.AppendixId)
                .HasConstraintName("FK_ExtractedTable_Appendix");

            entity.HasOne(d => d.Article).WithMany(p => p.ExtractedTables)
                .HasForeignKey(d => d.ArticleId)
                .HasConstraintName("FK_ExtractedTable_Articles");

            entity.HasOne(d => d.Legislation).WithMany(p => p.ExtractedTables)
                .HasForeignKey(d => d.LegislationId)
                .HasConstraintName("FK_ExtractedTable_Legislations");
        });

        modelBuilder.Entity<ExtractedTableCell>(entity =>
        {
            entity.HasKey(e => e.CellId).HasName("PK__Extracte__79D851EDD4B9B8AE");

            entity.ToTable("ExtractedTableCell");

            entity.Property(e => e.CellId)
                .ValueGeneratedNever()
                .HasColumnName("Cell_Id");
            entity.Property(e => e.CellText)
                .HasMaxLength(1500)
                .HasColumnName("Cell_Text");
            entity.Property(e => e.ColIndex).HasColumnName("Col_Index");
            entity.Property(e => e.ColSpan).HasColumnName("Col_Span");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.RowIndex).HasColumnName("Row_Index");
            entity.Property(e => e.RowSpan).HasColumnName("Row_Span");
            entity.Property(e => e.TableId).HasColumnName("Table_Id");

            entity.HasOne(d => d.Table).WithMany(p => p.ExtractedTableCells)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExtractedTableCell_ExtractedTable");
        });

        modelBuilder.Entity<FailedAmendedLegislation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Action).HasMaxLength(255);
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");

            entity.HasOne(d => d.Legislation).WithMany(p => p.FailedAmendedLegislations)
                .HasForeignKey(d => d.LegislationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FailedAmendedLegislations_Legislations");
        });

        modelBuilder.Entity<FailedLegislationRelation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.RelationType).HasMaxLength(255);

            entity.HasOne(d => d.Legislation).WithMany(p => p.FailedLegislationRelations)
                .HasForeignKey(d => d.LegislationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FailedLegislationRelations_Legislations");
        });

        modelBuilder.Entity<FederalMetaArabic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FedralMetaArabic_New");

            entity.ToTable("FederalMetaArabic");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AmendedLegislation)
                .HasMaxLength(1550)
                .HasColumnName("Amended_Legislation");
            entity.Property(e => e.DetailUrl)
                .HasMaxLength(100)
                .HasColumnName("detail_url");
            entity.Property(e => e.DownloadInfo)
                .HasMaxLength(250)
                .HasColumnName("download_info");
            entity.Property(e => e.EffectiveDate)
                .HasMaxLength(50)
                .HasColumnName("effective_date");
            entity.Property(e => e.Identifier).HasMaxLength(650);
            entity.Property(e => e.IssuedDate)
                .HasMaxLength(50)
                .HasColumnName("issued_date");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .HasColumnName("language");
            entity.Property(e => e.LegislationState)
                .HasMaxLength(50)
                .HasColumnName("legislation_state");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.OfficialGazetteDate)
                .HasMaxLength(50)
                .HasColumnName("official_gazette_date");
            entity.Property(e => e.OfficialGazetteNo)
                .HasMaxLength(50)
                .HasColumnName("official_gazette_no");
            entity.Property(e => e.OriginalAmendingRepealing)
                .HasMaxLength(50)
                .HasColumnName("Original_Amending_Repealing");
            entity.Property(e => e.PdfFileName)
                .HasMaxLength(550)
                .HasColumnName("PDF_FileName");
            entity.Property(e => e.PdfTitle).HasColumnName("PDF_Title");
            entity.Property(e => e.PrimaryLawType)
                .HasMaxLength(50)
                .HasColumnName("primary_law_type");
            entity.Property(e => e.PrimarySector)
                .HasMaxLength(50)
                .HasColumnName("primary_sector");
            entity.Property(e => e.RelatedLegislationNumber)
                .HasMaxLength(150)
                .HasColumnName("Related_Legislation_Number");
            entity.Property(e => e.RelatedLegislationTitle)
                .HasMaxLength(1550)
                .HasColumnName("Related_Legislation_Title");
            entity.Property(e => e.RelatedLegislationYear)
                .HasMaxLength(150)
                .HasColumnName("Related_Legislation_Year");
            entity.Property(e => e.RepealedLegislation)
                .HasMaxLength(550)
                .HasColumnName("Repealed_Legislation");
            entity.Property(e => e.Source).HasMaxLength(50);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<FederalMetaArabicOld>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FederalMetaArabic_old");

            entity.Property(e => e.AmendedLegislation)
                .HasMaxLength(1550)
                .HasColumnName("Amended_Legislation");
            entity.Property(e => e.DetailUrl)
                .HasMaxLength(100)
                .HasColumnName("detail_url");
            entity.Property(e => e.DownloadInfo)
                .HasMaxLength(250)
                .HasColumnName("download_info");
            entity.Property(e => e.DownloadUrl)
                .HasMaxLength(105)
                .IsUnicode(false);
            entity.Property(e => e.EffectiveDate)
                .HasMaxLength(50)
                .HasColumnName("effective_date");
            entity.Property(e => e.GeneratedFileName)
                .HasMaxLength(57)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Identifier).HasMaxLength(650);
            entity.Property(e => e.IssuedDate)
                .HasMaxLength(50)
                .HasColumnName("issued_date");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .HasColumnName("language");
            entity.Property(e => e.LegislationState)
                .HasMaxLength(50)
                .HasColumnName("legislation_state");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.OfficialGazetteDate)
                .HasMaxLength(50)
                .HasColumnName("official_gazette_date");
            entity.Property(e => e.OfficialGazetteNo)
                .HasMaxLength(50)
                .HasColumnName("official_gazette_no");
            entity.Property(e => e.OriginalAmendingRepealing)
                .HasMaxLength(50)
                .HasColumnName("Original_Amending_Repealing");
            entity.Property(e => e.PdfFileName)
                .HasMaxLength(550)
                .HasColumnName("PDF_FileName");
            entity.Property(e => e.PrimaryLawType)
                .HasMaxLength(50)
                .HasColumnName("primary_law_type");
            entity.Property(e => e.PrimarySector)
                .HasMaxLength(50)
                .HasColumnName("primary_sector");
            entity.Property(e => e.RelatedLegislationNumber)
                .HasMaxLength(150)
                .HasColumnName("Related_Legislation_Number");
            entity.Property(e => e.RelatedLegislationTitle)
                .HasMaxLength(1550)
                .HasColumnName("Related_Legislation_Title");
            entity.Property(e => e.RelatedLegislationYear)
                .HasMaxLength(150)
                .HasColumnName("Related_Legislation_Year");
            entity.Property(e => e.RepealedLegislation)
                .HasMaxLength(550)
                .HasColumnName("Repealed_Legislation");
            entity.Property(e => e.Source).HasMaxLength(50);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<FederalMetaEnglish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FedralMetaEnglish_New");

            entity.ToTable("FederalMetaEnglish");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AmendedLegislation)
                .HasMaxLength(600)
                .HasColumnName("Amended_Legislation");
            entity.Property(e => e.DatabaseTitle)
                .HasMaxLength(300)
                .HasColumnName("Database_Title");
            entity.Property(e => e.DetailUrl)
                .HasMaxLength(100)
                .HasColumnName("detail_url");
            entity.Property(e => e.DownloadInfo)
                .HasMaxLength(250)
                .HasColumnName("download_info");
            entity.Property(e => e.EffectiveDate).HasColumnName("effective_date");
            entity.Property(e => e.Identifier).HasMaxLength(750);
            entity.Property(e => e.IssuedDate).HasColumnName("issued_date");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .HasColumnName("language");
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.LegislationState)
                .HasMaxLength(50)
                .HasColumnName("legislation_state");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.OfficialGazetteDate).HasColumnName("official_gazette_date");
            entity.Property(e => e.OfficialGazetteNo)
                .HasMaxLength(50)
                .HasColumnName("official_gazette_no");
            entity.Property(e => e.OriginalAmendingRepealing)
                .HasMaxLength(50)
                .HasColumnName("Original_Amending_Repealing");
            entity.Property(e => e.PdfFileName)
                .HasMaxLength(550)
                .HasColumnName("PDF_FileName");
            entity.Property(e => e.PdfTitle)
                .HasMaxLength(300)
                .HasColumnName("PDF_Title");
            entity.Property(e => e.PrimaryLawType)
                .HasMaxLength(50)
                .HasColumnName("primary_law_type");
            entity.Property(e => e.PrimarySector)
                .HasMaxLength(50)
                .HasColumnName("primary_sector");
            entity.Property(e => e.RelatedLegislationNumber)
                .HasMaxLength(150)
                .HasColumnName("Related_Legislation_Number");
            entity.Property(e => e.RelatedLegislationTitle)
                .HasMaxLength(1850)
                .HasColumnName("Related_Legislation_Title");
            entity.Property(e => e.RelatedLegislationYear)
                .HasMaxLength(150)
                .HasColumnName("Related_Legislation_Year");
            entity.Property(e => e.RepealedLegislation)
                .HasMaxLength(600)
                .HasColumnName("Repealed_Legislation");
            entity.Property(e => e.Source).HasMaxLength(50);
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasColumnName("title");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<FederalMetaEnglishOld>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FederalMetaEnglish_old");

            entity.Property(e => e.AmendedLegislation)
                .HasMaxLength(600)
                .HasColumnName("Amended_Legislation");
            entity.Property(e => e.DetailUrl)
                .HasMaxLength(100)
                .HasColumnName("detail_url");
            entity.Property(e => e.DownloadInfo)
                .HasMaxLength(250)
                .HasColumnName("download_info");
            entity.Property(e => e.DownloadUrl)
                .HasMaxLength(105)
                .IsUnicode(false);
            entity.Property(e => e.EffectiveDate).HasColumnName("effective_date");
            entity.Property(e => e.GeneratedFileName)
                .HasMaxLength(57)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Identifier).HasMaxLength(750);
            entity.Property(e => e.IssuedDate).HasColumnName("issued_date");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .HasColumnName("language");
            entity.Property(e => e.LegislationState)
                .HasMaxLength(50)
                .HasColumnName("legislation_state");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.OfficialGazetteDate).HasColumnName("official_gazette_date");
            entity.Property(e => e.OfficialGazetteNo)
                .HasMaxLength(50)
                .HasColumnName("official_gazette_no");
            entity.Property(e => e.OriginalAmendingRepealing)
                .HasMaxLength(50)
                .HasColumnName("Original_Amending_Repealing");
            entity.Property(e => e.PdfFileName)
                .HasMaxLength(550)
                .HasColumnName("PDF_FileName");
            entity.Property(e => e.PrimaryLawType)
                .HasMaxLength(50)
                .HasColumnName("primary_law_type");
            entity.Property(e => e.PrimarySector)
                .HasMaxLength(50)
                .HasColumnName("primary_sector");
            entity.Property(e => e.RelatedLegislationNumber)
                .HasMaxLength(150)
                .HasColumnName("Related_Legislation_Number");
            entity.Property(e => e.RelatedLegislationTitle)
                .HasMaxLength(1850)
                .HasColumnName("Related_Legislation_Title");
            entity.Property(e => e.RelatedLegislationYear)
                .HasMaxLength(150)
                .HasColumnName("Related_Legislation_Year");
            entity.Property(e => e.RepealedLegislation)
                .HasMaxLength(600)
                .HasColumnName("Repealed_Legislation");
            entity.Property(e => e.Source).HasMaxLength(50);
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasColumnName("title");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<FileMapping>(entity =>
        {
            entity.ToTable("FileMapping");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.LanguageId).HasColumnName("Language_ID");
            entity.Property(e => e.NewFileName).HasMaxLength(250);
            entity.Property(e => e.OldFileName).HasMaxLength(250);
            entity.Property(e => e.SourceId).HasColumnName("Source_ID");

            entity.HasOne(d => d.Language).WithMany(p => p.FileMappings)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FileMapping_Languages");

            entity.HasOne(d => d.Source).WithMany(p => p.FileMappings)
                .HasForeignKey(d => d.SourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FileMapping_Source");
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

        modelBuilder.Entity<LegilslationConflictComment>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.LegislationConflictId).HasColumnName("Legislation_Conflict_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");

            entity.HasOne(d => d.LegislationConflict).WithMany(p => p.LegilslationConflictComments)
                .HasForeignKey(d => d.LegislationConflictId)
                .HasConstraintName("FK_LegilslationConflictComments_LegislationConflicts");
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

        modelBuilder.Entity<LegislationConflict>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ArticleId).HasColumnName("Article_Id");
            entity.Property(e => e.ConflictStatusId).HasColumnName("Conflict_Status_Id");
            entity.Property(e => e.ConflictTypeId).HasColumnName("ConflictType_Id");
            entity.Property(e => e.ConflictedArticleId).HasColumnName("Conflicted_Article_Id");
            entity.Property(e => e.ConflictedLegislationId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Conflicted_Legislation_Id");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.IntensityLevelTypeId).HasColumnName("Intensity_Level_Type_Id");
            entity.Property(e => e.IntensityRationale).HasColumnName("Intensity_Rationale");
            entity.Property(e => e.IsReviewed).HasColumnName("Is_Reviewed");
            entity.Property(e => e.LegilationId).HasColumnName("Legilation_Id");
            entity.Property(e => e.LlmConflictReason).HasColumnName("LLM_Conflict_Reason");
            entity.Property(e => e.Name).HasMaxLength(250);

            entity.HasOne(d => d.Article).WithMany(p => p.LegislationConflicts)
                .HasForeignKey(d => d.ArticleId)
                .HasConstraintName("FK_LegislationConflicts_Articles");

            entity.HasOne(d => d.ConflictStatus).WithMany(p => p.LegislationConflicts)
                .HasForeignKey(d => d.ConflictStatusId)
                .HasConstraintName("FK_LegislationConflicts_ConflictStatusTypes");

            entity.HasOne(d => d.ConflictType).WithMany(p => p.LegislationConflicts)
                .HasForeignKey(d => d.ConflictTypeId)
                .HasConstraintName("FK_LegislationConflicts_ConflictTypes");

            entity.HasOne(d => d.IntensityLevelType).WithMany(p => p.LegislationConflicts)
                .HasForeignKey(d => d.IntensityLevelTypeId)
                .HasConstraintName("FK_LegislationConflicts_ConflictIntensityTypes");

            entity.HasOne(d => d.Legilation).WithMany(p => p.LegislationConflicts)
                .HasForeignKey(d => d.LegilationId)
                .HasConstraintName("FK_LegislationConflicts_Legislations");
        });

        modelBuilder.Entity<LegislationMapping>(entity =>
        {
            entity.ToTable("LegislationMapping");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.SourceId).HasColumnName("Source_Id");
            entity.Property(e => e.TargetLegislationName)
                .HasMaxLength(250)
                .HasColumnName("Target_Legislation_Name");

            entity.HasOne(d => d.Legislation).WithMany(p => p.LegislationMappings)
                .HasForeignKey(d => d.LegislationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegislationMapping_Legislations");

            entity.HasOne(d => d.Source).WithMany(p => p.LegislationMappings)
                .HasForeignKey(d => d.SourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegislationMapping_Source");
        });

        modelBuilder.Entity<LegislationSilver>(entity =>
        {
            entity.HasKey(e => e.LegislationId);

            entity.ToTable("LegislationSilver");

            entity.Property(e => e.LegislationId)
                .ValueGeneratedNever()
                .HasColumnName("Legislation_Id");
            entity.Property(e => e.Aisummary)
                .HasMaxLength(2500)
                .HasColumnName("AISummary");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedAt)
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
            entity.Property(e => e.Version).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Category).WithMany(p => p.LegislationSilvers)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_LegislationSilver_Category");

            entity.HasOne(d => d.IssuingAuthority).WithMany(p => p.LegislationSilvers)
                .HasForeignKey(d => d.IssuingAuthorityId)
                .HasConstraintName("FK_LegislationSilver_IssuingAuthorityTypes");

            entity.HasOne(d => d.Language).WithMany(p => p.LegislationSilvers)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegislationSilver_Languages");

            entity.HasOne(d => d.LegislationType).WithMany(p => p.LegislationSilvers)
                .HasForeignKey(d => d.LegislationTypeId)
                .HasConstraintName("FK_LegislationSilver_LegislationTypes");

            entity.HasOne(d => d.Source).WithMany(p => p.LegislationSilvers)
                .HasForeignKey(d => d.SourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegislationSilver_DataSource");

            entity.HasOne(d => d.Status).WithMany(p => p.LegislationSilvers)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_LegislationSilver_LegislationStatusTypes");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.LegislationSilvers)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK_LegislationSilver_SubCategory");
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

        modelBuilder.Entity<LegislationVersion>(entity =>
        {
            entity.HasKey(e => e.VersionId);

            entity.Property(e => e.VersionId)
                .ValueGeneratedNever()
                .HasColumnName("Version_Id");
            entity.Property(e => e.ChangesSummary)
                .HasMaxLength(250)
                .HasColumnName("Changes_Summary");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_by");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.EffectiveDate)
                .HasColumnType("datetime")
                .HasColumnName("Effective_Date");
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_at");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_by");
            entity.Property(e => e.VersionNumber).HasColumnName("Version_Number");

            entity.HasOne(d => d.Legislation).WithMany(p => p.LegislationVersions)
                .HasForeignKey(d => d.LegislationId)
                .HasConstraintName("FK_LegislationVersions_Legislations");
        });

        modelBuilder.Entity<LegislationsDataSourceRefTable>(entity =>
        {
            entity.ToTable("LegislationsDataSourceRefTable");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DirPath).HasMaxLength(650);
            entity.Property(e => e.LanguageId).HasColumnName("LanguageID");
            entity.Property(e => e.LegislationStatusTypeId).HasColumnName("LegislationStatusTypeID");
            entity.Property(e => e.PdfdirPath)
                .HasMaxLength(650)
                .HasColumnName("PDFDirPath");
            entity.Property(e => e.SourceId).HasColumnName("SourceID");

            entity.HasOne(d => d.Language).WithMany(p => p.LegislationsDataSourceRefTables)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegislationsDataSourceRefTable_Languages");

            entity.HasOne(d => d.LegislationStatusType).WithMany(p => p.LegislationsDataSourceRefTables)
                .HasForeignKey(d => d.LegislationStatusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegislationsDataSourceRefTable_LegislationStatusTypes");

            entity.HasOne(d => d.Source).WithMany(p => p.LegislationsDataSourceRefTables)
                .HasForeignKey(d => d.SourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LegislationsDataSourceRefTable_Source");
        });

        modelBuilder.Entity<OwnershipInvolvement>(entity =>
        {
            entity.HasKey(e => e.OwnershipId).HasName("PK__OwnershipInvolvement");

            entity.ToTable("OwnershipInvolvement");

            entity.Property(e => e.OwnershipId)
                .ValueGeneratedNever()
                .HasColumnName("Ownership_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.InvolvedEntities)
                .HasMaxLength(1500)
                .HasColumnName("Involved_Entities");
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.OwnershipEntity)
                .HasMaxLength(250)
                .HasColumnName("Ownership_Entity");

            entity.HasOne(d => d.Legislation).WithMany(p => p.OwnershipInvolvements)
                .HasForeignKey(d => d.LegislationId)
                .HasConstraintName("FK_OwnershipInvolvement_Legislations");
        });

        modelBuilder.Entity<ReferencedLegislation>(entity =>
        {
            entity.HasKey(e => e.ReferenceId).HasName("PK__ReferencedLegislation");

            entity.Property(e => e.ReferenceId)
                .ValueGeneratedNever()
                .HasColumnName("Reference_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.ReferencedLegislationId).HasColumnName("Referenced_Legislation_Id");
            entity.Property(e => e.ReferencedLegislationTitle)
                .HasMaxLength(1500)
                .HasColumnName("Referenced_Legislation_Title");
            entity.Property(e => e.ReferencedLegislationTypeId).HasColumnName("Referenced_Legislation_Type_Id");

            entity.HasOne(d => d.Legislation).WithMany(p => p.ReferencedLegislationLegislations)
                .HasForeignKey(d => d.LegislationId)
                .HasConstraintName("FK_ReferencedLegislation_Legislations");

            entity.HasOne(d => d.ReferencedLegislationNavigation).WithMany(p => p.ReferencedLegislationReferencedLegislationNavigations)
                .HasForeignKey(d => d.ReferencedLegislationId)
                .HasConstraintName("FK_ReferencedLegislations_Legislation");

            entity.HasOne(d => d.ReferencedLegislationType).WithMany(p => p.ReferencedLegislations)
                .HasForeignKey(d => d.ReferencedLegislationTypeId)
                .HasConstraintName("FK_ReferencedLegislations_ReferencedLegislationTypes");
        });

        modelBuilder.Entity<ReferencedLegislationType>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.LookupKeywords).HasMaxLength(1500);
            entity.Property(e => e.RelationshipTypeAr)
                .HasMaxLength(50)
                .HasColumnName("RelationshipType_Ar");
            entity.Property(e => e.RelationshipTypeEn)
                .HasMaxLength(50)
                .HasColumnName("RelationshipType_En");
        });

        modelBuilder.Entity<ReferencedLegislationsSilver>(entity =>
        {
            entity.HasKey(e => e.ReferenceId);

            entity.ToTable("ReferencedLegislationsSilver");

            entity.Property(e => e.ReferenceId)
                .ValueGeneratedNever()
                .HasColumnName("Reference_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.DisplayName).HasMaxLength(10);
            entity.Property(e => e.LegislationId).HasColumnName("Legislation_Id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.ReferencedLegislationId).HasColumnName("Referenced_Legislation_Id");
            entity.Property(e => e.ReferencedLegislationTitle)
                .HasMaxLength(1500)
                .HasColumnName("Referenced_Legislation_Title");
            entity.Property(e => e.ReferencedLegislationTypeId).HasColumnName("Referenced_Legislation_Type_Id");

            entity.HasOne(d => d.Legislation).WithMany(p => p.ReferencedLegislationsSilverLegislations)
                .HasForeignKey(d => d.LegislationId)
                .HasConstraintName("FK_RefLegSilver_LegislationSilver");

            entity.HasOne(d => d.ReferencedLegislation).WithMany(p => p.ReferencedLegislationsSilverReferencedLegislations)
                .HasForeignKey(d => d.ReferencedLegislationId)
                .HasConstraintName("FK_RefLegSilver_ReferencedLegislationSilver");

            entity.HasOne(d => d.ReferencedLegislationType).WithMany(p => p.ReferencedLegislationsSilvers)
                .HasForeignKey(d => d.ReferencedLegislationTypeId)
                .HasConstraintName("FK_RefLegSilver_ReferencedLegislationTypes");
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
