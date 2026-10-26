using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.RegistrySystem;

namespace FactorySimulator.Core.Source.ContentSystem;

public class ContentModule
{
    public virtual void RegisterContent(RegistryManager registryManager) { }

    protected string BuildName(string name)
    {
        return $"{GetType().GetCustomAttribute<ContentModuleDataAttribute>()!.Name}:{name}";
    }
}
