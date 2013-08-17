using UnityEngine;
using System.Collections;

public class ConsoleControls : MonoBehaviour {

    private GameObject circle;
    private GameObject screen;

    private GameObject occupier = null;

	// Use this for initialization
	void Start () {
        circle = gameObject.transform.Find("Circle").gameObject;
        screen = gameObject.transform.Find("Console Screen").gameObject;
        Debug.Log(circle);
	}

	// Update is called once per frame
	void Update () {
        if (occupier == null) {
            foreach (GameObject obj in Units.instance.playerUnits) {
                if (occupier == null && Vector3.Distance(obj.transform.position, circle.transform.position) <= 2) {
                    EnterConsole(obj);
                    break;
                }
            }
        }
	}

    void EnterConsole(GameObject obj) {
        if (obj != null) {
            Debug.Log("Entered circle");
            occupier = obj;

            // Snap to circle
            Vector3 newPos = circle.transform.position;
            newPos.y = obj.transform.position.y;
            obj.transform.position = newPos;

            // Halt movement
            RTSUnitMovement movement = obj.GetComponent<RTSUnitMovement>();
            movement.path = null;

            // Set unit's console
            UnitStats stats = obj.GetComponent<UnitStats>();
            stats.console = gameObject;

            obj.transform.LookAt(screen.transform.position);
        }
    }

    void DisconnectConsole() {
        GameObject obj = occupier;
        if (obj != null) {
            occupier = null;

            // Clear unit's console
            UnitStats stats = obj.GetComponent<UnitStats>();
            stats.console = null;
        }
    }
}
