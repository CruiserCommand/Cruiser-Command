/*
 * Name: RTS Unit Selection
 * Author: James 'Sevion' Nhan and Erik 'Siretu' Ihren
 * Date: 03/07/2013
 * Version: 1.1.0.0
 * Description:
 * 		This is a simple RTS movement script that handles
 * 		unit selection and drag selection.
 */

using UnityEngine;
using System.Collections;

public class RTSUnitSelection : MonoBehaviour {
    public string SelectableUnitTag;
    public LayerMask Ground;
    public LayerMask Sky;
    public LayerMask Units;
	private Vector3 CornerScreen;
    private RTSUnitSelectionManager UnitManager;
    private Vector3 SelectionBoxRectGroundCorner;
    private Vector3 SelectionBoxRectGroundCorner2;
    private Vector3 SelectionBoxRectSkyCorner;
    private Vector3 SelectionBoxRectSkyCorner2;
    private Rect SelectionBoxRectDraw;
    private GUIStyle SelectionBoxStyle;
    private Texture2D SelectionBoxTexture;
	
	void Start(){
        UnitManager = GameObject.FindWithTag("UnitManager").GetComponent<RTSUnitSelectionManager>();
        SelectionBoxStyle = new GUIStyle();
        SelectionBoxTexture = new Texture2D(1, 1);
        Color transparentGreen = Color.green;
        transparentGreen.a = 0.2f;
        SelectionBoxTexture.SetPixel(1, 1, transparentGreen);
	}

    void OnGUI() {
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

        SelectionBoxTexture.Apply();
        SelectionBoxStyle.normal.background = SelectionBoxTexture;
        GUI.Box(SelectionBoxRectDraw, "", SelectionBoxStyle);

        GUI.EndGroup();
    }
	
	void OnMouseDown() {

		// Raycast to the planes
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit info;
		Physics.Raycast(ray, out info, Mathf.Infinity, Ground);
        SelectionBoxRectGroundCorner = info.point;
        Physics.Raycast(ray, out info, Mathf.Infinity, Sky);
        SelectionBoxRectSkyCorner = info.point;

		CornerScreen = Input.mousePosition;
        SelectionBoxRectDraw = new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, -1, -1);
	}

    void OnMouseDrag() {
        // Raycast to the planes
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        Physics.Raycast(ray, out info, Mathf.Infinity, Ground);
        SelectionBoxRectGroundCorner2 = info.point;
        Physics.Raycast(ray, out info, Mathf.Infinity, Sky);
        SelectionBoxRectSkyCorner2 = info.point;

        // Get resize the box to the area between the initial click and the mouse position
        Vector3 ResizeVector = Input.mousePosition - CornerScreen;
        SelectionBoxRectDraw.width = ResizeVector.x;
        SelectionBoxRectDraw.height = -ResizeVector.y;

        // Highlight all units within the rect for now
        // May need to change to a Frustrum later
        float left, top, right, bottom;
        // Set the top and left to the maximums to get the corner. Funky rotation causes maximum rather than min x and max z.
        // If the rotation changes, this will have to be revisited
        left = Mathf.Max(SelectionBoxRectGroundCorner.x, SelectionBoxRectGroundCorner2.x);
        top = Mathf.Max(SelectionBoxRectGroundCorner.z, SelectionBoxRectGroundCorner2.z);
        // Set the bottom and right to the minimums to get the corner. Same as above.
        right = Mathf.Min(SelectionBoxRectGroundCorner.x, SelectionBoxRectGroundCorner2.x);
        bottom = Mathf.Min(SelectionBoxRectGroundCorner.z, SelectionBoxRectGroundCorner2.z);
        // These are the vectors of the corners on the ground
        a = new Vector3(left, -10, top);
        b = new Vector3(right, -10, bottom);
        // Loop through all of the selectable units with the tag SelectableUnit tag
        GameObject[] selectableUnits = GameObject.FindGameObjectsWithTag(SelectableUnitTag);
        foreach (GameObject unit in selectableUnits) {
            // If it's in the rect, highlight it. Else unhighlight it.
            if (RectContains(unit.transform.position, left, top, right, bottom)) {
                UnitManager.HighlightUnit(unit);
            } else {
                UnitManager.UnhighlightUnit(unit);
            }
        }
    }

    Vector3 a, b;

    void Update() {
        Debug.DrawRay(a, Vector3.up * 10, Color.red);
        Debug.DrawRay(b, Vector3.up * 10, Color.green);
    }

    bool RectContains(Vector3 position, float left, float top, float right, float bottom) {
        // Check if the position given is within the rect defined by the floats given
        return position.x <= left && position.x >= right && position.z >= bottom && position.z <= top;
    }

    void OnMouseUp() {
        // Check if we clicked on a unit directly
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (Physics.Raycast(ray, out info, Mathf.Infinity, Units)) {
            // If so, highlight it
            UnitManager.HighlightUnit(info.transform.gameObject);
        }

        // Reset the selection box
        SelectionBoxRectGroundCorner = new Vector3(0, -500, 0);
        SelectionBoxRectGroundCorner2 = new Vector3(0, -500, 0);
        SelectionBoxRectSkyCorner = new Vector3(0, -500, 0);
        SelectionBoxRectSkyCorner2 = new Vector3(0, -500, 0);

        // And the drawn box
        SelectionBoxRectDraw.width = 0;
        SelectionBoxRectDraw.height = 0;
        SelectionBoxRectDraw.x = -1;
        SelectionBoxRectDraw.y = -1;
        // Clear the selection if we're not holding shift to add
		if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)){
			UnitManager.ClearSelection();
		}
        // Select all objects highlighted
		foreach(GameObject obj in UnitManager.GetHighlightedObjects()){
			UnitManager.SelectUnit(obj);
		}
        // Unhighlight units because they're already selected
		UnitManager.ClearHighlight();
	}
}
