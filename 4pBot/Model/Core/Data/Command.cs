using System;

namespace pBot.Model.Core
{
    /// <summary>
    ///     Empty string mean any value
    /// </summary>
    public class Command
    {
        public enum CommandType
        {
            Default,
            Negation,
            Any
        }

        private static readonly Command EmptyCommand = new Command("", "", CommandType.Default);

        public Command(string sender, string actionName, CommandType commandType, params string[] parameters)
        {
            if (sender == null || actionName == null || parameters == null)
            {
                throw new ArgumentNullException();
            }

            Sender = sender;
            TypeOfCommand = commandType;
            ActionName = actionName;
            Parameters = parameters;
        }

        public static string Any => string.Empty;

        public string Sender { get; }
        public CommandType TypeOfCommand { get; }
        public string ActionName { get; }
        public string[] Parameters { get; }

        public static Command Empty()
        {
            return EmptyCommand;
        }

        public static bool operator ==(Command command1, Command command2)
        {
            var isActionNamesEquals = command2.ActionName.Equals(Any) || command1.ActionName.Equals(Any) ||
                                      command1.ActionName.Equals(command2.ActionName, StringComparison.OrdinalIgnoreCase);

            var isNegationEquals = command1.TypeOfCommand.Equals(command2.TypeOfCommand)
                                   || command1.TypeOfCommand == CommandType.Any
                                   || command2.TypeOfCommand == CommandType.Any;

            var isSenderEquals = command1.Sender.Equals(command2.Sender, StringComparison.OrdinalIgnoreCase) ||
                                 command1.Sender.Equals(Any) ||
                                 command2.Sender.Equals(Any);

            var areParametersEqual = true;

            for (var i = 0; i < command1.Parameters.Length || i < command2.Parameters.Length; i++)
            {
                if (command1.Parameters.Length <= i || command2.Parameters.Length <= i)
                {
                    if (command1.Parameters.Length <= i && command2.Parameters?[i] == Any)
                    {
                        break;
                    }
                    if (command2.Parameters.Length <= i && command1.Parameters?[i] == Any)
                    {
                        break;
                    }
                    areParametersEqual = false;
                    break;
                }

                if (command1.Parameters[i] == Any || command2.Parameters[i] == Any)
                {
                    continue;
                }

                areParametersEqual = command1.Parameters[i].Equals(command2.Parameters[i],
                    StringComparison.OrdinalIgnoreCase);
            }

            return isSenderEquals && isNegationEquals && isActionNamesEquals && areParametersEqual;
        }

        public bool Equals(Command command)
        {
            return this == command;
        }

        public override bool Equals(object obj)
        {
            if (obj is Command)
            {
                return Equals((Command) obj);
            }

            return base.Equals(obj);
        }

        public static bool operator !=(Command command1, Command command2)
        {
            return !(command1 == command2);
        }

        public override string ToString()
        {
            return $"{Sender} {ActionName} {TypeOfCommand} {string.Join(" ", Parameters)}";
        }
    }
}