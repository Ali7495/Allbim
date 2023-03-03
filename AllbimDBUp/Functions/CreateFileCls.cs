using DbUp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AllbimDBUp.Functions
{
    public class CreateFileCls
    {
        public bool CreateFileMethod(string FileName , string FolderName)
        {
            try
            {
                string ProjectPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string ScriptsFolderPath = ProjectPath.Replace("bin\\Debug\\DbUpdate.exe", FolderName == "Scripts" || FolderName == "-" ? "Scripts" : "LogScripts");
                ScriptsFolderPath = ScriptsFolderPath.Replace("\\", "/");
                // Create a new file     
                using (FileStream fs = File.Create(ScriptsFolderPath +"/"+ FileName))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes("");
                    fs.Write(title, 0, title.Length);
                }
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("File Creation Done... ");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ExecScripts()
        {

            string ConnectionString = ""; //ConfigurationManager.ConnectionStrings["AlbimDbCnnLocal"].ConnectionString;
            var Executer = DeployChanges.To.SqlDatabase(ConnectionString)
                 .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                 (string script) => script.StartsWith($"DbUpdate.Scripts")
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
            string ConnectionStringLog = ""; //ConfigurationManager.ConnectionStrings["AlbimLogDbCnnLocal"].ConnectionString;

            var ExecuterLog = DeployChanges.To.SqlDatabase(ConnectionStringLog)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                    (string script) => script.StartsWith("DbUpdate.LogScripts")
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
            Console.ReadLine();
        }
    }
}
