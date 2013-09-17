using UnityEngine;
using System.Collections;
using CC.eLog;

public class GroundMove : Ability{
    private Hotkey hotkey = new Hotkey(KeyCode.Mouse1, false, false);

    private GameObject owner;

    public GroundMove(GameObject unit){
        base.name = "Move";
        owner = unit;
    }

    public override void DoEffect(Vector3 target) {
        Log.Info("general", "Ordered to move");

        UnitOrder.OrderStruct order = new UnitOrder.OrderStruct(UnitOrder.Order.Move, target);
        UnitOrder unitorder = owner.GetComponent<UnitOrder>();
        unitorder.IssueOrder(order);
    }

    public override Hotkey GetHotkey() {
        return new Hotkey(hotkey);
    }

}
