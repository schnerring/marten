using System;
using JasperFx.CodeGeneration;
using JasperFx.Core.Reflection;
using JasperFx.Events;
using Marten.Exceptions;
using Marten.Schema;

namespace Marten.Events.Daemon;

[SingleTenanted]
public class DeadLetterEvent
{
    public DeadLetterEvent()
    {
    }

    public DeadLetterEvent(IEvent e, ShardName shardName, ApplyEventException ex)
    {
        ProjectionName = shardName.ProjectionName;
        ShardName = shardName.Key;
        Timestamp = DateTimeOffset.UtcNow;
        ExceptionMessage = ex.Message;

        EventSequence = e.Sequence;

        ExceptionType = ex.InnerException?.GetType().NameInCode();
    }

    public Guid Id { get; set; }
    public string ProjectionName { get; set; }
    public string ShardName { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string ExceptionMessage { get; set; }
    public string ExceptionType { get; set; }
    public long EventSequence { get; set; }

    public override string ToString()
    {
        return
            $"{nameof(ProjectionName)}: {ProjectionName}, {nameof(ShardName)}: {ShardName}, {nameof(EventSequence)}: {EventSequence}";
    }
}
