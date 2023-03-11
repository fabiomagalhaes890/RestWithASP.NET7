using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestWithASPNET.Models.Mappings
{
    public class PeopleMapping : EntityBaseMapping<People>
    {
        public override void Configure(EntityTypeBuilder<People> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Enabled).IsRequired(false);
        }
    }
}
