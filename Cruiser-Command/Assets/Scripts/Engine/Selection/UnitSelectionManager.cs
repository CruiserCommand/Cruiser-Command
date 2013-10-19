/*
 * Name: RTS Unit Selection Manager
 * Author: James 'Sevion' Nhan and Erik 'Siretu' Ihren
 * Date: 06/07/2013
 * Version: 1.0.0.1
 * Description:
 * 		This is a simple RTS movement script that handles
 * 		unit selection and drag selection.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitSelectionManager : MonoBehaviour {
	public List<GameObject> SelectedObjects;
	private List<GameObject> HighlightedObjects;

    void Start() {
        SelectedObjects = new List<GameObject>();
		HighlightedObjects = new List<GameObject>();
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

	public GameObject[] GetHighlightedObjects() {
		return HighlightedObjects.ToArray();
	}
	
	public void HighlightUnit(GameObject unit) {
		if (!HighlightedObjects.Contains(unit)) {
			HighlightedObjects.Add(unit);
		}
	}
	
	public void UnhighlightUnit(GameObject unit) {
		if (HighlightedObjects.Contains(unit)) {
			HighlightedObjects.Remove(unit);
		}
	}
	
	public void ClearHighlight() {
		HighlightedObjects.Clear();
	}
	
	public bool IsHighlighted(GameObject unit) {
		return HighlightedObjects.Contains(unit);
	}

}
