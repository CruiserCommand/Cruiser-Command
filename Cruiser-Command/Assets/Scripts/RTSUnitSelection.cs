/*
 * Name: RTS Unit Selection
 * Author: James 'Sevion' Nhan
 * Date: 03/07/2013
 * Version: 1.0.0.0
 * Description:
 * 		This is a simple RTS movement script that handles
 * 		unit selection and drag selection.
 */

using UnityEngine;
using System.Collections;

public class RTSUnitSelection : MonoBehaviour {
    public static bool Unselect = false;
	public GameObject SelectionBox = null;
	private GameObject SelectionBoxInstance = null;
	private Vector3 Corner;
	
	void OnMouseDown() {
		// Raycast to the plane
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit info;
		Physics.Raycast(ray, out info, Mathf.Infinity, 1);
		Corner = info.point;
		
		// Instantiate the selection box
		SelectionBoxInstance = Instantiate(SelectionBox, Corner, SelectionBox.transform.rotation) as GameObject;
	}
	
	void OnMouseDrag() {
		// Raycast to the plane
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit info;
		Physics.Raycast(ray, out info, Mathf.Infinity, 1);
		
		// Get resize the box to the area between the initial click and the mouse position
		Vector3 ResizeVector = info.point - Corner;
		Vector3 NewScale = SelectionBoxInstance.transform.localScale;
		NewScale.x = ResizeVector.x;
		NewScale.z = -ResizeVector.z;
		SelectionBoxInstance.transform.localScale = NewScale;
	}
	
	void OnMouseUp() {
        Unselect = (RTSSelectableUnit.HighlightedUnits == 0);
        RTSSelectableUnit.HighlightedUnits = 0;

		// Destroy the selection box so it doesn't linger on screen
		GameObject.DestroyObject(SelectionBoxInstance);
	}
}