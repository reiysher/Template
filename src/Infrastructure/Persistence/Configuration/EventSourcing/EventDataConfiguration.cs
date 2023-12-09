﻿using Infrastructure.Persistence.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.EventSourcing;

internal class EventDataConfiguration
    : IEntityTypeConfiguration<EventData>
{
    public void Configure(EntityTypeBuilder<EventData> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable("events", "event_sourcing");

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Type)
            .HasMaxLength(1024)
            .IsRequired();

        builder.Property(e => e.Version)
            .IsRequired();

        builder.Property(e => e.Payload)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.HasIndex(e => new { e.StreamId, e.Version })
            .IsUnique();
    }
}
