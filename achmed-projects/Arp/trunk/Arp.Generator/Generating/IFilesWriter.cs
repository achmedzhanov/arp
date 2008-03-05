using System.IO;

namespace Arp.Generator.Generating
{
    public interface IFilesWriter
    {
        void Write(string fileName, string content);

        bool Exists(string fileName);

    }
}