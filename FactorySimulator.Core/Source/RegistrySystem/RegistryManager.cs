using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.AssetSystem;
using FactorySimulator.Core.Source.ContentSystem;
using FactorySimulator.Core.Source.SceneSystem;
using Microsoft.Xna.Framework.Content;

namespace FactorySimulator.Core.Source.RegistrySystem;

public class RegistryManager : Manager
{
    public readonly Dictionary<string, Scene> Scenes = new();

    public RegistryManager(GameContextView gameContextView) : base(gameContextView)
    {

    }

    private record ContentModuleRecord(ContentModuleDataAttribute Attribute, ContentModule Module);

    private List<ContentModuleRecord> _modules = new();

    public void LoadContentModules()
    {
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            var attr = type.GetCustomAttribute<ContentModuleDataAttribute>();
            if (attr == null) continue;

            _modules.Add(new ContentModuleRecord(attr, (ContentModule)Activator.CreateInstance(type)!));
        }
    }

    public void RegisterContent()
    {
        _modules.ForEach(m => m.Module.RegisterContent(this));
    }

    public void Register(ContentItem registryEntry)
    {
        registryEntry.ProvideContextView(_gameContextView);

        switch (registryEntry)
        {
            case Scene scene:
                Scenes.Add(scene.Name, scene);
                break;
        }
    }
}