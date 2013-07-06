/*
 * Name: RTS Unit Selection
 * Author: James 'Sevion' Nhan and Erik 'Siretu' Ihren
 * Date: 06/07/2013
 * Version: 1.0.2.0
 * Description:
 * 		This is a simple RTS movement script that handles
 * 		unit selection.
 */

using UnityEngine;
using System.Collections;

public class RTSSelectableUnit : MonoBehaviour {
    public GameObject SelectionCircle = null;
    private GameObject SelectionCircleInstance = null;
    private RTSUnitSelectionManager UnitManager;

	void Start () {
        // Get our singleton Unit Manager
		UnitManager = GameObject.FindWithTag("UnitManager").GetComponent<RTSUnitSelectionManager>();
	}

    void OnMouseUpAsButton() {
        // If the Selection Circle doesn't exist, create it at the unit
		Debug.Log ("Foo");
        if (SelectionCircleInstance == null) {
            SelectionCircleInstance = GameObject.Instantiate(SelectionCircle, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - gameObject.transform.localScale.y / 2 + 0.01f, gameObject.transform.position.z), Quaternion.identity) as GameObject;
            // Parent it to the unit so it follows it
            SelectionCircleInstance.transform.parent = gameObject.transform;
			Debug.Log(SelectionCircleInstance.transform.position);
			Debug.Log(gameObject.transform.position);
        }
        // Select the unit
		if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)){
			UnitManager.ClearSelection();
			UnitManager.SelectUnit(gameObject);
		} else {
			if(UnitManager.IsSelected(gameObject)){
				UnitManager.DeselectUnit(gameObject);
			} else {
        		UnitManager.SelectUnit(gameObject);
			}
		}
    }

    void OnTriggerEnter(Collider col) {
		Debug.Log("In OnTriggerEnter");
        // This keeps track of the number of units currently highlighted
		UnitManager.HighlightUnit(gameObject);
        // If the Selection Circle doesn't exist, create it at the unit
        if (SelectionCircleInstance == null) {
			Debug.Log("Created circle");
            SelectionCircleInstance = GameObject.Instantiate(SelectionCircle, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - gameObject.transform.localScale.y / 2 + 0.01f, gameObject.transform.position.z), Quaternion.identity) as GameObject;
            // Parent it to the unit so it follows it
            SelectionCircleInstance.transform.parent = gameObject.transform;
			Debug.Log(SelectionCircleInstance.transform.position);
			Debug.Log(gameObject.transform.position);
        }
	}

    void OnTriggerExit(Collider col) {
        // This unit left the Selection Box, so decrement the number of highlighted units
        UnitManager.UnhighlightUnit(gameObject);
        // Then destroy the Selection Circle
		Debug.Log("Destroyed circle");
		Debug.Log(SelectionCircleInstance);
        GameObject.DestroyObject(SelectionCircleInstance);
	}
	
	void Update(){
		if(!UnitManager.IsSelected(gameObject) && !UnitManager.IsHighlighted(gameObject)){
			Destroy(SelectionCircleInstance);
			SelectionCircleInstance = null;
		}
	}
}
