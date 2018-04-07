using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using SchoolManagment.Domain.Contracts;
using SchoolManagment.Domain.Shared;

namespace SchoolManagment.Infrastructure.Bus
{
    public class CommandBus : ICommandBus
    {
        Dictionary<Type, Type> handlers;
        MethodInfo dispatchCommand;

        public CommandBus()
        {
            this.handlers = RegisterCommandHandlers();
            this.dispatchCommand = GetType().GetMethod("DispatchCommand", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public void Dispatch(ICommand command)
        {
            var cmdType = command.GetType();
            var handler = Activator.CreateInstance(handlers[cmdType]);
            var genericMethod = dispatchCommand.MakeGenericMethod(cmdType);
            genericMethod.Invoke(this, new object[] { handler, command });
        }

        void DispatchCommand<T>(ICommandHandler<T> handler, T command) where T : class, ICommand
        {
             handler.Execute(command);
        }

        Dictionary<Type, Type> RegisterCommandHandlers()
        {
            Func<Type, bool> isCommandHandler = t =>
                t.GetInterfaces()
                    .Any(i => i.IsGenericParameter && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>));

            Func<Type, IEnumerable<Tuple<Type, Type>>> collect = t =>
                t.GetInterfaces().Select(i =>
                    Tuple.Create(i.GetGenericArguments()[0], t));

            return Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(isCommandHandler)
                .SelectMany(collect)
                .ToDictionary(x => x.Item1, x => x.Item2);
        }
    }
}
