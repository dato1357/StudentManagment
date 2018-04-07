namespace SchoolManagment.Domain.Shared
{
    public class CommandResult
    {
        public CommandResult(CommandResultStatus status, string message)
        {
            CommandResultStatus = status;
            Message = message;
        }
        public CommandResultStatus CommandResultStatus { get;}
        public string Message { get;}
    }
}