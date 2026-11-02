using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.EntitySystem;

namespace FactorySimulator.Core.Source.Level;

public class Chunk
{
    public readonly List<Entity> Entities = new();
}