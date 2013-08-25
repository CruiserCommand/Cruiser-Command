using UnityEngine;
using System.Collections;
using CC.eLog;

public class PowerConsole : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void EnterConsole(GameObject obj) {
        Log.Trace("console", "Entered power console circle");
    }
}
