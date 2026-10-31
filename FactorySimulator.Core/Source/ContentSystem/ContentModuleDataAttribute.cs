using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorySimulator.Core.Source.ContentSystem;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ContentModuleDataAttribute : Attribute
{
    public string Name { get; }

    public ContentModuleDataAttribute(string name)
    {
        Name = name;
    }
}
