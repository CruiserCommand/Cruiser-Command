using UnityEngine;
using System.Collections;
using CC.eLog;

public class Move : Ability{

    public override void DoEffect(Vector3 target) {
        Log.Info("general", "Ordered to move");
    }

    public override Hotkey GetHotkey() {
        return new Hotkey(KeyCode.Mouse1, false, false);
    }

}
