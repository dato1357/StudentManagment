using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagment.Domain.Shared
{
    public interface ICommandValidator<in TCommand> where TCommand : ICommand
    {
        IEnumerable<List<string>> Validate(TCommand command);
    }
}
