using JetBrains.Util;

namespace Arp.log4net.Psi.Tree
{
    public enum ValidationError
    {
        Ok, Error
    }
    
    public class ValidationResult
    {
        private ValidationError error;
        private TextRange range;
        private string message;

        public static readonly ValidationResult Ok = new ValidationResult(ValidationError.Ok, TextRange.InvalidRange, "");


        protected ValidationResult(ValidationError error, TextRange range, string message)
        {
            this.error = error;
            this.range = range;
            this.message = message;
        }

        public ValidationResult(TextRange range, string message)
            : this(ValidationError.Error, range, message)
        {
        }

        public ValidationError Error
        {
            get { return error; }
        }

        public TextRange Range
        {
            get { return range; }
        }

        public string Message
        {
            get { return message; }
        }


        ///<summary>
        ///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("Error: {0} Range: {1} Message: {2}", Error, Range, Message);
        }
    }

    
    public interface IParameterStringValueValidator
    {
        ValidationResult Validate(string value);
    }
}