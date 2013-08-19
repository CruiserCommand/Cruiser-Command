using UnityEngine;
using System.Collections;

public class InitiateServer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TNServerInstance.Start(5666);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
