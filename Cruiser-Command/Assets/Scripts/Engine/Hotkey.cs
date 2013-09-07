using UnityEngine;
using System.Collections.Generic;

// Represents a hotkey setting.
public class Hotkey
{
    // The key.
    private KeyCode key;

    // Must shift be held?
    private bool shift;

    // Must ctrl be held?
    private bool ctrl;

    public Hotkey(KeyCode key, bool shift, bool ctrl)
    {
        this.key = key;
        this.shift = shift;
        this.ctrl = ctrl;
    }

    public KeyCode GetKey() { return key; }
    public bool GetShift() { return shift; }
    public bool GetCtrl() { return ctrl; }
}