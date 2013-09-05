using UnityEngine;
using System.Collections;

public class CapitalMove : MonoBehaviour {

    private float movementSpeed = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W)){
			Vector3 newPos = new Vector3(1.0f,0.0f,0.0f) *Time.deltaTime*movementSpeed;
			gameObject.transform.position += newPos;
		} 
		if(Input.GetKey(KeyCode.S)){
            Vector3 newPos = new Vector3(-5.0f, 0.0f, 0.0f);
			gameObject.transform.position = newPos;
		} 
		if(Input.GetKey(KeyCode.D)){
			Vector3 newPos = new Vector3(0.0f,0.0f,-1.0f) *Time.deltaTime*movementSpeed;
			gameObject.transform.position += newPos;
		} 
		if(Input.GetKey(KeyCode.A)){
            Vector3 newPos = new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime * movementSpeed;
			gameObject.transform.position += newPos;
		}
		if(Input.GetKey(KeyCode.E)){
			transform.Rotate(0,25*Time.deltaTime,0);
		}
		if(Input.GetKey(KeyCode.Q)){
			transform.Rotate(0,-25*Time.deltaTime,0);
		}
        if (Input.GetKey(KeyCode.R)) {
            gameObject.transform.Find("PlayerUnit Owner").GetComponent<UnitMovement>().Move(new Vector3(-5f, 1.05f, -5f));
        }
	}
}