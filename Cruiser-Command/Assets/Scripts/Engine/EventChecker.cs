using UnityEngine;
using System.Collections.Generic;

// Checks user events and executes assosiated abilities.
public class EventChecker : MonoBehaviour {
    /*
     * TO-DO Should probably send the current mouse position for all abilites, this wont work for e.g. Wraith EMP
     */

    // List of all the Mouse KeyCodes...
    private static KeyCode[] mouseButtons = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Mouse3, KeyCode.Mouse4, KeyCode.Mouse5, KeyCode.Mouse6 };

    public void Update() {
        if (Input.anyKeyDown) {
            List<Unit> selectedUnits = Player.GetCurrentPlayer().GetSelection(); // TO-DO Player.GetCurrent().GetSelection();
            
            if (selectedUnits != null) {
                foreach (Unit unit in selectedUnits) {
                    CheckAbilitiesHotkeys(unit.GetAbilities());
                }
            }
        }
    }

    private void CheckAbilitiesHotkeys(List<Ability> abilities) {
        foreach (Ability ability in abilities) {
            Hotkey hotkey = ability.GetHotkey();

            // If the ability is triggered by mouse, don't check if it's also triggered by keys; could cause ability to be triggered twice.
            if (MousekeyMatchesHotkey(hotkey)) {
                // Find the coordinates of the click
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit info;
                Physics.Raycast(ray, out info, Mathf.Infinity);
                Vector3 target = info.point;

                ability.DoEffect(target);
            } else if (CurrentKeysMatchesHotkey(hotkey)) {
                Vector3 target = new Vector3(float.MinValue, float.MinValue, float.MinValue);
                ability.DoEffect(target);
            }
        }
    }

    // Special care needs to be taken if triggered by mouse because we need a target.
    private bool MousekeyMatchesHotkey(Hotkey hotkey) {
        if (HotkeyIsMouseButton(hotkey)) {
            if (CurrentKeysMatchesHotkey(hotkey)) {
                return true;
            }
        }

        return false;
    }

    private bool HotkeyIsMouseButton(Hotkey hotkey) {
        foreach (KeyCode mouseButton in mouseButtons) {
            if (hotkey.GetKey() == mouseButton) {
                return true;
            }
        }

        return false;
    }

    private bool CurrentKeysMatchesHotkey(Hotkey hotkey) {
        // Only trigger if correct key was recently pressed.
        bool rightKey = Input.GetKeyDown(hotkey.GetKey());

        // Also check that shift is down (if shift is needed; holding down shift wont stop hotkeys that don't require it).
        bool rightShift = hotkey.GetShift() ?
            (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) :
            true;

        // Also check that ctrl is down (if ctrl is needed; holding down ctrl wont stop hotkeys that don't require it).
        bool rightCtrl = hotkey.GetCtrl() ?
            (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) :
            true;

        return rightKey && rightShift && rightCtrl;
    }
}