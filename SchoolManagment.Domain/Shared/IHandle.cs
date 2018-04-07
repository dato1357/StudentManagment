using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagment.Domain.Shared
{
    public interface IHandle<T> where T : DomainEvent
    {
        void Handle(T @event);
    }
}
