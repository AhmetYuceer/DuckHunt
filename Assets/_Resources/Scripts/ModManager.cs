using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModManager : MonoSingleton<ModManager>, ISelectedMod
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SelectSixtySecondsMod()
    {
        GameMods.activeMod = GameMods.Mods.sixtySeconds;
    }
    public void SelectThreeBulletsMod()
    {
        GameMods.activeMod = GameMods.Mods.threeBullets;
    }
}