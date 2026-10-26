using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.RegistrySystem;

namespace FactorySimulator.Core.Source.SceneSystem;

public class Scene : ContentItem
{
    public Scene(string name) : base(name)
    {
        
    }

    public virtual void Start() { }

    public virtual void Update() { }

    public virtual void Draw() { }
    
    public virtual void End() { }
}