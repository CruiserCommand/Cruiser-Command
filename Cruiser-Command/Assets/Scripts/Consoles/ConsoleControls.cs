using UnityEngine;
using System.Collections;

public class ConsoleControls : MonoBehaviour {

    private GameObject circle;
    private GameObject screen;

    private GameObject occupier = null;
    private GameObject lastOccupier = null;
    private UnitSelectionManager UnitManager;

    private const double CIRCLE_RADIUS = 2;

	// Use this for initialization
	void Start () {
        circle = gameObject.transform.Find("Circle").gameObject;
        screen = gameObject.transform.Find("Console Screen").gameObject;
        UnitManager = GameObject.FindWithTag("UnitManager").GetComponent<UnitSelectionManager>();
	}

	// Update is called once per frame
	void Update () {
        // If noone is in the console already...
        if (occupier == null) {
            // Loop through all marines.
            foreach (GameObject obj in Marine.GetAllMarines()) {
                // Check that it wasn't the last marine (who hasn't had time to move from the circle) and that the marine is in range.
                if (obj != lastOccupier && Vector3.Distance(obj.transform.position, circle.transform.position) <= CIRCLE_RADIUS)
                {
                    EnterConsole(obj);
                    break;
                }
            }

            // If the last marine to be in the console has left the circle; clear lastOccupier so s/he may enter again.
            if (lastOccupier != null && Vector3.Distance(lastOccupier.transform.position, circle.transform.position) > CIRCLE_RADIUS) {
                lastOccupier = null;
            }
        }
	}

    public void EnterConsole(GameObject obj) {
        // Save who is in the console right now
        occupier = obj;

        // Snap to circle
        Vector3 newPos = circle.transform.position;
        newPos.y = obj.transform.position.y;
        obj.transform.position = newPos;

        obj.transform.LookAt(screen.transform.position);

        // Halt movement TO-DO make use of abilities/orders
        UnitMovement movement = obj.GetComponent<UnitMovement>();
        movement.path = null;

        UnitManager.SelectUnit(gameObject.transform.parent.gameObject);

        obj.transform.LookAt(screen.transform.position);
        // Set unit's console
        Marine marine = obj.GetComponent<Marine>();
        marine.EnterConsole(gameObject);

        gameObject.transform.parent.SendMessage("EnterConsole", obj);
    }

    public void DisconnectConsole() {
        GameObject obj = occupier;
        if (obj != null) {
            occupier = null;
            lastOccupier = obj;

            // Clear unit's console
            Marine marine = obj.GetComponent<Marine>();
			marine.LeaveConsole();

			gameObject.transform.parent.SendMessage("DisconnectConsole");
        }
    }
}
