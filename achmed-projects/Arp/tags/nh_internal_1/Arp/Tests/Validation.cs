using Arp.log4net.Psi.Tree;
using Arp.log4net.Psi.Tree.Impl.Validators;
using NUnit.Framework;

namespace Arp.Tests
{
    [TestFixture]
    public class Validation
    {
        [Test]
        public void CLR()
        {
            CLRValueTypesValidator intValidator = new CLRValueTypesValidator("System.Int32");
            Assert.AreEqual(ValidationResult.Ok, intValidator.Validate("123"));
            Assert.AreEqual(ValidationError.Error, intValidator.Validate("qw123").Error);
            Assert.AreEqual(ValidationError.Error, intValidator.Validate("").Error);

            CLRValueTypesValidator dateTimeValidator = new CLRValueTypesValidator("System.DateTime");
            Assert.AreEqual(ValidationResult.Ok, dateTimeValidator.Validate("12/12/12"));
            Assert.AreEqual(ValidationError.Error, dateTimeValidator.Validate("qweqwe").Error); 
            Assert.AreEqual(ValidationError.Error, dateTimeValidator.Validate("").Error); 
        }

        [Test]
        public void IP()
        {
            IPAddressValidator addressValidator = new IPAddressValidator();
            Assert.AreEqual(ValidationResult.Ok, addressValidator.Validate("72.14.207.99"));
            Assert.AreEqual(ValidationResult.Ok, addressValidator.Validate("google.com"));
            Assert.AreEqual(ValidationError.Error, addressValidator.Validate("qw123;").Error);
            Assert.AreEqual(ValidationError.Error, addressValidator.Validate("").Error);
        }

    }
}