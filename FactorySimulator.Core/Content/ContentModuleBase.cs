using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.AssetSystem;
using FactorySimulator.Core.Source.ContentSystem;
using FactorySimulator.Core.Source.EntitySystem;
using FactorySimulator.Core.Source.InputSystem;
using FactorySimulator.Core.Source.Level;
using FactorySimulator.Core.Source.RegistrySystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FactorySimulator.Core.Content;

[ContentModuleData("base")]
public class ContentModuleBase : ContentModule
{
    public override void RegisterContent(RegistryManager registryManager)
    {
        registryManager.Register(new LevelScene(BuildName("level")));

        registryManager.Register(new InputAction(BuildName("up"), [new KeyBinding(Keys.W)]));
        registryManager.Register(new InputAction(BuildName("down"), [new KeyBinding(Keys.S)]));
        registryManager.Register(new InputAction(BuildName("left"), [new KeyBinding(Keys.A)]));
        registryManager.Register(new InputAction(BuildName("right"), [new KeyBinding(Keys.D)]));

        registryManager.Register(new InputAction(BuildName("shift"), [new KeyBinding(Keys.LeftShift), new KeyBinding(Keys.RightShift)]));
        registryManager.Register(new InputAction(BuildName("control"), [new KeyBinding(Keys.LeftControl), new KeyBinding(Keys.RightControl)]));
        registryManager.Register(new InputAction(BuildName("alt"), [new KeyBinding(Keys.LeftAlt), new KeyBinding(Keys.RightAlt)]));

        registryManager.Register(new InputAction(BuildName("left_click"), [new MouseBinding(MouseButton.Left)]));
        registryManager.Register(new InputAction(BuildName("middle_click"), [new MouseBinding(MouseButton.Middle)]));
        registryManager.Register(new InputAction(BuildName("right_click"), [new MouseBinding(MouseButton.Right)]));

        registryManager.Register(new EntityPrototype(BuildName("test_entity"), new Collider(Vector2.One), new Collider(Vector2.One), Vector2.Zero, "base:missing_texture"));
    }
}