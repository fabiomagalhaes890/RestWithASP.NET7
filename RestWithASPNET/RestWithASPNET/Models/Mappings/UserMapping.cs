using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestWithASPNET.Models.Mappings
{
    public class UserMapping : EntityBaseMapping<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.UserName).IsRequired();
            builder.Property(p => p.FullName).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.Password).IsRequired();
            builder.Property(p => p.RefreshToken).IsRequired(false);
            builder.Property(p => p.RefreshTokenExpiryTime).IsRequired(false);
        }
    }
}
