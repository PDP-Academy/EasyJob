using EasyJob.Domain.Constants;
using EasyJob.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyJob.Infrastructure.EntityTypeConfigurations;
public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.ToTable(TableName.Admins);

        builder
            .HasOne(admin => admin.User)
            .WithOne()
            .HasForeignKey<Admin>(admin => admin.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
