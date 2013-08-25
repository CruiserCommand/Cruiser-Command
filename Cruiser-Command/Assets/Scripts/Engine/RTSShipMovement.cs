/*
 * Name: RTS Ship Movement
 * Author: James 'Sevion' Nhan, Erik 'Siretu', and Aron Granberg
 * Date: 02/08/2013
 * Version: 1.0.0.0
 * Description:
 *		This is a movement system that handles moving
 *		ship units outside of the battlecruiser.
 */

using UnityEngine;
using System.Collections;

public class RTSShipMovement : MonoBehaviour {
    private RTSUnitSelectionManager UnitManager;
    private const float MOVESPEED = 10.0f;
    private const float DECAYDELTA = 0.01f;
    private float Throttle = 0.0f;
    private float Facing;

    void Start() {
        UnitManager = GameObject.FindWithTag("UnitManager").GetComponent<RTSUnitSelectionManager>();
        Facing = gameObject.transform.eulerAngles.y * Mathf.Deg2Rad;
    }

    void Update() {
        if (UnitManager.IsSelected(gameObject)) {
            if (Input.GetKey(KeyCode.W)) {
                Throttle += 0.1f * Time.deltaTime;

                if (Throttle > 1.0f) {
                    Throttle = 1.0f;
                }
            }
            if (Input.GetKey(KeyCode.S)) {
                Throttle -= 0.1f * Time.deltaTime;
                if (Throttle < -1.0f) {
                    Throttle = -1.0f;
                }
            }
        }

        // Velocity decay
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
            float f = Mathf.Sign(Throttle) * 0.1f * Time.deltaTime;
            Throttle -= f;
            if (Mathf.Abs(Throttle) < DECAYDELTA) {
                Throttle = 0;
            }
        }
        Facing = gameObject.transform.eulerAngles.y * Mathf.Deg2Rad;

        if (Throttle > 0.0001f || Throttle < -0.0001f) {
            gameObject.transform.position -= new Vector3(MOVESPEED * -Mathf.Sin(Facing), 0, MOVESPEED * -Mathf.Cos(Facing)) * Throttle * Time.deltaTime;
        }
	}
}
