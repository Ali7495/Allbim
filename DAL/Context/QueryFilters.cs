using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public partial class AlbimDbContext : DbContext
    {
        public void OnModelFilteringDeleted(ModelBuilder builder)
        {
            builder.Entity<Person>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<PersonAttachment>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<PersonAddress>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<User>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Company>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<CompanyAddress>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Role>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Attachment>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Address>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<PolicyRequestComment>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<PolicyRequestStatus>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<PersonCompany>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Discount>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Article>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Insurance>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<ArticleMetaKey>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Info>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<InsuranceFaq>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Category>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<ArticleCategory>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Province>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<City>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<PolicyRequestCommentAttachment>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<InsuranceFrontTab>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<CompanyAgent>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<ArticleType>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Resource>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<ResourceOperation>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<ContactUs>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Payment>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<PaymentStatus>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<PaymentGateway>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<PaymentGatewayDetail>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<Faq>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<PolicyRequestFactor>().HasQueryFilter(b => !b.IsDeleted);
            builder.Entity<PolicyRequestFactorDetail>().HasQueryFilter(b => !b.IsDeleted);
        }
    }
}