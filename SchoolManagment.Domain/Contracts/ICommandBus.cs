using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SchoolManagment.Domain.Shared;

namespace SchoolManagment.Domain.Contracts
{
    public interface ICommandBus
    {
        void Dispatch(ICommand command);
    }
}
