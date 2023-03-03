using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllbimDBUp.Commands
{
    public static class CommandsCls
    {
        public enum CommandsEnums
        {
            [Description("AddMigration")] AddMigration,
            [Description("ExecMigration")] ExecMigration
        }

        public static string[] SplitCommand(string ConsoleCommand)
        {
            return ConsoleCommand.Split(new Char[] { ' ' });
        }
        public static string SetFileName(string FileName)
        {
            return DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_"  + DateTime.Now.Minute.ToString() + "_" + FileName + ".sql";
        }
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
       where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        public static String GetDescription(this Enum value)
        {
            var EnumStringValue = GetAttribute<DescriptionAttribute>(value);
            return EnumStringValue != null ? EnumStringValue.Description : null;
        }

    }
}
