using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using JanusEngine.Content;
using JanusEngine.Graphics;

namespace JanusEngine
{
    public class ServiceAtribute : Attribute
    {
        public string Name { get; set; }
    }

    public class ServiceDependencyAttribute : Attribute
    {
        public Type DependsOn { get; set; }
    }

    public class ServiceProvider : IServiceProvider
    {
        Dictionary<Type, object> m_services = new Dictionary<Type, object>();

        public void Add<ServiceType>(ServiceType service)
        {
            m_services.Add(typeof(ServiceType), service);
        }

        public object GetService(Type serviceType)
        {
            return m_services[serviceType];
        }

    }
}
