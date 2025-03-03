using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AppPrimiani.Api.Data.Mappings.Identity
{
    public class IdentityRoleCaimMapping : IEntityTypeConfiguration<IdentityRoleClaim<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<long>> builder)
        {
            builder.ToTable("IdentityRoleClaim");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClaimType).HasMaxLength(255);
            builder.Property(x => x.ClaimValue).HasMaxLength(255);
        }
    }
}
