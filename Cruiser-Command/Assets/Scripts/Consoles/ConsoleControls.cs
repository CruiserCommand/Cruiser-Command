using UnityEngine;
using System.Collections;

public class ConsoleControls : MonoBehaviour {

    private GameObject circle;
    private GameObject screen;

    private GameObject occupier = null;
    private GameObject lastOccupier = null;

    private const double CIRCLE_RADIUS = 2;

	// Use this for initialization
	void Start () {
        circle = gameObject.transform.Find("Circle").gameObject;
        screen = gameObject.transform.Find("Console Screen").gameObject;
        Debug.Log(circle);
	}

	// Update is called once per frame
	void Update () {
        if (occupier == null) {
            foreach (GameObject obj in Unit.GetAllUnitsObjects()) {
                if (obj != null && occupier == null && obj != lastOccupier && Vector3.Distance(obj.transform.position, circle.transform.position) <= CIRCLE_RADIUS) {
                    EnterConsole(obj);
                    break;
                }
            }
            if (lastOccupier != null && Vector3.Distance(lastOccupier.transform.position, circle.transform.position) > CIRCLE_RADIUS) {
                lastOccupier = null;
            }
        }
	}

    public void EnterConsole(GameObject obj) {
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
            Unit stats = obj.GetComponent<Unit>();
            stats.console = gameObject;

            obj.transform.LookAt(screen.transform.position);

            gameObject.transform.parent.SendMessage("EnterConsole", obj);
        }
    }

    public void DisconnectConsole() {
        GameObject obj = occupier;
        if (obj != null) {
            occupier = null;
            lastOccupier = obj;

            // Clear unit's console
            Unit stats = obj.GetComponent<Unit>();
            stats.console = null;
        }
    }
}
