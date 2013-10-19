using UnityEngine;
using System.Collections;
[System.Serializable]
public class TestAbility : Ability {

    public override void DoEffect(Vector3 target) {

    }
    
    
    public override Hotkey GetHotkey() {
        return new Hotkey(KeyCode.A,false,false);
    }
}
    