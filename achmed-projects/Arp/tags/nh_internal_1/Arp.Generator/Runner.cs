using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;
using Arp.Generator.Acceptors;
using Arp.Generator.Generating;
using Arp.Generator.Preprocessing;
using Arp.Generator.Preprocessing.Impl;
using Arp.Generator.Names;

namespace Arp.Generator
{
    public class Runner
    {
        private string xsdFile;
        private string outputDirectory;
        private string baseNamespace;
        private readonly EnumAcceptor enumAcceptor = new EnumAcceptor();
        private ElementAcceptor elementAcceptor = new ElementAcceptor();

        public string XsdFile
        {
            get { return xsdFile; }
            set { xsdFile = value; }
        }

        public string OutputDirectory
        {
            get { return outputDirectory; }
            set { outputDirectory = value; }
        }


        public string BaseNamespace
        {
            get { return baseNamespace; }
            set { baseNamespace = value; }
        }

        public void Run()
        {
            XmlSchema schema = CreateXmlSchema(xsdFile);
            PreprocesingVisitor visitor = CreateGeneratorVisitor();
            visitor.VisitSchema(schema);

            Arp.Generator.Generating.TreeElements treeElements = new Arp.Generator.Generating.TreeElements();

            treeElements.FileWriter = new FilesWriter(outputDirectory);
            treeElements.NameConverter = new CamelNameConverter();
            treeElements.PluralProvider = new DictionaryPluralProvider();

            // TODO get types froim nested elements!

            List<ITypeInfo> types = new List<ITypeInfo>();
            foreach (IElementInfo elementInfo in elementAcceptor.Elements)
            {
                treeElements.GenerateElementInterface(elementInfo.TypeInfo);
                treeElements.GenerateElementImpl(elementInfo.TypeInfo);
                if(!types.Contains(elementInfo.TypeInfo))
                    types.Add(elementInfo.TypeInfo);
            }

            treeElements.GenerateCompositeElementTypes(types);
            treeElements.GenerateCompositeElementTypeInstrances(types);

            treeElements.GenerateElementsFactory(elementAcceptor.Elements);
        }

        private XmlSchema CreateXmlSchema(string file)
        {
            using(FileStream fileStream =  File.Open(file,FileMode.Open))
            {
                XmlSchema xmlSchema = XmlSchema.Read(fileStream, ValidationEventHandler);
                xmlSchema.Compile(ValidationEventHandler);
                return xmlSchema;                
            }
        }

        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error)
                throw new Exception("", e.Exception);
        }

        protected PreprocesingVisitor CreateGeneratorVisitor()
        {
            PreprocesingVisitor preprocesingVisitor = new PreprocesingVisitor();
            preprocesingVisitor.NameConverter = new CamelNameConverter();
            preprocesingVisitor.TargetNamespace = BaseNamespace;
            preprocesingVisitor.EnumAcceptor = this.enumAcceptor;
            preprocesingVisitor.ElementsAcceptor = this.elementAcceptor;
            return preprocesingVisitor;
        }


        class EnumAcceptor : IEnumAcceptor
        {
            readonly List<EnumGenerationInfo> enums = new List<EnumGenerationInfo>();

            #region IEnumAcceptor Members

            public void Accept(EnumGenerationInfo enumGenerationInfo)
            {
                enums.Add(enumGenerationInfo);
            }

            #endregion

            public List<EnumGenerationInfo> Enums
            {
                get { return enums; }
            }
        }

        class ElementAcceptor : IElementsAcceptor
        {
            readonly List<IElementInfo> elements = new List<IElementInfo>();

            #region IElementsAcceptor Members

            public void Accept(IElementInfo elementGenerationInfo)
            {
                elements.Add(elementGenerationInfo);
            }

            #endregion

            public List<IElementInfo> Elements
            {
                get { return elements; }
            }
        }


    }
}