using DbUp;
using System;
using System.Reflection;

namespace AllbimDBUp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            string dbConn = args[0];
            string logConn = args[1];


            Console.WriteLine("AlbimDb upgrade");
            // Runner ConsoleCommadRunner = new Runner();
            //Console.WriteLine("Enter your Command : ");
            //string ConcoleCommand = Console.ReadLine();
            //Sample Command : AddMigration TestFile Scripts|LogScripts
            //ConsoleCommadRunner.ExecRunner(ConcoleCommand);

            var Executer = DeployChanges.To.SqlDatabase(dbConn)
                 .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                 (string script) => script.StartsWith($"AllbimDBUp.Scripts")
                 ).LogToConsole().Build();
            var Result = Executer.PerformUpgrade();

            if (!Result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Result.Error);
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("AllbimDb Scripts apllied Successfully");
            }



            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("AlbimLogDb upgrade:");
            Console.ResetColor();

            var ExecuterLog = DeployChanges.To.SqlDatabase(logConn)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                    (string script) => script.StartsWith("AllbimDBUp.LogScripts")
                ).LogToConsole().Build();


            var ResultLog = ExecuterLog.PerformUpgrade();

            if (!ResultLog.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ResultLog.Error);
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("AllbimLogDb Scripts apllied Successfully");
            }



            Console.ResetColor();
            // Console.ReadLine();
        }
    }
}
