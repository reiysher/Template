using Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Subscriptions;

internal class SubscriptionDashboardConfiguration
    : IEntityTypeConfiguration<SubscriptionDashboard>
{
    public void Configure(EntityTypeBuilder<SubscriptionDashboard> builder)
    {
        builder.HasKey(p => p.Id);
        builder.ToTable("dashboard_subscriptions");

        builder.Property(p => p.Id)
            .ValueGeneratedNever();
    }
}
