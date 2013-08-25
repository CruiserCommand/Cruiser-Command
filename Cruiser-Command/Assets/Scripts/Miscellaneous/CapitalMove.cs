using UnityEngine;
using System.Collections;

public class CapitalMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W)){
			Vector3 newPos = (new Vector3(1.0f,0.0f,0.0f) - gameObject.transform.position).normalized*Time.deltaTime;
			gameObject.transform.position += newPos;
		} 
		if(Input.GetKey(KeyCode.S)){
			Vector3 newPos = (new Vector3(-1.0f,0.0f,0.0f) - gameObject.transform.position).normalized*Time.deltaTime;
			gameObject.transform.position += newPos;
		} 
		if(Input.GetKey(KeyCode.D)){
			Vector3 newPos = (new Vector3(0.0f,0.0f,-1.0f) - gameObject.transform.position).normalized*Time.deltaTime;
			gameObject.transform.position += newPos;
		} 
		if(Input.GetKey(KeyCode.A)){
			Vector3 newPos = (new Vector3(0.0f,0.0f,1.0f) - gameObject.transform.position).normalized*Time.deltaTime;
			gameObject.transform.position += newPos;
		}
		if(Input.GetKey(KeyCode.E)){
			transform.Rotate(0,25*Time.deltaTime,0);
		}
		if(Input.GetKey(KeyCode.Q)){
			transform.Rotate(0,-25*Time.deltaTime,0);
		}
	}
}