using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FilesRemoverLib
{
    public class FilesRemoverLib
    {
        public class Constants
        {
            public const string ID = "FilesRemover 1.0";
            public const string APPLICATION_TITLE = "Files Remover 1.0";
            public const string APPLICATION_TITLE_UNDERLINE = "=================";
            public const string CONFIGURATION_FILE_NAME = "FilesRemoverConf.DAT";
            public const string ANSWER_YES = "Y";
        }

        public class ErrorCode
        {
            public const string CONFIGURATION_FILE_NOT_FOUND = "ERROR(001)";
        }

        public class Message
        {
            public const string DONT_HAVE_PERMISSION_TO_ACCESS_HEADER_APP = "You not have permission to access HeaderApplication()!";
            public const string DONT_HAVE_PERMISSION_TO_READ_CONFIGURATION_FILE = "You not have permission to access ReadFilesRemoverConf()!";
            public const string CONFIGURATION_FILE_NOT_FOUND = "Application cannot find configuration files, you must configure this app before use.";
            public const string ENTER_DIRECTORY_TARGET = "Enter path as target : ";
            public const string ENTER_NEW_DIRECTORY_TARGER = "Enter new path as target : ";
            public const string CONFIGURATION_COMPLETED = "Configuration completed.";
            public const string RECONFIGURATION_COMPLETED = "Reconfiguration completed.";
            public const string ASK_TO_DELETE_FILES = "Start deleting files? (Y/N) ";
            public const string ASK_TO_RECONFIGURE_APPLICATION = "You want to change target directory ? (Y/N) ";
            public const string PRESS_ANY_KEY_TO_EXIT = "Press any key to exit..";
        }
    }
}
