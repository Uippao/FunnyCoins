# FunnyCoins

FunnyCoins is a plugin for LabAPI that adds effects to flipping coins. Its architecture also allows other plugins to easily register their own effects.

## Features

Coinflip effects are first decided through the config option `good_effect_chance`, after which a good or a bad one is selected based on weights from their respective pool.
The configs should be mostly self-explanatory. You can customize messages the coins show or even translate them too from the configs.


## Setup

1. Download the latest version of `FunnyCoins.dll`, `dependencies.zip` and `plugin-dependencies.zip` from the releases page.
2. Place `FunnyCoins.dll` and the contents of `plugin-dependencies.zip` into `YOUR_SERVER_DIR/LabAPI/plugins/global/`, or alternatively into a specific port's directory instead of global.
3. Extract `dependencies.zip` into `YOUR_SERVER_DIR/LabAPI/dependencies/global/`, or alternatively into a specific port's directory instead of global.
4. Restart the server to generate the configs into `YOUR_SERVER_DIR/LabAPI/configs/PORT/FunnyCoins/config.yml`
5. You're done! It is recommended to check out the configs at this point.


## API Usage

You can add your own coin effects to FunnyCoins by creating a new effect class and registering it with the API:

```csharp
using FunnyCoins.API;
using FunnyCoins.Effects;
using LabApi.Features.Wrappers;

// A simple effect
public class SomethingNiceEffect : SimpleCoinEffect
{
    public override string Id => "SomethingNice";               // Unique effect ID
    public override bool IsGood => true;                      // Marks as good or bad
    public override int DefaultWeight => 5;                   // Default chance weight
    public override string DefaultMessage => "You feel nice"; // Message shown to players

    // Optionally can be used to change the default hint show time from 4 seconds
    public override float DefaultMessageDuration => 5f;

    public override void Execute(Player player)
    {
        // Apply the effect logic
        player.Health = player.MaxHealth;
    }
}

// Register the effect somewhere in your code, so FunnyCoins can load it
FunnyCoinsAPI.RegisterEffect(new SomethingNiceEffect());
```

### SimpleCoinEffect vs ICoinEffect

* **`SimpleCoinEffect`**
  Use this for straightforward effects with a single default message. It automatically provides a default configurable message and duration. Your only requirement is to override the `Execute(Player player)` method with the effect logic.

* **`ICoinEffect`**
  Implement this interface for complex effects that may need multiple messages, dynamic durations, or custom behavior. You are responsible for defining:

  * `DefaultMessages`: a collection of messages with keys and durations.
  * `DefaultMessage`: set to null.
  * `HandlesOwnMessage`: whether your effect manages its own messages instead of using the default system, usually you'd set this to true when using the interface.
  * `Execute(Player player)`: the full logic for the effect. You can use your own messages here

### Notes for External Plugins

By default, your effects plug into FunnyCoins' own configuration system, giving you some nicities.
You can also not define any messages at all, not use `FunnyCoins.API.ShowEffectMessage()`, and use anything you want to show hints or not show them at all, allowing for custom configs and whatnot.


## License

This plugin is free software licensed under the GNU Lesser General Public License version 3.0, or any later version released by the Free Software Foundation, if you so wish.
The LGPL is an extension of the GNU GPLv3, and contains its terms. Check that license out [here](https://www.gnu.org/licenses/gpl-3.0.html).
