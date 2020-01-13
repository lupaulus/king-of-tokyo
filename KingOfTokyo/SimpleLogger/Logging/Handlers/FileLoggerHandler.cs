using System;
using System.IO;
using System.Threading;
using SimpleLogger.Logging.Formatters;

namespace SimpleLogger.Logging.Handlers
{
    public class FileLoggerHandler : ILoggerHandler
    {
        private readonly string _fileName;
        private readonly string _directory;
        private readonly ILoggerFormatter _loggerFormatter;
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

        public FileLoggerHandler() : this(CreateFileName()) { }

        public FileLoggerHandler(string fileName) : this(fileName, string.Empty) { }

        public FileLoggerHandler(string fileName, string directory) : this(new DefaultLoggerFormatter(), fileName, directory) { }

        public FileLoggerHandler(ILoggerFormatter loggerFormatter) : this(loggerFormatter, CreateFileName()) { }

        public FileLoggerHandler(ILoggerFormatter loggerFormatter, string fileName) : this(loggerFormatter, fileName, string.Empty) { }

        public FileLoggerHandler(ILoggerFormatter loggerFormatter, string fileName, string directory)
        {
            _loggerFormatter = loggerFormatter;
            _fileName = fileName;
            _directory = directory;
        }

        public void Publish(LogMessage logMessage)
        {
            if (!string.IsNullOrEmpty(_directory))
            {
                var directoryInfo = new DirectoryInfo(Path.Combine(_directory));
                if (!directoryInfo.Exists)
                    directoryInfo.Create();
            }


            _readWriteLock.EnterWriteLock();
            StreamWriter writer = null;

            try
            {
                // Append text to the file
                writer = new StreamWriter(File.Open(Path.Combine(_directory, _fileName), FileMode.Append));
                writer.WriteLine(_loggerFormatter.ApplyFormat(logMessage));
                writer.Close();
            }
            finally
            {
                // Release lock
                if (writer != null)
                {
                    writer.Close();
                }

            }
            _readWriteLock.ExitWriteLock();

        }

        private static string CreateFileName()
        {
            var currentDate = DateTime.Now;
            var guid = Guid.NewGuid();
            return string.Format("Log_{0:0000}{1:00}{2:00}-{3:00}{4:00}_{5}.log",
                currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, guid);
        }
    }
}
