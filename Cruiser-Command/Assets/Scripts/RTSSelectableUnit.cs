/*
 * Name: RTS Unit Selection
 * Author: James 'Sevion' Nhan
 * Date: 03/07/2013
 * Version: 1.0.1.0
 * Description:
 * 		This is a simple RTS movement script that handles
 * 		unit selection.
 */

using UnityEngine;
using System.Collections;

public class RTSSelectableUnit : MonoBehaviour {
    public static int HighlightedUnits = 0;
    public GameObject SelectionCircle = null;
    private GameObject SelectionCircleInstance = null;
    private RTSUnitSelectionManager UnitManager;

	void Start () {
        // Get our singleton Unit Manager
		UnitManager = GameObject.FindWithTag("UnitManager").GetComponent<RTSUnitSelectionManager>();
	}

    void OnMouseUpAsButton() {
        // If the Selection Circle doesn't exist, create it at the unit
        if (SelectionCircleInstance == null) {
            SelectionCircleInstance = GameObject.Instantiate(SelectionCircle, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - gameObject.transform.localScale.y / 2 + 0.01f, gameObject.transform.position.z), Quaternion.identity) as GameObject;
            // Parent it to the unit so it follows it
            SelectionCircleInstance.transform.parent = gameObject.transform;
        }
        // Select the unit
        UnitManager.SelectUnit(gameObject);
    }

    void OnTriggerEnter(Collider col) {
        // This keeps track of the number of units currently highlighted
        HighlightedUnits++;
        // If the Selection Circle doesn't exist, create it at the unit
        if (SelectionCircleInstance == null) {
            SelectionCircleInstance = GameObject.Instantiate(SelectionCircle, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - gameObject.transform.localScale.y / 2 + 0.01f, gameObject.transform.position.z), Quaternion.identity) as GameObject;
            // Parent it to the unit so it follows it
            SelectionCircleInstance.transform.parent = gameObject.transform;
        }
	}

    void OnTriggerExit(Collider col) {
        // This unit left the Selection Box, so decrement the number of highlighted units
        HighlightedUnits--;
        // Then destroy the Selection Circle
        GameObject.DestroyObject(SelectionCircleInstance);
	}

    void Update() {
        // Check if we have no units selected when we let go of the mouse
        if (RTSUnitSelection.Unselect) {
            // None, so deselect all units
            GameObject.DestroyObject(SelectionCircleInstance);
            // Reset Untselect so we don't have rubber ducking (Gogo Erik)
            RTSUnitSelection.Unselect = false;
        }
        // Select the unit if it's highlighted else deselect it
        if (SelectionCircleInstance != null) {
            UnitManager.SelectUnit(gameObject);
        } else {
            UnitManager.DeselectUnit(gameObject);
        }
    }
}
