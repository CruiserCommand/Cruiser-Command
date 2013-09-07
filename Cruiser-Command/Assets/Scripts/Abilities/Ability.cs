using UnityEngine;
using System.Collections.Generic;

// Represents an ability with an assosciated hotkey.
[System.Serializable]
public abstract class Ability
{
    private string name;

    // Vector3(float.MinValue, float.MinValue, float.MinValue) if no target; e.g. Engine Boost
    public abstract void DoEffect(Vector3 target);

    public abstract Hotkey GetHotkey();
}