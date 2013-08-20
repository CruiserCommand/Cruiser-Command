using UnityEngine;
using System.Collections;

public class InitiateClient : MonoBehaviour {
    public string serverAddress = "127.0.0.1";
    public int serverPort = 5666;

	// Use this for initialization
	void Start () {

        // Make it possible to use UDP using a random port
        //TNManager.StartUDP(Random.Range(10000, 50000));

        // Connect to the remote server
        //TNManager.Connect(serverAddress, serverPort);

        TNManager.Connect("127.0.0.1:"+serverPort);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnNetworkConnect(bool success, string message) {
        Debug.Log("Connected!: " + success+ " | " + message);
        Debug.Log(TNManager.isConnected);
        Debug.Log(TNManager.isInChannel);

    }
}
