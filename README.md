# Instant Water

A Core Keeper mod that makes water spread instantly instead of creeping one tile every 1–2 seconds.

Water flow in the base game waits for a timer on every tile. This mod restarts those timers with zero lifespan right before the game's water spreading system runs, so a flood resolves within a few frames.

## Features

- Water and pit-filling spread on the next frame instead of 1–2 s per tile.
- Only the timing changes. The game's tile rules (water priority, pits, lava) are untouched.
- Single player only. Multiplayer is not tested.

## Limitations

- Spread is one tile per frame, not literally zero time. A 20-tile lake fills in roughly 0.3 s at 60 FPS.
- Tested on the current Steam build. The mod is compiled at load time from source, so it may break after a game update that changes the water system.

## Installation

1. Download or clone this repository.
2. Copy the `InstantWater` folder (containing `InstantWater.cs` and `ModManifest.json`) into:

   ```
   <Core Keeper install>\CoreKeeper_Data\StreamingAssets\Mods\
   ```

   On a default Steam install that is:

   ```
   C:\Program Files (x86)\Steam\steamapps\common\Core Keeper\CoreKeeper_Data\StreamingAssets\Mods\InstantWater\
   ```

   Create the `Mods` folder if it does not exist.
3. Start the game and load a single-player world.

To verify it loaded, check `%USERPROFILE%\AppData\LocalLow\Pugstorm\Core Keeper\Player.log` for:

```
loaded mod InstantWater
Successfully compiled InstantWater
```

## Uninstall

Delete the `InstantWater` folder from `StreamingAssets\Mods\`.

## How it works

- `InstantWaterSpreadSystem` is a `SystemBase` in the server simulation group, ordered `UpdateBefore(typeof(WaterSpreadingSystem))`.
- Each frame it loops over every `WaterSpreaderCD` and calls `timer.Start(now, 0f)`, which makes `IsTimerElapsed` true immediately.
- The manifest sets `accessesExtraAssemblies: true` so the script can reference the game's `WaterSpreadingSystem` and `WaterSpreaderCD` types.

## Credits

Written with Claude Code. Based on reading the game's decompiled water system (`Pug.Other.dll`) for reference only; no game code is included in this repository.
