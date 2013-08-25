/*
 * Name: RTS Camera Movement
 * Author: James 'Sevion' Nhan
 * Date: 02/07/2013
 * Version: 1.0.2.0
 * Description:
 * 		This is a simple RTS movement script that handles
 * 		arrow keys, mouse edge, and scroll wheel zooming.
 * 		Also supports rotating the Camera.main with Insert and
 * 		Delete.
 */

using UnityEngine;
using System.Collections;

public class RTSCameraMovement : MonoBehaviour {
	public const int SCROLLDISTANCE = 5;
	public const float SCROLLSPEED = 30.0f;
    public const float ZOOMSPEED = 5.0f;
	public const float ROTSPEED = 5.0f;
	public const float MAXZOOM = 29.0f;
	public const float MAXROT = 90.0f;
	private float DEFAULTROT = 0.0f;
	private float DEFAULTZOOM = 0.0f;
    private float DEFAULTHEIGHT = 0.0f;
	private const KeyCode ANGLELEFTKEY = KeyCode.Insert;
	private const KeyCode ANGLERIGHTKEY = KeyCode.Delete;

	void Start() {
		DEFAULTZOOM = Camera.main.transform.eulerAngles.x;
		DEFAULTROT = Camera.main.transform.eulerAngles.y;
        DEFAULTHEIGHT = Camera.main.transform.position.y;
	}

	// Update is called once per frame
	void Update() {
		// The Camera.main's X rotation in radians
		float CameraAngleFromPerpendicular = Camera.main.transform.eulerAngles.x * Mathf.PI / 180.0f;

        int left = 0, right = 0, up = 0, down = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            left = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            right = 1;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            up = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            down = -1;
        }

		// Get the input data in the respective axes
		float xAxisValue = (left + right) * SCROLLSPEED * Time.deltaTime;
		// The Y and Z axes need to be adjusted if there's any X rotation in the Camera.main
		// Y corresponds to Sin and Z corresponds to Cos
	    float yAxisValue = (up + down) * SCROLLSPEED * Mathf.Sin(CameraAngleFromPerpendicular) * Time.deltaTime;
        float zAxisValue = (up + down) * SCROLLSPEED * Mathf.Cos(CameraAngleFromPerpendicular) * Time.deltaTime;

		// Check that there is a current Camera.main and then transform it to the new vector
	    if(Camera.main != null){
	        Camera.main.transform.Translate(new Vector3(xAxisValue, yAxisValue, zAxisValue));
	    }

		// Get the mouse's coordinates
		float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;

		// Reset these values
		xAxisValue = 0.0f;
		yAxisValue = 0.0f;
		zAxisValue = 0.0f;

		// X axis transformation
        if ((mousePosX < SCROLLDISTANCE)) {
            xAxisValue = -SCROLLSPEED / 2.0f * Time.deltaTime;
		} else if ((mousePosX >= Screen.width - SCROLLDISTANCE)) {
			xAxisValue = SCROLLSPEED / 2.0f * Time.deltaTime;
        }
		// Y axis transformation
		// The Y and Z axes need to be adjusted if there's any X rotation in the Camera.main
		// Y corresponds to Sin and Z corresponds to Cos
        if ((mousePosY < SCROLLDISTANCE)) {
			yAxisValue = -SCROLLSPEED * Mathf.Sin(CameraAngleFromPerpendicular) * Time.deltaTime;
			zAxisValue = -SCROLLSPEED * Mathf.Cos(CameraAngleFromPerpendicular) * Time.deltaTime;
        } else if ((mousePosY >= Screen.height - SCROLLDISTANCE)) {
			yAxisValue = SCROLLSPEED * Mathf.Sin(CameraAngleFromPerpendicular) * Time.deltaTime;
			zAxisValue = SCROLLSPEED * Mathf.Cos(CameraAngleFromPerpendicular) * Time.deltaTime;
        }

		// Check that there is a current Camera.main and then transform it to the new vector
	    if(Camera.main != null){
	        Camera.main.transform.Translate(new Vector3(xAxisValue, yAxisValue, zAxisValue));
		}

		// Scrolling Zoom
		// Scrolling up or down
		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            // Clamping
			if (Camera.main.transform.eulerAngles.x - ZOOMSPEED > DEFAULTZOOM - MAXZOOM) {
				Camera.main.transform.eulerAngles -= new Vector3(ZOOMSPEED, 0, 0);
                Camera.main.transform.position -= new Vector3(0, ZOOMSPEED / 2.0f, 0);
			} else {
				Camera.main.transform.eulerAngles = new Vector3(DEFAULTZOOM - MAXZOOM, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
			}
		} else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            // Clamping
			if (Camera.main.transform.eulerAngles.x + ZOOMSPEED < DEFAULTZOOM) {
                Camera.main.transform.eulerAngles += new Vector3(ZOOMSPEED, 0, 0);
                Camera.main.transform.position += new Vector3(0, ZOOMSPEED / 2.0f, 0);
			} else {
                Camera.main.transform.eulerAngles = new Vector3(DEFAULTZOOM, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, DEFAULTHEIGHT, Camera.main.transform.position.z);
			}
		}
		
		// Angle Left and Right
		if (Input.GetKey(ANGLELEFTKEY) && !Input.GetKey(ANGLERIGHTKEY)) {
			//Clamping
			if (Camera.main.transform.eulerAngles.y - ROTSPEED > DEFAULTROT - MAXROT) {
				Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y - ROTSPEED, Camera.main.transform.eulerAngles.z);
			} else {
				Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, DEFAULTROT - MAXROT, Camera.main.transform.eulerAngles.z);
			}
		} else if (Input.GetKey(ANGLERIGHTKEY) && !Input.GetKey(ANGLELEFTKEY)) {
			//Clamping
			if (Camera.main.transform.eulerAngles.y + ROTSPEED < DEFAULTROT + MAXROT) {
				Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y + ROTSPEED, Camera.main.transform.eulerAngles.z);
			} else {
				Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, DEFAULTROT + MAXROT, Camera.main.transform.eulerAngles.z);
			}
		} else if (!Input.GetKey(ANGLELEFTKEY) && !Input.GetKey(ANGLERIGHTKEY) && Camera.main.transform.eulerAngles.y != DEFAULTROT) {
			// Return back to default rotation angle
			if (Camera.main.transform.eulerAngles.y < DEFAULTROT) {
				// Clamping
				if (Camera.main.transform.eulerAngles.y + ROTSPEED < DEFAULTROT) {
					Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y + ROTSPEED, Camera.main.transform.eulerAngles.z);
				} else {
					Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, DEFAULTROT, Camera.main.transform.eulerAngles.z);
				}
			} else if (Camera.main.transform.eulerAngles.y > DEFAULTROT) {
				// Clamping
				if (Camera.main.transform.eulerAngles.y - ROTSPEED > DEFAULTROT) {
					Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y - ROTSPEED, Camera.main.transform.eulerAngles.z);
				} else {
					Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, DEFAULTROT, Camera.main.transform.eulerAngles.z);
				}
			}
		}
	}
}
