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

            builder.HasNoKey();

            builder
                .HasOne(comp => comp.User)
                .WithOne()
                .HasForeignKey<Company>(comp => comp.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
