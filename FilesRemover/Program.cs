using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilesRemoverLib;

namespace FilesRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = FilesRemoverLib.FilesRemoverLib.Constants.APPLICATION_TITLE;
            string appID = FilesRemoverLib.FilesRemoverLib.Constants.ID;
            if(appID != null)
            {
                HeaderApplication(appID);
                MainApplication(appID);
            }
            ExitingApplication(appID);
        }

        private static void HeaderApplication(string appID)
        {
            if (appID.Equals(FilesRemoverLib.FilesRemoverLib.Constants.ID))
            {
                Console.WriteLine(FilesRemoverLib.FilesRemoverLib.Constants.APPLICATION_TITLE);
                Console.WriteLine(FilesRemoverLib.FilesRemoverLib.Constants.APPLICATION_TITLE_UNDERLINE);
                Console.WriteLine();
            }
            else
            {
                throw new Exception(FilesRemoverLib.FilesRemoverLib.Message.DONT_HAVE_PERMISSION_TO_ACCESS_HEADER_APP);
            }
        }

        private static void MainApplication(string appID)
        {
            string targetDir = ReadFilesRemoverConf(appID);
            if (targetDir.Equals(FilesRemoverLib.FilesRemoverLib.ErrorCode.CONFIGURATION_FILE_NOT_FOUND))
            {
                CreateNewConf(appID);
            }
            else
            {
                if (WantToReconfigApplication())
                {
                    ReconfigureApplication();
                    targetDir = ReadFilesRemoverConf(appID);
                }
                RemoveFiles(appID, targetDir);
            }
        }

        private static string ReadFilesRemoverConf(string iD)
        {
            if (iD.Equals(FilesRemoverLib.FilesRemoverLib.Constants.ID))
            {
                try
                {
                    StreamReader sr = new StreamReader(FilesRemoverLib.FilesRemoverLib.Constants.CONFIGURATION_FILE_NAME);
                    string srLine = @sr.ReadLine();
                    sr.Close();
                    return srLine;
                }
                catch (Exception e)
                {
                    return FilesRemoverLib.FilesRemoverLib.ErrorCode.CONFIGURATION_FILE_NOT_FOUND;
                }
            }
            else
            {
                throw new Exception(FilesRemoverLib.FilesRemoverLib.Message.DONT_HAVE_PERMISSION_TO_READ_CONFIGURATION_FILE);
            }
        }

        private static void CreateNewConf(string appID)
        {
            string targetDir = string.Empty;

            Console.WriteLine(FilesRemoverLib.FilesRemoverLib.Message.CONFIGURATION_FILE_NOT_FOUND);
            Console.Write(FilesRemoverLib.FilesRemoverLib.Message.ENTER_DIRECTORY_TARGET);
            targetDir = Console.ReadLine();
            WriteToConfFile(targetDir);
            Console.WriteLine(FilesRemoverLib.FilesRemoverLib.Message.CONFIGURATION_COMPLETED);
            RemoveFiles(appID, targetDir);
        }
        
        private static bool WantToReconfigApplication()
        {
            Console.Write(FilesRemoverLib.FilesRemoverLib.Message.ASK_TO_RECONFIGURE_APPLICATION);
            string answer = Console.ReadLine().Trim().ToUpper();
            if (answer.Equals(FilesRemoverLib.FilesRemoverLib.Constants.ANSWER_YES))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void ReconfigureApplication()
        {
            string targetDir = string.Empty;
            Console.WriteLine("Current path is "+ReadFilesRemoverConf(FilesRemoverLib.FilesRemoverLib.Constants.ID));
            Console.Write(FilesRemoverLib.FilesRemoverLib.Message.ENTER_NEW_DIRECTORY_TARGER);
            targetDir = Console.ReadLine();
            WriteToConfFile(targetDir);
            Console.WriteLine(FilesRemoverLib.FilesRemoverLib.Message.RECONFIGURATION_COMPLETED);
        }

        private static void WriteToConfFile(string targetDir)
        {
            StreamWriter sw = new StreamWriter(FilesRemoverLib.FilesRemoverLib.Constants.CONFIGURATION_FILE_NAME);
            sw.Write(@targetDir);
            sw.Close();
        }

        private static void RemoveFiles(string appID, string targetDir)
        {
            Console.WriteLine();
            Console.Write(FilesRemoverLib.FilesRemoverLib.Message.ASK_TO_DELETE_FILES);
            string answer = Console.ReadLine().Trim().ToUpper();
            if (answer.Equals(FilesRemoverLib.FilesRemoverLib.Constants.ANSWER_YES))
            {
                ProcessToRemoveFiles(targetDir);
            }
        }

        private static void ProcessToRemoveFiles(string targetDir)
        {
            if (System.IO.Directory.Exists(@targetDir))
            {
                int numberOfFiles = Directory.GetFiles(targetDir, "*.*", SearchOption.AllDirectories).Length;
                string strCommand = "del /s/q \"" + targetDir + "\"";
                strCommand = strCommand + " && " + "rmdir /s/q \"" + targetDir +"\"";
                strCommand = strCommand + " && " + "mkdir \"" + targetDir + "\"";
                Process.Start("cmd.exe", "/c " + @strCommand);
                Console.WriteLine("RESULT : Deleting " + numberOfFiles + " files in " + targetDir + " completed.");
            }
            else
            {
                Console.WriteLine("RESULT : Cannot delete " + targetDir + " because directory is not exists or Application doesnt have permission to access directory.");
            }
        }

        private static void ExitingApplication(string appID)
        {
            Console.WriteLine();
            Console.Write(FilesRemoverLib.FilesRemoverLib.Message.PRESS_ANY_KEY_TO_EXIT);
            Console.ReadKey();
        }
    }
}
