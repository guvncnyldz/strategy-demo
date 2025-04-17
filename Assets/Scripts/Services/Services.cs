using System;
using System.Collections.Generic;

public static class Services
{
    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<T>(Type type, T service) where T : ServiceBase
    {
        if (!_services.ContainsKey(type))
        {
            _services.Add(type, service);
        }
    }

    public static T Get<T>() where T : ServiceBase
    {
        var type = typeof(T);

        if (_services.ContainsKey(type))
        {
            return (T)_services[type];
        }

        return null;
    }

    public static void Unregister(Type type)
    {
        if (_services.ContainsKey(type))
        {
            _services.Remove(type);
        }
    }
}