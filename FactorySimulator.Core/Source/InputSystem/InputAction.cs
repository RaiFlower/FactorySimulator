using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactorySimulator.Core.Source.RegistrySystem;

namespace FactorySimulator.Core.Source.InputSystem;

public class InputAction : ContentItem
{
    public readonly List<InputBinding> Bindings;

    public InputAction(string name, List<InputBinding> bindings) : base(name)
    {
        Bindings = bindings;
    }

    public bool IsPressed => Bindings.Any(b => b.IsPressed);

    public event Action OnPress = null!;
    public event Action OnRelease = null!;

    public void Update(InputSnapshot inputSnapshot)
    {
        Bindings.ForEach(b => b.Update(inputSnapshot));

        if (Bindings.Any(b => !b.WasPressed && b.IsPressed))
        {
            OnPress?.Invoke();
        }

        if (Bindings.Any(b => b.WasPressed && !b.IsPressed))
        {
            OnRelease?.Invoke();
        }
    }
}