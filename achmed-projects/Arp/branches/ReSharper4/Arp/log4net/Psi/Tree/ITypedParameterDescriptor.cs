namespace Arp.log4net.Psi.Tree
{
    
    public enum DeclaredParameterType
    {
        /// <summary>
        /// xml attribute
        /// </summary>
        XmlAttribute, 
        /// <summary>
        /// static xml element like appender, logger, root etc
        /// </summary>
        StatisElement, 
        /// <summary>
        /// dynamic appender element
        /// Exmaple
        /// <appender>
        ///     <fileName value="..."/>
        ///     <layout type="..."> .... </layout>
        ///     ......
        /// </appender>
        /// </summary>
        DynamicElement,

        /// <summary>
        /// dynamic parameter declared in "param" element
        /// Example
        /// <param name="file" value="log.txt" />
        /// <param name="layout" type="SimpleLayout" >
        ///     ......
        /// </param>
        /// </summary>
        DynamicParam,
    }
    
//    TODO use this type in completion service, validation service
    public interface ITypedParameterDescriptor : IParameterDescriptor
    {
        DeclaredParameterType DeclaredParameterType
        { get;}

    }
}