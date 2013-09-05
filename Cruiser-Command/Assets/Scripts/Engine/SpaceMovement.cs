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

public class SpaceMovement : MonoBehaviour {
    private const float MOVESPEED = 10.0f;
    private const float DECAYDELTA = 0.01f;
    private float Throttle = 0.0f;
    private float Facing;

    void Start() {
        Facing = gameObject.transform.eulerAngles.y * Mathf.Deg2Rad;
    }

    void Update() {
        Facing = gameObject.transform.eulerAngles.y * Mathf.Deg2Rad;

        if (Throttle > 0.0001f || Throttle < -0.0001f) {
            gameObject.transform.position -= new Vector3(MOVESPEED * -Mathf.Sin(Facing), 0, MOVESPEED * -Mathf.Cos(Facing)) * Throttle * Time.deltaTime;
        }
	}

    public void addThrottle(float add) {
        Throttle += add;
        Throttle = Mathf.Min(Mathf.Max(-1f, Throttle), 1);
    }

    public float getThrottle() {
        return Throttle;
    }

    public void setThrottle(float newThrottle) {
        Throttle = newThrottle;
        Throttle = Mathf.Min(Mathf.Max(-1f, Throttle), 1);
    }
}
