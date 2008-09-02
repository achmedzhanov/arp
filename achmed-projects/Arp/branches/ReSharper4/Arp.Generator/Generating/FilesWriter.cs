using System;
using System.IO;
using System.Reflection;
using log4net;

namespace Arp.Generator.Generating
{
    public class FilesWriter : IFilesWriter
    {
        private static readonly ILog log = LogManager.GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);
        
        private readonly string targetDirectory;

        public FilesWriter(string targetDirectory)
        {
            this.targetDirectory = targetDirectory;
        }

        #region IFilesWriter Members

        public void Write(string fileName, string content)
        {
            if(Path.IsPathRooted(fileName))
                throw new ArgumentException("expected relative file name");

            string combined = Path.Combine(targetDirectory, fileName);
            string directoryPath = Path.GetDirectoryName(combined);
            if(!Directory.Exists(directoryPath))
            {
                #region Logging

                if (log.IsInfoEnabled)
                {
                    log.Info("Create Directory " + directoryPath);
                }

                #endregion

                Directory.CreateDirectory(directoryPath);
            }

            #region Logging

            if (log.IsInfoEnabled)
            {
                log.Info("Write file " + combined);
            }

            #endregion

            File.WriteAllText(combined, content);
        }


        public bool Exists(string fileName)
        {
            if (Path.IsPathRooted(fileName))
                throw new ArgumentException("expected relative file name");

            string combined = Path.Combine(targetDirectory, fileName);

            return File.Exists(combined);
        }

        #endregion



    }
}