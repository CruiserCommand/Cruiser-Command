/*
 * Name: RTS Unit Selection Manager
 * Author: James 'Sevion' Nhan
 * Date: 03/07/2013
 * Version: 1.0.0.0
 * Description:
 * 		This is a simple RTS movement script that handles
 * 		unit selection and drag selection.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RTSUnitSelectionManager : MonoBehaviour {
	private List<GameObject> SelectedObjects;

    void Start() {
        SelectedObjects = new List<GameObject>();
    }
	
	public GameObject[] GetSelectedObjects() {
		return SelectedObjects.ToArray();
	}
	
	public void SelectUnit(GameObject unit) {
		if (!SelectedObjects.Contains(unit)) {
			SelectedObjects.Add(unit);
		}
	}
	
	public void DeselectUnit(GameObject unit) {
		if (SelectedObjects.Contains(unit)) {
			SelectedObjects.Remove(unit);
		}
	}
	
	public void ClearSelection() {
		SelectedObjects.Clear();
	}
	
	public bool IsSelected(GameObject unit) {
		return SelectedObjects.Contains(unit);
	}
}
