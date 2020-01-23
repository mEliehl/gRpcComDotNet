using System.IO;

namespace Client
{
    public class FileSystemWatcherWrapper : FileSystemWatcher
    {

        public FileSystemWatcherWrapper(string path, FileSystemEventHandler created)
            : base()
        {
            Path = path;
            NotifyFilter = NotifyFilters.LastAccess
                        | NotifyFilters.LastWrite
                        | NotifyFilters.FileName
                        | NotifyFilters.DirectoryName;

            // Only watch csv files.
            Filter = "*.csv";
            Created += created;

            // Begin watching.
            EnableRaisingEvents = true;
        }
    }
}