using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Psi.Tree.Impl.StatisParameters
{
    public class StatisParameterDescriptor : IParameterDescriptor
    {
        private readonly string name;
        private readonly IType type;
        private readonly IDeclaredElement decraedElement;
        private readonly bool isRequired;
        private readonly string requredBefore;
        private readonly string requredAfter;
        private readonly bool isAttribute;
        private readonly string[] conflicts;
        private readonly bool isEnumerable;
        private readonly string[] possibleValues;

        public static readonly string  [] BOOLEAN_VALUES = new string[] {"true", "false"};
        // FIXME it's not full list of possible THRESHOLD. use special config?
        public static readonly string[] THRESHOLD = new string[] { "ALL", "INFO", "WARN", "ERROR" }; 

        public StatisParameterDescriptor(string name, bool isRequired) : this(name, isRequired, false)
        {
        
        }

        public StatisParameterDescriptor(string name, bool isRequired, bool isAttribute)
            : this(name, null, null, isRequired, null, null, isAttribute, null, null)
        {
        }

        public StatisParameterDescriptor(string name, bool isRequired, string[] possibleValues)
            : this(name, null, null, isRequired, null, null, true, null, possibleValues)
        {
        }


        public StatisParameterDescriptor(string name, IType type, IDeclaredElement decraedElement, bool isRequired, string requredBefore, string requredAfter, bool isAttribute, string[] conflicts, string[] possibleValues)
        {
            this.name = name;
            this.type = type;
            this.decraedElement = decraedElement;
            this.isRequired = isRequired;
            this.requredBefore = requredBefore;
            this.requredAfter = requredAfter;
            this.isAttribute = isAttribute;
            this.conflicts = conflicts;
            this.possibleValues = possibleValues;
            if(possibleValues == null || possibleValues.Length == 0)
                isEnumerable = false;
            else
                isEnumerable = true;
        }


        public string Name
        {
            get { return name; }
        }

        public IType Type
        {
            get { return type; }
        }

        public IDeclaredElement DecraedElement
        {
            get { return decraedElement; }
        }

        public bool IsRequired
        {
            get { return isRequired; }
        }

        public string RequredBefore
        {
            get { return requredBefore; }
        }

        public string RequredAfter
        {
            get { return requredAfter; }
        }

        public bool IsAttribute
        {
            get { return isAttribute; }
        }

        public string[] Conflicts
        {
            get { return conflicts; }
        }

        public bool IsEnumerable
        {
            get { return isEnumerable; }
        }

        public string[] PossibleValues
        {
            get { return possibleValues; }
        }

        #region IParameterDescriptor Members

        public bool RequredType
        {
            get { return false; }
        }

        #endregion
    }
}