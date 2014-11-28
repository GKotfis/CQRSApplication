using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSApplication.Events
{
    public interface IEventsBus
    {
        void PublishEvent<T>(T cmd) where T : IEvent;
    }
}
