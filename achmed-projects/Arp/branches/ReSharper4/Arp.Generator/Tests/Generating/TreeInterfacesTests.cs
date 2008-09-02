                    using System;
using System.IO;
using System.Net.Mime;
using System.Xml.Schema;
using Arp.Generator.Generating;
using Arp.Generator.Names;
using Arp.Generator.Preprocessing;
using Arp.Generator.Preprocessing.Impl;
using log4net.Config;
using NUnit.Framework;

namespace Arp.Generator.Tests.Generating
{
    [TestFixture]
    public class TreeInterfacesTests : BaseTestFixture
    {
        [SetUp]
        public void Setup()
        {
            BasicConfigurator.Configure();
        }

        [Test]
        public void GenerateInterface()
        {
            #region schema

            string xsd =
                @"<xs:schema targetNamespace=""urn:nhibernate-mapping-2.2"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns=""urn:nhibernate-mapping-2.2"" elementFormDefault=""qualified"" attributeFormDefault=""unqualified"">
	<xs:simpleType name=""polymorphismType"">
		<xs:annotation>
			<xs:documentation>Types of polymorphism</xs:documentation>
		</xs:annotation>
		<xs:restriction base=""xs:string"">
			<xs:enumeration value=""implicit"" />
			<xs:enumeration value=""explicit"" />
		</xs:restriction>
	</xs:simpleType>
	<xs:element name=""class"">
		<xs:annotation>
			<xs:documentation>Root of an entity class hierarchy. Entities have their own tables</xs:documentation>
		</xs:annotation>
		<xs:complexType>
			<xs:sequence>
				<xs:choice minOccurs=""0"">
					<xs:element ref=""jcs-cache"" />
					<xs:element ref=""cache"" />
				</xs:choice>
				<xs:element ref=""discriminator"" minOccurs=""0"" />
				<xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
					<xs:element ref=""property"" />
					<xs:element ref=""many-to-one"" />
				</xs:choice>
				<xs:element ref=""filter"" minOccurs=""0"" maxOccurs=""unbounded"" />
			</xs:sequence>
            <xs:attribute name=""table"" type=""xs:string"" use=""optional"">
				<xs:annotation>
					<xs:documentation>default: unqualified classname</xs:documentation>
				</xs:annotation>
			</xs:attribute>
			<xs:attribute name=""polymorphism"" type=""polymorphismType"" use=""optional"" default=""implicit"" />
		</xs:complexType>
	</xs:element>
	<xs:element name=""jcs-cache"" type=""cacheType"" />
	<xs:element name=""cache"" type=""cacheType"" />
	<xs:complexType name=""cacheType"">
		<xs:attribute name=""region"" type=""xs:string"" use=""optional"" />
		<xs:attribute name=""usage"" use=""required"">
			<xs:simpleType>
				<xs:restriction base=""xs:string"">
					<xs:enumeration value=""read-only"" />
					<xs:enumeration value=""read-write"" />
					<xs:enumeration value=""nonstrict-read-write"" />
				</xs:restriction>
			</xs:simpleType>
		</xs:attribute>
	</xs:complexType>
	<xs:element name=""discriminator"">
		<xs:annotation>
			<xs:documentation>Polymorphic data requires a column holding a class discriminator value. This value is not directly exposed to the application</xs:documentation>
		</xs:annotation>
		<xs:complexType>
			<xs:attribute name=""column"" type=""xs:string"" />
			<xs:attribute name=""type"" type=""xs:string"" default=""String"" />
			<xs:attribute name=""not-null"" type=""xs:boolean"" default=""true"" />
			<xs:attribute name=""length"" type=""xs:positiveInteger"" />
			<xs:attribute name=""force"" type=""xs:boolean"" default=""false"" />
			<xs:attribute name=""insert"" type=""xs:boolean"" default=""true"" />
			<xs:attribute name=""formula"" type=""xs:string"" />
		</xs:complexType>
	</xs:element>
	<xs:element name=""property"">
		<xs:complexType>
			<xs:attribute name=""name"" type=""xs:string"" use=""required"" />
			<xs:attribute name=""access"" type=""xs:string"" />
			<xs:attribute name=""type"" type=""xs:string"" />
		</xs:complexType>
	</xs:element>
	<xs:element name=""many-to-one"">
		<xs:complexType>
			<xs:attribute name=""name"" type=""xs:string"" use=""required"" />
			<xs:attribute name=""access"" type=""xs:string"" />
			<xs:attribute name=""class"" type=""xs:string"" />
		</xs:complexType>
	</xs:element>
	<xs:element name=""filter"">
		<xs:complexType mixed=""true"">
			<xs:attribute name=""name"" type=""xs:string"" use=""required"" />
			<xs:attribute name=""condition"" type=""xs:string"" use=""optional"" />
		</xs:complexType>
	</xs:element>
</xs:schema>";

            #endregion

            XmlSchema schema = base.CreateXmlSchema(xsd);
            PreprocesingVisitor visitor = CreateGeneratorVisitor();
            visitor.VisitSchema(schema);
            ElementGenerator generator = (ElementGenerator)visitor.ElementsAcceptor;
            Assert.AreEqual(7, generator.ElementGenerationInfos.Count);

            IElementInfo objectsElement = generator.ElementGenerationInfos.Find(delegate(IElementInfo obj)
                                                                                             {
                                                                                                 return
                                                                                                     obj.Name ==  "class";
                                                                                             });
                
            Assert.IsNotNull(objectsElement);
            
            TreeElements treeElements = new TreeElements();
            treeElements.FileWriter = new FilesWriter(TargetDirectory);
            treeElements.NameConverter = new CamelNameConverter();
            treeElements.PluralProvider = new DictionaryPluralProvider(); 
            treeElements.GenerateElementInterface(objectsElement.TypeInfo);
                
            // TODO compare file with expected
        }

        [Test]
        public void GenerateInterfaceNH12Live()
        {
            string xsdPath = "..\\..\\Tests\\nhibernate-mapping.xsd";

            string targerDirectory = Path.Combine(Environment.CurrentDirectory, "nh12live");
            if (Directory.Exists(targerDirectory))
            {
                Directory.Delete(targerDirectory, true);
            }

            Runner runner = new Runner();
            runner.BaseNamespace = "Arp.NH.Psi.Tree";
            runner.OutputDirectory = targerDirectory;
            runner.XsdFile = xsdPath;

            runner.Run();

            // TODO compare file with expected
        }        


        protected string TargetDirectory
        {
            get 
            {
                return Environment.CurrentDirectory;
            }
        }
    }
}