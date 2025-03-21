using System.Collections.Generic;
using System.Linq;
using JasperFx.Events;

namespace Marten.Events.Daemon.Internals;

public class EventPage: List<IEvent>
{
    public EventPage(long floor)
    {
        Floor = floor;
    }

    public long Floor { get; }
    public long Ceiling { get; private set; }

    public long HighWaterMark { get; set; }

    public void CalculateCeiling(int batchSize, long highWaterMark, int skippedEvents = 0)
    {
        Ceiling = (Count + skippedEvents) == batchSize
            ? this.Last().Sequence
            : highWaterMark;
    }
}
