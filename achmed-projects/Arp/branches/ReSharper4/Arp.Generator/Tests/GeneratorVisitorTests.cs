using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml.Schema;
using Arp.Generator.Acceptors;
using Arp.Generator.Names;
using Arp.Generator.Preprocessing;
using Arp.Generator.Preprocessing.Impl;
using NUnit.Framework;

namespace Arp.Generator.Tests
{


    [TestFixture]
    public class GeneratorVisitorTests : BaseTestFixture
    {
        [Test]
        public void GenerateEnums()
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
	<xs:simpleType name=""customSQLCheck"">
		<xs:restriction base=""xs:string"">
			<xs:enumeration value=""none"" />
			<xs:enumeration value=""rowcount"" />
			<!-- <xs:enumeration value=""param"" /> -->
		</xs:restriction>
	</xs:simpleType>
    <xs:element name=""objects"">
        <xs:complexType>
            <xs:attribute name=""default-dependency-check"" use=""optional"" default=""none"">
                <xs:simpleType>
                    <xs:restriction base=""xs:string"">
                        <xs:enumeration value=""none""/>
                        <xs:enumeration value=""objects""/>
                        <xs:enumeration value=""simple""/>
                        <xs:enumeration value=""all""/>
                    </xs:restriction>
                </xs:simpleType>
            </xs:attribute>
        </xs:complexType>
    </xs:element>
</xs:schema>";

            #endregion

            XmlSchema schema = base.CreateXmlSchema(xsd);
            PreprocesingVisitor visitor = CreateGeneratorVisitor();
            visitor.VisitSchema(schema);
            EnumGenerator generator = (EnumGenerator)visitor.EnumAcceptor;
            Assert.AreEqual(4, generator.EnumGenerationInfos.Count);

            // polymorphismType
            
            EnumGenerationInfo polymorphismTypeEnum = generator.EnumGenerationInfos.Find(delegate(EnumGenerationInfo obj)
                                                                                             {
                                                                                                 return
                                                                                                     obj.TypeName.FullName ==
                                                                                                     (BASE_NAMESPACE + "." + "PolymorphismType");
                                                                                             });

            Assert.IsNotNull(polymorphismTypeEnum);
            Assert.AreEqual(2, polymorphismTypeEnum.NamesValues.Count);
            Assert.AreEqual("0", polymorphismTypeEnum.NamesValues["Implicit"]);
            Assert.AreEqual("1", polymorphismTypeEnum.NamesValues["Explicit"]);
            Assert.AreEqual("implicit", polymorphismTypeEnum.NamesXmlValues["Implicit"]);
            Assert.AreEqual("explicit", polymorphismTypeEnum.NamesXmlValues["Explicit"]);

            // cacheType usage
            EnumGenerationInfo cacheTypeUsageTypeEnum = generator.EnumGenerationInfos.Find(delegate(EnumGenerationInfo obj)
                                                                                             {
                                                                                                    return
                                                                                                     obj.TypeName.FullName ==
                                                                                                     (BASE_NAMESPACE + "." + "CacheTypeUsageType");
                                                                                             });

            Assert.IsNotNull(cacheTypeUsageTypeEnum);


            // customSQLCheck

            EnumGenerationInfo customSQLCheckEnum = generator.EnumGenerationInfos.Find(delegate(EnumGenerationInfo obj)
                                                                                             {
                                                                                                 return
                                                                                                     obj.TypeName.FullName ==
                                                                                                     (BASE_NAMESPACE + "." + "CustomSQLCheck");
                                                                                             });

            Assert.IsNotNull(customSQLCheckEnum);



            // objects default-dependency-check
            EnumGenerationInfo objectsDefaultDependencyCheckTypeEnum = generator.EnumGenerationInfos.Find(delegate(EnumGenerationInfo obj)
                                                                                             {
                                                                                                 return
                                                                                                     obj.TypeName.FullName ==
                                                                                                     (BASE_NAMESPACE + "." + "ObjectsDefaultDependencyCheckType");
                                                                                             });

            Assert.IsNotNull(objectsDefaultDependencyCheckTypeEnum);



        }

        [Test]
        public void GenerateElements()
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
	<xs:attributeGroup name=""baseClassAttributes"">
		<xs:attribute name=""name"" type=""xs:string"" use=""required"" />
		<xs:attribute name=""proxy"" type=""xs:string"" use=""optional"">
			<xs:annotation>
				<xs:documentation>default: no proxy interface</xs:documentation>
			</xs:annotation>
		</xs:attribute>
		<xs:attribute name=""lazy"" type=""xs:boolean"" use=""optional"">
			<xs:annotation>
				<xs:documentation>Enable the lazy loading of this class in associations</xs:documentation>
			</xs:annotation>
		</xs:attribute>
		<xs:attribute name=""dynamic-update"" type=""xs:boolean"" use=""optional"" default=""false"" />
		<xs:attribute name=""dynamic-insert"" type=""xs:boolean"" use=""optional"" default=""false"" />
		<xs:attribute name=""select-before-update"" type=""xs:boolean"" use=""optional"" default=""false"" />
	</xs:attributeGroup>
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
            <xs:attributeGroup ref=""baseClassAttributes"" />            
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

            // polymorphismType

            ElementGenerationInfo objectsElement = (ElementGenerationInfo)generator.ElementGenerationInfos.Find(delegate(IElementInfo obj)
                                                                                             {
                                                                                                 return
                                                                                                     ((ElementGenerationInfo)obj).TypeGenerationInfo.TypeName.FullName ==
                                                                                                     (BASE_NAMESPACE + "." + "Class");
                                                                                             });

            Assert.IsNotNull(objectsElement);
            Assert.AreEqual(2, objectsElement.TypeGenerationInfo.Attributes.Count);
            Assert.AreEqual(8, objectsElement.TypeGenerationInfo.AttributesInfo.Count);
            Assert.AreEqual(5, objectsElement.TypeGenerationInfo.FlatNestedElements.Count);
        }


        [Test]
        public void GenerateNH12Live()
        {
            string xsd = File.ReadAllText("..\\..\\Tests\\nhibernate-mapping.xsd");
            
            XmlSchema schema = base.CreateXmlSchema(xsd);
            PreprocesingVisitor visitor = CreateGeneratorVisitor();
            visitor.VisitSchema(schema);
            ElementGenerator generator = (ElementGenerator)visitor.ElementsAcceptor;
            
            // TODO count
            //Assert.AreEqual(7, generator.ElementGenerationInfos.Count);

            // polymorphismType

            ElementGenerationInfo objectsElement = (ElementGenerationInfo)generator.ElementGenerationInfos.Find(delegate(IElementInfo obj)
                                                                                             {
                                                                                                 return
                                                                                                     ((ElementGenerationInfo)obj).TypeGenerationInfo.TypeName.FullName ==
                                                                                                     (BASE_NAMESPACE + "." + "Class");
                                                                                             });

            Assert.IsNotNull(objectsElement);
            Assert.AreEqual(10, objectsElement.TypeGenerationInfo.Attributes.Count);
            Assert.AreEqual(16, objectsElement.TypeGenerationInfo.AttributesInfo.Count);
            Assert.AreEqual(1, objectsElement.TypeGenerationInfo.AttributeGroups.Count);
            Assert.AreEqual(12, objectsElement.TypeGenerationInfo.FlatNestedElements.Count);
            Assert.AreEqual(1, objectsElement.TypeGenerationInfo.Groups.Count);
        }
    }
}