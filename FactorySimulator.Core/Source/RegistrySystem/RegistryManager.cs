using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.AssetSystem;
using FactorySimulator.Core.Source.ContentSystem;
using FactorySimulator.Core.Source.InputSystem;
using FactorySimulator.Core.Source.SceneSystem;
using Microsoft.Xna.Framework.Content;

namespace FactorySimulator.Core.Source.RegistrySystem;

public class RegistryManager : Manager
{
    public readonly Dictionary<string, Scene> Scenes = new();
    public readonly Dictionary<string, InputAction> InputActions = new();

    public RegistryManager(GameContextView gameContextView) : base(gameContextView)
    {

    }

    private record ContentModuleRecord(ContentModuleDataAttribute Attribute, ContentModule Module);

    private List<ContentModuleRecord> _modules = new();

    public void RegisterContent()
    {
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            var attr = type.GetCustomAttribute<ContentModuleDataAttribute>();
            if (attr == null) continue;

            _modules.Add(new ContentModuleRecord(attr, (ContentModule)Activator.CreateInstance(type)!));
        }

        _modules.ForEach(m => m.Module.RegisterContent(this));

        LoadContentReferences();
    }

    public void Register(ContentItem registryEntry)
    {
        registryEntry.ProvideContextView(_gameContextView);

        switch (registryEntry)
        {
            case Scene scene:
                Scenes.Add(scene.Name, scene);
                break;
            case InputAction inputAction:
                InputActions.Add(inputAction.Name, inputAction);
                break;
        }
    }

    public void LoadContentReferences()
    {
        Scenes.Values.ToList().ForEach(item => item.LoadContentReferences());
        InputActions.Values.ToList().ForEach(item => item.LoadContentReferences());
    }

    public void LoadAssetReferences()
    {
        Scenes.Values.ToList().ForEach(item => item.LoadAssetReferences());
        InputActions.Values.ToList().ForEach(item => item.LoadAssetReferences());
    }
}