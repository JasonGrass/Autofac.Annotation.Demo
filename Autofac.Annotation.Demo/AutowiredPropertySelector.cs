using System.Linq;
using System.Reflection;
using Autofac.Core;

namespace Autofac.Annotation.Demo
{
    public class AutowiredPropertySelector : IPropertySelector
    {
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            return propertyInfo.CustomAttributes.Any(it => it.AttributeType == typeof(Autowired));
        }
    }
}