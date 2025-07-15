using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuaveChallenge.API.Models;

namespace QuaveChallenge.API.Data.Configurations
{
    public class CheckinInformationConfiguration : IEntityTypeConfiguration<CheckinInformation>
    {
        public void Configure(EntityTypeBuilder<CheckinInformation> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CheckinTime);
            builder.Property(p => p.CheckoutTime);

            builder.HasOne(p => p.Person)
                .WithMany()
                .HasForeignKey(p => p.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
