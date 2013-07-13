/*
 * Name: RTS Unit Selection
 * Author: James 'Sevion' Nhan and Erik 'Siretu' Ihren
 * Date: 03/07/2013
 * Version: 1.0.0.1
 * Description:
 * 		This is a simple RTS movement script that handles
 * 		unit selection and drag selection.
 */

using UnityEngine;
using System.Collections;

public class RTSUnitSelection : MonoBehaviour {
    public LayerMask Ground;
    public LayerMask Sky;
	private Vector3 CornerScreen;
    private RTSUnitSelectionManager UnitManager;
    private Vector3 SelectionBoxRectGroundCorner;
    private Vector3 SelectionBoxRectGroundCorner2;
    private Vector3 SelectionBoxRectSkyCorner;
    private Vector3 SelectionBoxRectSkyCorner2;
    private Rect SelectionBoxRectDraw;
    private GUIStyle SelectionBoxStyle;
    private Texture2D SelectionBoxTexture;
    private GameObject SelectionBox;
	
	void Start(){
        UnitManager = GameObject.FindWithTag("UnitManager").GetComponent<RTSUnitSelectionManager>();
        SelectionBoxStyle = new GUIStyle();
        SelectionBoxTexture = new Texture2D(1, 1);
        Color transparentGreen = Color.green;
        transparentGreen.a = 0.2f;
        SelectionBoxTexture.SetPixel(1, 1, transparentGreen);
        SelectionBox = new GameObject();
        SelectionBox.AddComponent<MeshFilter>();
        SelectionBox.AddComponent<MeshRenderer>();
        SelectionBox.AddComponent<MeshCollider>();

        var shader = Shader.Find("Transparent/Diffuse");
        var mat = new Material(shader);
        //transparentGreen.a = 0.0f;
        mat.color = transparentGreen;
        transparentGreen.a = 0.2f;

        SelectionBox.renderer.material = mat;
	}

    // Must update so when scrolling the screen while dragging, the original point stays properly
    // Basically, save Input.mousePosition, and recast on scroll/Update tick (may be costly)
    void Update() {
        var meshFilter = SelectionBox.GetComponent<MeshFilter>();
        var mc = SelectionBox.GetComponent<MeshCollider>();
        var verticies = new Vector3[8] {
            SelectionBoxRectGroundCorner2,
            new Vector3(SelectionBoxRectGroundCorner.x, SelectionBoxRectGroundCorner.y, SelectionBoxRectGroundCorner2.z),
            new Vector3(SelectionBoxRectGroundCorner2.x, SelectionBoxRectGroundCorner.y, SelectionBoxRectGroundCorner.z),
            SelectionBoxRectGroundCorner,
            SelectionBoxRectSkyCorner2,
            new Vector3(SelectionBoxRectSkyCorner.x, SelectionBoxRectSkyCorner.y, SelectionBoxRectSkyCorner2.z),
            new Vector3(SelectionBoxRectSkyCorner2.x, SelectionBoxRectSkyCorner.y, SelectionBoxRectSkyCorner.z),
            SelectionBoxRectSkyCorner
            /*
            new Vector3(-5, -5, -5), // 0        7-----5
            new Vector3(5, -5, -5),  // 1        |3---1|
            new Vector3(-5, -5, 5),  // 2        ||   ||
            new Vector3(5, -5, 5),   // 3        |2---0|
            new Vector3(-5, 0, -5),  // 4        6-----4
            new Vector3(5, 0, -5),   // 5
            new Vector3(-5, 0, 5),   // 6
            new Vector3(5, 0, 5)     // 7
            */
        };

        var triangles = new int[36] {
            1, 2, 0,
            1, 3, 2,
            4, 6, 5,
            6, 7, 5,
            4, 5, 1,
            4, 1, 0,
            6, 2, 3,
            6, 3, 7,
            6, 0, 2,
            6, 4, 0,
            7, 3, 1,
            7, 1, 5
        };

        var uvs = new Vector2[8] {
            new Vector2(1, 1),
            new Vector2(1, -1),
            new Vector2(-1, 1),
            new Vector2(-1, -1),
            new Vector2(1, 1),
            new Vector2(1, -1),
            new Vector2(-1, 1),
            new Vector2(-1, -1)
        };

        Mesh mesh = new Mesh();
        mesh.vertices = verticies;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();

        mc.sharedMesh = null;
        mc.isTrigger = true;
        mc.convex = true;
        mc.enabled = true;
        meshFilter.sharedMesh = mesh;

        var shader = Shader.Find("Transparent/Diffuse");
        var mat = new Material(shader);
        Color transparentGreen = Color.green;
        transparentGreen.a = 0.2f;
        mat.color = transparentGreen;

        Graphics.DrawMesh(mc.sharedMesh, Vector3.zero, Quaternion.identity, mat, 0);
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
    }

    void OnMouseUp() {
        //SelectionBoxRectGroundCorner = new Vector3(0, -500, 0);
        //SelectionBoxRectGroundCorner2 = new Vector3(0, -500, 0);
        //SelectionBoxRectSkyCorner = new Vector3(0, -500, 0);
        //SelectionBoxRectSkyCorner2 = new Vector3(0, -500, 0);

        SelectionBoxRectDraw.width = 0;
        SelectionBoxRectDraw.height = 0;
        SelectionBoxRectDraw.x = -1;
        SelectionBoxRectDraw.y = -1;
		if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)){
			UnitManager.ClearSelection();
		}
		foreach(GameObject obj in UnitManager.GetHighlightedObjects()){
			UnitManager.SelectUnit(obj);
		}
		UnitManager.ClearHighlight();
	}
}
