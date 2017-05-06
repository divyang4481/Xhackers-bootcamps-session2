using System;
using System.Collections.Generic;
using System.Reflection;

namespace GPSImageTag.Core.Helpers
{
    public class ServiceManager
    {

        public static void Register(Type ServiceType)
        {
            var Interfaces = ServiceType.GetTypeInfo().ImplementedInterfaces;
            foreach (var I in Interfaces)
                _ServiceTypes[I] = ServiceType;
        }

        public static void Register<T>(T Service)
        {
            _ServiceObjects[typeof(T)] = Service;
        }

        public static void RegisterFactory<T>(Func<T> Factory)
        {
            _ServiceFactories[typeof(T)] = Factory;
        }

        public static T GetObject<T>()
        {
            var a = typeof(ServiceManager).GetTypeInfo().Assembly;
            var ts = a.ExportedTypes;
            object Service = null;
            if (_ServiceObjects.TryGetValue(typeof(T), out Service))
                return (T)Service;
            object ServiceFactory = null;
            if (_ServiceFactories.TryGetValue(typeof(T), out ServiceFactory))
                return ((Func<T>)ServiceFactory)();
            Type Type = null;
            if (_ServiceTypes.TryGetValue(typeof(T), out Type))
            {
                var obj = (T)Activator.CreateInstance(Type);
                _ServiceObjects[typeof(T)] = obj;
                return obj;
            }
            return default(T);
        }

        private static IDictionary<Type, object> _ServiceObjects = new Dictionary<Type, object>();
        private static IDictionary<Type, Type> _ServiceTypes = new Dictionary<Type, Type>();
        private static IDictionary<Type, object> _ServiceFactories = new Dictionary<Type, object>();
    }
}
