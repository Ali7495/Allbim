using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAL.Models
{
    public partial class AlbimDbContext : DbContext
    {

        public AlbimDbContext(DbContextOptions<AlbimDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
        public virtual DbSet<ArticleMetaKey> ArticleMetaKeys { get; set; }
        public virtual DbSet<ArticleSection> ArticleSections { get; set; }
        public virtual DbSet<ArticleType> ArticleTypes { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Center> Centers { get; set; }
        public virtual DbSet<CentralRuleType> CentralRuleTypes { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyAddress> CompanyAddresses { get; set; }
        public virtual DbSet<CompanyAgent> CompanyAgents { get; set; }
        public virtual DbSet<CompanyAgentPerson> CompanyAgentPeople { get; set; }
        public virtual DbSet<CompanyCenter> CompanyCenters { get; set; }
        public virtual DbSet<CompanyCenterSchedule> CompanyCenterSchedules { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Enumeration> Enumerations { get; set; }
        public virtual DbSet<Faq> Faqs { get; set; }
        public virtual DbSet<Info> Infos { get; set; }
        public virtual DbSet<InspectionSession> InspectionSessions { get; set; }
        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<InsuranceCentralRule> InsuranceCentralRules { get; set; }
        public virtual DbSet<InsuranceFaq> InsuranceFaqs { get; set; }
        public virtual DbSet<InsuranceField> InsuranceFields { get; set; }
        public virtual DbSet<InsuranceFrontTab> InsuranceFrontTabs { get; set; }
        public virtual DbSet<InsuranceStep> InsuranceSteps { get; set; }
        public virtual DbSet<InsuranceTermType> InsuranceTermTypes { get; set; }
        public virtual DbSet<InsuredRequest> InsuredRequests { get; set; }
        public virtual DbSet<InsuredRequestCompany> InsuredRequestCompanies { get; set; }
        public virtual DbSet<InsuredRequestPerson> InsuredRequestPeople { get; set; }
        public virtual DbSet<InsuredRequestPlace> InsuredRequestPlaces { get; set; }
        public virtual DbSet<InsuredRequestRelatedPerson> InsuredRequestRelatedPeople { get; set; }
        public virtual DbSet<InsuredRequestVehicle> InsuredRequestVehicles { get; set; }
        public virtual DbSet<InsuredRequestVehicleDetail> InsuredRequestVehicleDetails { get; set; }
        public virtual DbSet<Insurer> Insurers { get; set; }
        public virtual DbSet<InsurerTerm> InsurerTerms { get; set; }
        public virtual DbSet<InsurerTermDetail> InsurerTermDetails { get; set; }
        public virtual DbSet<IssueSession> IssueSessions { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<OnlinePayment> OnlinePayments { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentGateway> PaymentGateways { get; set; }
        public virtual DbSet<PaymentGatewayDetail> PaymentGatewayDetails { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonAddress> PersonAddresses { get; set; }
        public virtual DbSet<PersonAttachment> PersonAttachments { get; set; }
        public virtual DbSet<PersonCompany> PersonCompanies { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<PlaceAddress> PlaceAddresses { get; set; }
        public virtual DbSet<PolicyRequest> PolicyRequests { get; set; }
        public virtual DbSet<PolicyRequestAttachment> PolicyRequestAttachments { get; set; }
        public virtual DbSet<PolicyRequestComment> PolicyRequestComments { get; set; }
        public virtual DbSet<PolicyRequestCommentAttachment> PolicyRequestCommentAttachments { get; set; }
        public virtual DbSet<PolicyRequestDetail> PolicyRequestDetails { get; set; }
        public virtual DbSet<PolicyRequestFactor> PolicyRequestFactors { get; set; }
        public virtual DbSet<PolicyRequestFactorDetail> PolicyRequestFactorDetails { get; set; }
        public virtual DbSet<PolicyRequestHolder> PolicyRequestHolders { get; set; }
        public virtual DbSet<PolicyRequestInspection> PolicyRequestInspections { get; set; }
        public virtual DbSet<PolicyRequestIssue> PolicyRequestIssues { get; set; }
        public virtual DbSet<PolicyRequestStatus> PolicyRequestStatuses { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<RegisterTemp> RegisterTemps { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
        public virtual DbSet<ReminderPeriod> ReminderPeriods { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResourceOperation> ResourceOperations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<SchemaVersion> SchemaVersions { get; set; }
        public virtual DbSet<Sysdiagram> Sysdiagrams { get; set; }
        public virtual DbSet<TownShip> TownShips { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleApplication> VehicleApplications { get; set; }
        public virtual DbSet<VehicleBrand> VehicleBrands { get; set; }
        public virtual DbSet<VehicleRuleCategory> VehicleRuleCategories { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                 optionsBuilder.UseSqlServer("Server=46.102.129.91;Database=AlbimDb;Trusted_Connection=False;;User Id=sa;Password=1777592Methoder;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelFilteringDeleted(modelBuilder);


            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ZoneNumber).HasMaxLength(10);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Address_City");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.ArticleTypeId).HasDefaultValueSql("((2))");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActivated)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Slug)
                    .HasMaxLength(50)
                    .HasColumnName("slug");

                entity.Property(e => e.Summary).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.HasOne(d => d.ArticleType)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.ArticleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Article_ArticleTypeId");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Article_AuthorId");
            });

            modelBuilder.Entity<ArticleCategory>(entity =>
            {
                entity.ToTable("ArticleCategory");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleCategories)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_ArticleCategory_ArticleId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ArticleCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_ArticleCategory_CategoryId");
            });

            modelBuilder.Entity<ArticleMetaKey>(entity =>
            {
                entity.ToTable("ArticleMetaKey");

                entity.Property(e => e.Key).HasMaxLength(255);

                entity.Property(e => e.Value).HasMaxLength(255);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleMetaKeys)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleMetaKey_ArticleId");
            });

            modelBuilder.Entity<ArticleSection>(entity =>
            {
                entity.ToTable("ArticleSection");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleSections)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleSection_ArticleId");
            });

            modelBuilder.Entity<ArticleType>(entity =>
            {
                entity.ToTable("ArticleType");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachment");

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Extension).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Path).HasMaxLength(500);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Slug).HasMaxLength(255);
            });

            modelBuilder.Entity<Center>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Centers)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Centers_CityId");
            });

            modelBuilder.Entity<CentralRuleType>(entity =>
            {
                entity.ToTable("CentralRuleType");

                entity.Property(e => e.RuleCaption).IsRequired();

                entity.HasOne(d => d.InsuranceField)
                    .WithMany(p => p.CentralRuleTypes)
                    .HasForeignKey(d => d.InsuranceFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CentralRuleType_InsuranceField");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.TownShip)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.TownShipId)
                    .HasConstraintName("FK_City_TownShip");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_ArticleId");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Comment_AuthorId");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Comment_ParentId");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.AvatarUrl).HasMaxLength(255);

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.EstablishedYear).HasMaxLength(25);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Summary).IsUnicode(false);

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_Company_ArticleId");
            });

            modelBuilder.Entity<CompanyAddress>(entity =>
            {
                entity.ToTable("CompanyAddress");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.CompanyAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_CompanyAddress_Address");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyAddresses)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_CompanyAddress_Company");
            });

            modelBuilder.Entity<CompanyAgent>(entity =>
            {
                entity.ToTable("CompanyAgent");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CompanyAgents)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyAgent_CityId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyAgents)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyAgent_CompanyId");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.CompanyAgents)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyAgent_PersonId");
            });

            modelBuilder.Entity<CompanyAgentPerson>(entity =>
            {
                entity.ToTable("CompanyAgentPerson");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Position).HasMaxLength(50);

                entity.HasOne(d => d.CompanyAgent)
                    .WithMany(p => p.CompanyAgentPeople)
                    .HasForeignKey(d => d.CompanyAgentId)
                    .HasConstraintName("FK_CompanyAgentPerson_CompanyAgentId");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.CompanyAgentPeople)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_CompanyAgentPerson_PersonId");
            });

            modelBuilder.Entity<CompanyCenter>(entity =>
            {
                entity.ToTable("CompanyCenter");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CompanyCenters)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_InsuranceCenter_CityId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyCenters)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_CompanyCenter_CompanyId");
            });

            modelBuilder.Entity<CompanyCenterSchedule>(entity =>
            {
                entity.ToTable("CompanyCenterSchedule");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.CompanyCenter)
                    .WithMany(p => p.CompanyCenterSchedules)
                    .HasForeignKey(d => d.CompanyCenterId)
                    .HasConstraintName("FK_CompanyCenterSchedule_CompanyCenterId");
            });

            modelBuilder.Entity<ContactUs>(entity =>
            {
                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(300);
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("Discount");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Insurance)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.InsuranceId)
                    .HasConstraintName("FK_Discount_InsuranceId");

                entity.HasOne(d => d.Insurer)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.InsurerId)
                    .HasConstraintName("FK_Discount_InsurerId");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Discount_PersonId");
            });

            modelBuilder.Entity<Enumeration>(entity =>
            {
                entity.ToTable("Enumeration");

                entity.Property(e => e.CategoryCaption)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.EnumCaption)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsEnable).HasDefaultValueSql("((1))");

                entity.Property(e => e.Order).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Enumeration_Enumeration");
            });

            modelBuilder.Entity<Faq>(entity =>
            {
                entity.ToTable("FAQ");
            });

            modelBuilder.Entity<Info>(entity =>
            {
                entity.ToTable("Info");

                entity.Property(e => e.Key).HasMaxLength(255);

                entity.Property(e => e.Slug).HasMaxLength(100);
            });

            modelBuilder.Entity<InspectionSession>(entity =>
            {
                entity.ToTable("InspectionSession");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Insurance>(entity =>
            {
                entity.ToTable("Insurance");

                entity.Property(e => e.AvatarUrl).HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Slug).HasMaxLength(100);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<InsuranceCentralRule>(entity =>
            {
                entity.ToTable("InsuranceCentralRule");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Discount).HasMaxLength(256);

                entity.Property(e => e.FieldType)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.GregorianYear)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.JalaliYear)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CentralRuleType)
                    .WithMany(p => p.InsuranceCentralRules)
                    .HasForeignKey(d => d.CentralRuleTypeId)
                    .HasConstraintName("FK_InsuranceCentralRule_CentralRuleType");
            });

            modelBuilder.Entity<InsuranceFaq>(entity =>
            {
                entity.ToTable("InsuranceFAQ");

                entity.HasOne(d => d.Insurance)
                    .WithMany(p => p.InsuranceFaqs)
                    .HasForeignKey(d => d.InsuranceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuranceFAQ_InsuranceId");
            });

            modelBuilder.Entity<InsuranceField>(entity =>
            {
                entity.ToTable("InsuranceField");

                entity.Property(e => e.InsuranceFieldTypeId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Type).HasMaxLength(200);

                entity.HasOne(d => d.Insurance)
                    .WithMany(p => p.InsuranceFields)
                    .HasForeignKey(d => d.InsuranceId)
                    .HasConstraintName("FK_InsuranceField_Insurance");
            });

            modelBuilder.Entity<InsuranceFrontTab>(entity =>
            {
                entity.ToTable("InsuranceFrontTab");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Insurance)
                    .WithMany(p => p.InsuranceFrontTabs)
                    .HasForeignKey(d => d.InsuranceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuranceFrontTab_Insurance");
            });

            modelBuilder.Entity<InsuranceStep>(entity =>
            {
                entity.ToTable("InsuranceStep");

                entity.Property(e => e.StepName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StepOrder).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Insurance)
                    .WithMany(p => p.InsuranceSteps)
                    .HasForeignKey(d => d.InsuranceId)
                    .HasConstraintName("FK_InsuranceStep_InsuranceId");
            });

            modelBuilder.Entity<InsuranceTermType>(entity =>
            {
                entity.ToTable("InsuranceTermType");

                entity.Property(e => e.TermCaption).IsRequired();

                entity.HasOne(d => d.InsuranceField)
                    .WithMany(p => p.InsuranceTermTypes)
                    .HasForeignKey(d => d.InsuranceFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuranceTermType_InsuranceField");
            });

            modelBuilder.Entity<InsuredRequest>(entity =>
            {
                entity.ToTable("InsuredRequest");

                entity.HasOne(d => d.PolicyRequest)
                    .WithMany(p => p.InsuredRequests)
                    .HasForeignKey(d => d.PolicyRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredRequest_PolicyRequest");
            });

            modelBuilder.Entity<InsuredRequestCompany>(entity =>
            {
                entity.ToTable("InsuredRequestCompany");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.InsuredRequestCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredRequestCompany__Company");

                entity.HasOne(d => d.InsuredRequest)
                    .WithMany(p => p.InsuredRequestCompanies)
                    .HasForeignKey(d => d.InsuredRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredRequestCompany_InsuredRequest");
            });

            modelBuilder.Entity<InsuredRequestPerson>(entity =>
            {
                entity.ToTable("InsuredRequestPerson");

                entity.HasOne(d => d.InsuredRequest)
                    .WithMany(p => p.InsuredRequestPeople)
                    .HasForeignKey(d => d.InsuredRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredRequestPerson_InsuredRequest");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.InsuredRequestPeople)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredRequestPerson__Person");
            });

            modelBuilder.Entity<InsuredRequestPlace>(entity =>
            {
                entity.ToTable("InsuredRequestPlace");

                entity.HasOne(d => d.InsuredRequest)
                    .WithMany(p => p.InsuredRequestPlaces)
                    .HasForeignKey(d => d.InsuredRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredRequestPlace_InsuredRequest");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.InsuredRequestPlaces)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredRequestPlace_Place");
            });

            modelBuilder.Entity<InsuredRequestRelatedPerson>(entity =>
            {
                entity.ToTable("InsuredRequestRelatedPerson");

                entity.HasOne(d => d.InsuredRequest)
                    .WithMany(p => p.InsuredRequestRelatedPeople)
                    .HasForeignKey(d => d.InsuredRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredRequestRelatedPerson_InsuredRequest");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.InsuredRequestRelatedPeople)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredRequestRelatedPerson_Person");
            });

            modelBuilder.Entity<InsuredRequestVehicle>(entity =>
            {
                entity.ToTable("InsuredRequestVehicle");

                entity.HasOne(d => d.InsuredRequest)
                    .WithMany(p => p.InsuredRequestVehicles)
                    .HasForeignKey(d => d.InsuredRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredRequestVehicle_InsuredRequest");

                entity.HasOne(d => d.OwnerCompany)
                    .WithMany(p => p.InsuredRequestVehicles)
                    .HasForeignKey(d => d.OwnerCompanyId)
                    .HasConstraintName("FK_InsuredRequestVehicle_Company");

                entity.HasOne(d => d.OwnerPerson)
                    .WithMany(p => p.InsuredRequestVehicles)
                    .HasForeignKey(d => d.OwnerPersonId)
                    .HasConstraintName("FK_InsuredRequestVehicle_Person");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.InsuredRequestVehicles)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredRequestVehicle_Vehicle");
            });

            modelBuilder.Entity<InsuredRequestVehicleDetail>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.InsuredRequestVehicle)
                    .WithMany(p => p.InsuredRequestVehicleDetails)
                    .HasForeignKey(d => d.InsuredRequestVehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredRequestVehicleDetails_InsuredRequestVehicle");
            });

            modelBuilder.Entity<Insurer>(entity =>
            {
                entity.ToTable("Insurer");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Insurers)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_Insurer_ArticleId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Insurers)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Insurer_Company");

                entity.HasOne(d => d.Insurance)
                    .WithMany(p => p.Insurers)
                    .HasForeignKey(d => d.InsuranceId)
                    .HasConstraintName("FK_Insurer_Insurance");
            });

            modelBuilder.Entity<InsurerTerm>(entity =>
            {
                entity.ToTable("InsurerTerm");

                entity.Property(e => e.CalculationTypeId).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Discount)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.InsuranceTermType)
                    .WithMany(p => p.InsurerTerms)
                    .HasForeignKey(d => d.InsuranceTermTypeId)
                    .HasConstraintName("FK_InsurerTerm_InsuranceTermType");

                entity.HasOne(d => d.Insurer)
                    .WithMany(p => p.InsurerTerms)
                    .HasForeignKey(d => d.InsurerId)
                    .HasConstraintName("FK_InsurerTerm_Insurer");
            });

            modelBuilder.Entity<InsurerTermDetail>(entity =>
            {
                entity.ToTable("InsurerTermDetail");

                entity.Property(e => e.CalculationType).HasMaxLength(100);

                entity.Property(e => e.Criteria).HasMaxLength(100);

                entity.Property(e => e.Discount).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(200);

                entity.Property(e => e.IsCumulative)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Value).HasMaxLength(100);

                entity.HasOne(d => d.InsurerTerm)
                    .WithMany(p => p.InsurerTermDetails)
                    .HasForeignKey(d => d.InsurerTermId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsurerTermDetail_InsurerTerm");
            });

            modelBuilder.Entity<IssueSession>(entity =>
            {
                entity.ToTable("IssueSession");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.Icon)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Menu_Menu");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Menu_Permission");
            });

            modelBuilder.Entity<OnlinePayment>(entity =>
            {
                entity.ToTable("OnlinePayment");

                entity.Property(e => e.RefId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SaleReferenceId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.OnlinePayments)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OnlinePayment_PaymentId");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.PaymentCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentStatusId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDateTime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.PaymentGateway)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentGatewayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_PaymentGatewayId");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_PaymentStatusId");
            });

            modelBuilder.Entity<PaymentGateway>(entity =>
            {
                entity.ToTable("PaymentGateway");

                entity.Property(e => e.AllowManual)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AllowOnline)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.TerminalId)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaymentGatewayDetail>(entity =>
            {
                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Value).IsRequired();

                entity.HasOne(d => d.PaymentGateway)
                    .WithMany(p => p.PaymentGatewayDetails)
                    .HasForeignKey(d => d.PaymentGatewayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentGatewayDetails_PaymentGateway");
            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.ToTable("PaymentStatus");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FatherName).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.Identity).HasMaxLength(10);

                entity.Property(e => e.JobName)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.NationalCode).HasMaxLength(10);
            });

            modelBuilder.Entity<PersonAddress>(entity =>
            {
                entity.ToTable("PersonAddress");

                entity.Property(e => e.AddressTypeId).HasDefaultValueSql("((2))");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.PersonAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_PersonAddress_Address");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonAddresses)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PersonAddress_Person");
            });

            modelBuilder.Entity<PersonAttachment>(entity =>
            {
                entity.ToTable("PersonAttachment");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.PersonAttachments)
                    .HasForeignKey(d => d.AttachmentId)
                    .HasConstraintName("FK_PersonAttachment_AttachmentId");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonAttachments)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PersonAttachment_PersonId");
            });

            modelBuilder.Entity<PersonCompany>(entity =>
            {
                entity.ToTable("PersonCompany");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Position).HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PersonCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_PersonCompany_CompanyId");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_PersonCompany_PersonCompanyId");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonCompanies)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PersonCompany_PersonId");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("Place");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<PlaceAddress>(entity =>
            {
                entity.ToTable("PlaceAddress");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.PlaceAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_PlaceAddress_Address");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.PlaceAddresses)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("FK_PlaceAddress_Place");
            });

            modelBuilder.Entity<PolicyRequest>(entity =>
            {
                entity.ToTable("PolicyRequest");

                entity.Property(e => e.AgentSelectionTypeId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PolicyNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.AgentSelected)
                    .WithMany(p => p.PolicyRequests)
                    .HasForeignKey(d => d.AgentSelectedId)
                    .HasConstraintName("FK_PolicyRequest_AgentSelectedId");

                entity.HasOne(d => d.Insurer)
                    .WithMany(p => p.PolicyRequests)
                    .HasForeignKey(d => d.InsurerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyRequest_Insurer");

                entity.HasOne(d => d.PolicyRequestStatus)
                    .WithMany(p => p.PolicyRequests)
                    .HasForeignKey(d => d.PolicyRequestStatusId)
                    .HasConstraintName("FK_PolicyRequest_PolicyRequestStatusId");

                entity.HasOne(d => d.RequestPerson)
                    .WithMany(p => p.PolicyRequestRequestPeople)
                    .HasForeignKey(d => d.RequestPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyRequest_Person");

                entity.HasOne(d => d.Reviewer)
                    .WithMany(p => p.PolicyRequestReviewers)
                    .HasForeignKey(d => d.ReviewerId)
                    .HasConstraintName("FK_PolicyRequest_ReviewerId");
            });

            modelBuilder.Entity<PolicyRequestAttachment>(entity =>
            {
                entity.ToTable("PolicyRequestAttachment");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.PolicyRequestAttachments)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyRequestAttachment_Attachment");

                entity.HasOne(d => d.PolicyRequest)
                    .WithMany(p => p.PolicyRequestAttachments)
                    .HasForeignKey(d => d.PolicyRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyRequestAttachment_PolicyRequest");
            });

            modelBuilder.Entity<PolicyRequestComment>(entity =>
            {
                entity.ToTable("PolicyRequestComment");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnName("createdDateTime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.PolicyRequestComments)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyRequestComment_AuthorId");

                entity.HasOne(d => d.PolicyRequest)
                    .WithMany(p => p.PolicyRequestComments)
                    .HasForeignKey(d => d.PolicyRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyRequestComment_PolicyRequestId");
            });

            modelBuilder.Entity<PolicyRequestCommentAttachment>(entity =>
            {
                entity.ToTable("PolicyRequestCommentAttachment");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.PolicyRequestCommentAttachments)
                    .HasForeignKey(d => d.AttachmentId)
                    .HasConstraintName("FK_PolicyRequestCommentAttachment_AttachmentId");

                entity.HasOne(d => d.PolicyRequestComment)
                    .WithMany(p => p.PolicyRequestCommentAttachments)
                    .HasForeignKey(d => d.PolicyRequestCommentId)
                    .HasConstraintName("FK_PolicyRequestCommentAttachment_PolicyRequestCommentId");
            });

            modelBuilder.Entity<PolicyRequestDetail>(entity =>
            {
                entity.Property(e => e.CalculationType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Criteria)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Discount)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Field)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UserInput)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.InsurerTerm)
                    .WithMany(p => p.PolicyRequestDetails)
                    .HasForeignKey(d => d.InsurerTermId)
                    .HasConstraintName("FK_PolicyRequestDetails_InsurerTermId");

                entity.HasOne(d => d.PolicyRequest)
                    .WithMany(p => p.PolicyRequestDetails)
                    .HasForeignKey(d => d.PolicyRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyRequestDetails_PolicyRequest");
            });

            modelBuilder.Entity<PolicyRequestFactor>(entity =>
            {
                entity.ToTable("PolicyRequestFactor");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.PolicyRequestFactors)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_PolicyRequestFactor_PaymentId");

                entity.HasOne(d => d.PolicyRequest)
                    .WithMany(p => p.PolicyRequestFactors)
                    .HasForeignKey(d => d.PolicyRequestId)
                    .HasConstraintName("FK_PolicyRequestFactor_PolicyRequestId");
            });

            modelBuilder.Entity<PolicyRequestFactorDetail>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.PolicyRequestFactor)
                    .WithMany(p => p.PolicyRequestFactorDetails)
                    .HasForeignKey(d => d.PolicyRequestFactorId)
                    .HasConstraintName("FK_PolicyRequestFactorDetails_PolicyRequestFactorId");
            });

            modelBuilder.Entity<PolicyRequestHolder>(entity =>
            {
                entity.ToTable("PolicyRequestHolder");

                entity.Property(e => e.IssuedPersonRelation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.PolicyRequestHolders)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_PolicyRequestHolder_AddressId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PolicyRequestHolders)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_PolicyRequestHolder_Company");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PolicyRequestHolders)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_PolicyRequestHolder_Person");

                entity.HasOne(d => d.PolicyRequest)
                    .WithMany(p => p.PolicyRequestHolders)
                    .HasForeignKey(d => d.PolicyRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyRequestHolder_PolicyRequest");
            });

            modelBuilder.Entity<PolicyRequestInspection>(entity =>
            {
                entity.ToTable("PolicyRequestInspection");

                entity.Property(e => e.InspectionTypeId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CompanyCenterSchedule)
                    .WithMany(p => p.PolicyRequestInspections)
                    .HasForeignKey(d => d.CompanyCenterScheduleId)
                    .HasConstraintName("FK_PolicyRequestInspection_CompanyCenterScheduleId");

                entity.HasOne(d => d.InspectionAddress)
                    .WithMany(p => p.PolicyRequestInspections)
                    .HasForeignKey(d => d.InspectionAddressId)
                    .HasConstraintName("FK_PolicyRequesInspection_InspectionAddressId");

                entity.HasOne(d => d.InspectionSession)
                    .WithMany(p => p.PolicyRequestInspections)
                    .HasForeignKey(d => d.InspectionSessionId)
                    .HasConstraintName("FK_PolicyRequestInspection_InspectionSessionId");

                entity.HasOne(d => d.PolicyRequest)
                    .WithMany(p => p.PolicyRequestInspections)
                    .HasForeignKey(d => d.PolicyRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyRequesInspection_PolicyRequestId");
            });

            modelBuilder.Entity<PolicyRequestIssue>(entity =>
            {
                entity.ToTable("PolicyRequestIssue");

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.NeedPrint).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IssueSession)
                    .WithMany(p => p.PolicyRequestIssues)
                    .HasForeignKey(d => d.IssueSessionId)
                    .HasConstraintName("FK_PolicyRequestIssue_IssueSessionId");

                entity.HasOne(d => d.PolicyRequest)
                    .WithMany(p => p.PolicyRequestIssues)
                    .HasForeignKey(d => d.PolicyRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyRequestIssue_PolicyRequestId");

                entity.HasOne(d => d.ReceiverAddress)
                    .WithMany(p => p.PolicyRequestIssues)
                    .HasForeignKey(d => d.ReceiverAddressId)
                    .HasConstraintName("FK_PolicyRequestIssue_ReceiverAddressId");
            });

            modelBuilder.Entity<PolicyRequestStatus>(entity =>
            {
                entity.ToTable("PolicyRequestStatus");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<RegisterTemp>(entity =>
            {
                entity.ToTable("RegisterTemp");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.ToTable("Reminder");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Reminders)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Reminder_CityId");

                entity.HasOne(d => d.Insurance)
                    .WithMany(p => p.Reminders)
                    .HasForeignKey(d => d.InsuranceId)
                    .HasConstraintName("FK_Reminder_InsuranceId");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Reminders)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Reminder_PersonId");

                entity.HasOne(d => d.ReminderPeriod)
                    .WithMany(p => p.Reminders)
                    .HasForeignKey(d => d.ReminderPeriodId)
                    .HasConstraintName("FK_Reminder_ReminderPeriodId");
            });

            modelBuilder.Entity<ReminderPeriod>(entity =>
            {
                entity.ToTable("ReminderPeriod");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("Resource");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<ResourceOperation>(entity =>
            {
                entity.ToTable("ResourceOperation");

                entity.Property(e => e.Class).HasMaxLength(100);

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.ResourceOperations)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_ResourceOperation_PermissionId");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ResourceOperations)
                    .HasForeignKey(d => d.ResourceId)
                    .HasConstraintName("FK_ResourceOperation_ResourceId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Caption).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Role_Role");
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("RolePermission");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_RolePermission_Permission");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_RolePermission_Role");
            });

            modelBuilder.Entity<SchemaVersion>(entity =>
            {
                entity.Property(e => e.Applied).HasColumnType("datetime");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Sysdiagram>(entity =>
            {
                entity.HasKey(e => e.DiagramId)
                    .HasName("PK__sysdiagr__C2B05B612DAA14AE");

                entity.ToTable("sysdiagrams");

                entity.HasIndex(e => new { e.PrincipalId, e.Name }, "UK_principal_name")
                    .IsUnique();

                entity.Property(e => e.DiagramId).HasColumnName("diagram_id");

                entity.Property(e => e.Definition).HasColumnName("definition");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.PrincipalId).HasColumnName("principal_id");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<TownShip>(entity =>
            {
                entity.ToTable("TownShip");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.TownShips)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_TownShip_Province");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.ChangePasswordCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.IsEnable).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsLoginNotify).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsTwoStepVerification).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsVerified).HasDefaultValueSql("((0))");

                entity.Property(e => e.Password).HasMaxLength(200);

                entity.Property(e => e.TwoStepCode).HasMaxLength(10);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.VerificationCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_User_Person");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRole_User");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.VehicleBrand)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleBrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicle_VehicleBrand");

                entity.HasOne(d => d.VehicleRuleCategory)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleRuleCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicle_VehicleRuleCategoryId");
            });

            modelBuilder.Entity<VehicleApplication>(entity =>
            {
                entity.ToTable("VehicleApplication");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.VehicleApplications)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleApplication_VehicleTypeId");
            });

            modelBuilder.Entity<VehicleBrand>(entity =>
            {
                entity.ToTable("VehicleBrand");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.VehicleBrands)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleBrand_VehicleType");
            });

            modelBuilder.Entity<VehicleRuleCategory>(entity =>
            {
                entity.ToTable("VehicleRuleCategory");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VehicleType");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
