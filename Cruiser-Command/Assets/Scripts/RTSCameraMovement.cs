/*
 * Name: RTS Camera Movement
 * Author: James 'Sevion' Nhan
 * Date: 02/07/2013
 * Version: 1.0.1.0
 * Description:
 * 		This is a simple RTS movement script that handles
 * 		arrow keys, mouse edge, and scroll wheel zooming.
 * 		Also supports rotating the camera with Insert and
 * 		Delete.
 */

using UnityEngine;
using System.Collections;

public class RTSCameraMovement : MonoBehaviour {
	public const int SCROLLDISTANCE = 5;
	public const float SCROLLSPEED = 1.0f;
	public const float ZOOMSPEED = 5.0f;
    public const float ZOOMROTSPEED = 4.0f;
	public const float ROTSPEED = 5.0f;
	public const float MAXSCROLL = 50.0f;
	public const float MAXZOOM = 40.0f;
	public const float MAXROT = 90.0f;
	private float DEFAULTFOV = 0.0f;
	private float DEFAULTROT = 0.0f;
	private float DEFAULTZOOM;
	private const KeyCode ANGLELEFTKEY = KeyCode.Insert;
	private const KeyCode ANGLERIGHTKEY = KeyCode.Delete;

	void Start() {
		DEFAULTFOV = camera.fieldOfView;
		DEFAULTZOOM = camera.transform.eulerAngles.x;
		DEFAULTROT = camera.transform.eulerAngles.y;
	}

	// Update is called once per frame
	void Update() {
		// The camera's X rotation in radians
		float CameraAngleFromPerpendicular = camera.transform.eulerAngles.x * Mathf.PI / 180.0f;

		// Get the input data in the respective axes
		float xAxisValue = Input.GetAxis("Horizontal");
		// The Y and Z axes need to be adjusted if there's any X rotation in the camera
		// Y corresponds to Sin and Z corresponds to Cos
	    float yAxisValue = Input.GetAxis("Vertical") * Mathf.Sin(CameraAngleFromPerpendicular);
		float zAxisValue = Input.GetAxis("Vertical") * Mathf.Cos(CameraAngleFromPerpendicular);

		// Check that there is a current camera and then transform it to the new vector
	    if(camera != null){
	        //camera.transform.Translate(new Vector3(xAxisValue, yAxisValue, zAxisValue));
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
			xAxisValue = -SCROLLSPEED;
		} else if ((mousePosX >= Screen.width - SCROLLDISTANCE)) {
			xAxisValue = SCROLLSPEED;
        }
		// Y axis transformation
		// The Y and Z axes need to be adjusted if there's any X rotation in the camera
		// Y corresponds to Sin and Z corresponds to Cos
        if ((mousePosY < SCROLLDISTANCE)) {
			yAxisValue = -SCROLLSPEED * Mathf.Sin(CameraAngleFromPerpendicular);
			zAxisValue = -SCROLLSPEED * Mathf.Cos(CameraAngleFromPerpendicular);
        } else if ((mousePosY >= Screen.height - SCROLLDISTANCE)) {
			yAxisValue = SCROLLSPEED * Mathf.Sin(CameraAngleFromPerpendicular);
			zAxisValue = SCROLLSPEED * Mathf.Cos(CameraAngleFromPerpendicular);
        }

		// Check that there is a current camera and then transform it to the new vector
	    if(camera != null){
	        camera.transform.Translate(new Vector3(xAxisValue, yAxisValue, zAxisValue));
		}

		// Scrolling Zoom
		// Scrolling up or down
		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            // Clamping
            if (camera.fieldOfView - ZOOMSPEED > DEFAULTFOV - MAXSCROLL) {
				camera.fieldOfView -= ZOOMSPEED;
            } else {
                camera.fieldOfView = DEFAULTFOV - MAXSCROLL;
            }
			if (camera.transform.eulerAngles.x - ZOOMROTSPEED > DEFAULTZOOM - MAXZOOM) {
				camera.transform.eulerAngles -= new Vector3(ZOOMROTSPEED, 0, 0);
			} else {
				camera.transform.eulerAngles = new Vector3(DEFAULTZOOM - MAXZOOM, camera.transform.eulerAngles.y, camera.transform.eulerAngles.z);
			}
		} else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            // Clamping
            if (camera.fieldOfView + ZOOMSPEED < DEFAULTFOV) {
				camera.fieldOfView += ZOOMSPEED;
            } else {
                camera.fieldOfView = DEFAULTFOV;
            }
			if (camera.transform.eulerAngles.x + ZOOMROTSPEED < DEFAULTZOOM) {
				camera.transform.eulerAngles += new Vector3(ZOOMROTSPEED, 0, 0);
			} else {
				camera.transform.eulerAngles = new Vector3(DEFAULTZOOM, camera.transform.eulerAngles.y, camera.transform.eulerAngles.z);
			}
		}
		
		// Angle Left and Right
		if (Input.GetKey(ANGLELEFTKEY) && !Input.GetKey(ANGLERIGHTKEY)) {
			//Clamping
			if (camera.transform.eulerAngles.y - ROTSPEED > DEFAULTROT - MAXROT) {
				camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x, camera.transform.eulerAngles.y - ROTSPEED, camera.transform.eulerAngles.z);
			} else {
				camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x, DEFAULTROT - MAXROT, camera.transform.eulerAngles.z);
			}
		} else if (Input.GetKey(ANGLERIGHTKEY) && !Input.GetKey(ANGLELEFTKEY)) {
			//Clamping
			if (camera.transform.eulerAngles.y + ROTSPEED < DEFAULTROT + MAXROT) {
				camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x, camera.transform.eulerAngles.y + ROTSPEED, camera.transform.eulerAngles.z);
			} else {
				camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x, DEFAULTROT + MAXROT, camera.transform.eulerAngles.z);
			}
		} else if (!Input.GetKey(ANGLELEFTKEY) && !Input.GetKey(ANGLERIGHTKEY) && camera.transform.eulerAngles.y != DEFAULTROT) {
			// Return back to default rotation angle
			if (camera.transform.eulerAngles.y < DEFAULTROT) {
				// Clamping
				if (camera.transform.eulerAngles.y + ROTSPEED < DEFAULTROT) {
					camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x, camera.transform.eulerAngles.y + ROTSPEED, camera.transform.eulerAngles.z);
				} else {
					camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x, DEFAULTROT, camera.transform.eulerAngles.z);
				}
			} else if (camera.transform.eulerAngles.y > DEFAULTROT) {
				// Clamping
				if (camera.transform.eulerAngles.y - ROTSPEED > DEFAULTROT) {
					camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x, camera.transform.eulerAngles.y - ROTSPEED, camera.transform.eulerAngles.z);
				} else {
					camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x, DEFAULTROT, camera.transform.eulerAngles.z);
				}
			}
		}
	}
}
