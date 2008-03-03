using System;
using System.IO;

namespace Arp.Generator.Generating
{
    public class FilesWriter : IFilesWriter
    {
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
                throw new InvalidOperationException("directory doesn't exists " + directoryPath);

            File.WriteAllText(combined, content);
        }

        #endregion
    }
}