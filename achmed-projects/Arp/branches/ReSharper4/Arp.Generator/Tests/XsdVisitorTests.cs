using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;
using Arp.Generator.Xsd;
using NUnit.Framework;

namespace Arp.Generator.Tests
{
    [TestFixture]
    public class XsdVisitorTests : BaseTestFixture
    {
        class TestXsdVisitor : XsdVisitor
        {
            private readonly List<string> simpleTypes = new List<string>();
            private readonly List<XmlSchemaSimpleTypeRestriction> restrictions = new List<XmlSchemaSimpleTypeRestriction>();

            public List<string> SimpleTypes
            {
                get { return simpleTypes; }
            }


            public List<XmlSchemaSimpleTypeRestriction> Restrictions
            {
                get { return restrictions; }
            }

            public override void VisitSimpleType(XmlSchemaSimpleType xmlSchemaSimpleType)
            {
                base.VisitSimpleType(xmlSchemaSimpleType);

                simpleTypes.Add(xmlSchemaSimpleType.Name);
            }


            public override void VisitXmlSchemaSimpleTypeRestrictionNotEmpty(XmlSchemaSimpleTypeRestriction restriction)
            {
                base.VisitXmlSchemaSimpleTypeRestrictionNotEmpty(restriction);

                restrictions.Add(restriction);

            }
        }


        [Test]
        public void VisitSimpleTypes()
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
	<xs:simpleType name=""outerJoinStrategy"">
		<xs:restriction base=""xs:string"">
			<xs:enumeration value=""auto"" />
			<xs:enumeration value=""true"" />
			<xs:enumeration value=""false"" />
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
</xs:schema>";

            #endregion

            TestXsdVisitor visitor = VisitSchema(xsd);
            Assert.AreEqual(4, visitor.SimpleTypes.Count);
            Assert.IsTrue(visitor.SimpleTypes.Contains("polymorphismType"));
            Assert.IsTrue(visitor.SimpleTypes.Contains("outerJoinStrategy")); 
        }

        [Test]
        public void VisitXmlSchemaSimpleTypeRestriction()
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
	<xs:simpleType name=""outerJoinStrategy"">
		<xs:restriction base=""xs:string"">
			<xs:enumeration value=""auto"" />
			<xs:enumeration value=""true"" />
			<xs:enumeration value=""false"" />
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
</xs:schema>";

            #endregion

            TestXsdVisitor visitor = VisitSchema(xsd);
            Assert.AreEqual(3, visitor.Restrictions.Count);
        }

        private TestXsdVisitor VisitSchema(string xsd)
        {
            XmlSchema xmlSchema = CreateXmlSchema(xsd);

            TestXsdVisitor visitor = new TestXsdVisitor();
            visitor.VisitSchema(xmlSchema);
            return visitor;
        }
    }
}