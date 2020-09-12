namespace Aviant.DDD.Infrastructure.Persistence.Configurations
{
    #region

    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    #endregion

    public class EntityConfiguration<TEntity, T> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity<T>
    {
        #region IEntityTypeConfiguration<TEntity> Members

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);
        }

        #endregion
    }
}