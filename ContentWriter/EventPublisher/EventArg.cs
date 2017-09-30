using System;


namespace EventPublisher
{
   public class EventArg
    {
       public EventArg(Guid eventId, object arg)
       {
           EventId = eventId;
           Arg = arg;
       }
       public Guid EventId { get; private set; }
      
       public object Arg { get; private set; }
    }

    
}
