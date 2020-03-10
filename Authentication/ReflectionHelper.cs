using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class ReflectionHelper
    {
        private TAttribute GetAttribute<TAttribute>(Type type) where TAttribute : Attribute
        {
            return (TAttribute)type.GetCustomAttribute(typeof(TAttribute));
        }

        public static Assembly GetAssembly()
        {
            return Assembly.GetCallingAssembly();
        }

        public static IEnumerable<MethodInfo> GetMethods<TClass>(Type type) where TClass : class
        {
            return ((IEnumerable<MethodInfo>)type.GetMethods(BindingFlags.Instance | BindingFlags.Public)).Where<MethodInfo>((Func<MethodInfo, bool>)(m => m.ReturnType == typeof(TClass))).Distinct<MethodInfo>();
        }

        public static IEnumerable<Type> GetTypes<TClass>(Assembly assembly) where TClass : class
        {
            return ((IEnumerable<Type>)assembly.GetExportedTypes()).Where<Type>((Func<Type, bool>)(type => type.IsSubclassOf(typeof(TClass))));
        }

        public static TClass CreateInstance<TClass>(Type type) where TClass : class
        {
            return Activator.CreateInstance(type) as TClass;
        }

        public static TClass CreateInstance<TClass>() where TClass : class
        {
            return Activator.CreateInstance(typeof(TClass)) as TClass;
        }

        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
