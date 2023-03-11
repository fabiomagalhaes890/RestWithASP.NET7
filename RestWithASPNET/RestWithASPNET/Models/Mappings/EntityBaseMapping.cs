using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestWithASPNET.Models.Mappings
{
    public class EntityBaseMapping<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public Guid Id { get; set; }
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
