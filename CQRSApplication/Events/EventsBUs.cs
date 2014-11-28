using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSApplication.Events
{
    public class EventsBus : IEventsBus
    {

        private readonly Func<Type, IEnumerable<IHandleEvent>> _handlersFactory;

        public EventsBus(Func<Type, IEnumerable<IHandleEvent>> handlersFactory)
        {
            _handlersFactory = handlersFactory;
        }

        public void PublishEvent<T>(T @event) where T : IEvent
        {
            var handlers = _handlersFactory(typeof(T)).Cast<IHandleEvent<T>>();

            foreach (var handler in handlers)
                handler.Handle(@event);
        }
    }
}
