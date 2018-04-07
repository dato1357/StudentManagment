using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagment.Domain.Shared
{
    public interface ICommandHandler<in Command> where Command : ICommand
    {
        CommandResult Execute(Command command);
    }
}
