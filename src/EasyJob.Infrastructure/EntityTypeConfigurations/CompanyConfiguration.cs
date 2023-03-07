using EasyJob.Domain.Constants;
using EasyJob.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyJob.Infrastructure.EntityTypeConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(TableName.Companies);

            builder.HasKey(comp => comp.UserId);

            builder
            .HasOne(admin => admin.User)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
