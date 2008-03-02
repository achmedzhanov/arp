using Antlr.StringTemplate;
using Arp.Common.Assertions;
using Arp.Generator.Preprocessing.Impl;

namespace Arp.Generator.Generating
{
    public class TreeInterfaces
    {
        public const string INTERFACE_DECLARATION =
@"
namespace $namespace$
{
    public interface $name$ : IElement 
    {
$body$ 
    }
}
";

        public const string PROPERTY_DECLARATION =
@"
        $type$ $name$
        {
            get;
        }
}";

        private IFilesWriter fileWriter;


        public IFilesWriter FileWriter
        {
            get { return fileWriter; }
            set { fileWriter = value; }
        }

        public void Generate(TypeGenerationInfo typeGenerationInfo)
        {
            //Assert.CheckNotNull(fileWriter);

            StringTemplate stringTemplate = new StringTemplate(INTERFACE_DECLARATION);
            stringTemplate.SetAttribute("namespace", typeGenerationInfo.TypeName.Namespace);
            stringTemplate.SetAttribute("name", typeGenerationInfo.TypeName.ShortName);
            stringTemplate.SetAttribute("body", ";;");
            string ret = stringTemplate.ToString();
            fileWriter.Write(typeGenerationInfo.TypeName.ShortName + ".cs", ret);
        }
    }
}