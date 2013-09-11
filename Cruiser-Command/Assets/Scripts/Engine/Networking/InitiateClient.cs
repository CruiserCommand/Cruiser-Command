using UnityEngine;
using System.Collections;
using CC.eLog;

public class InitiateClient : uLink.MonoBehaviour {
    public string serverAddress = "178.174.215.92";
    public const int serverPort = 9001;
    public bool connectToServer = true;
	public string username;

	void Start() {
		GameObject buttonManager = GameObject.Find("ButtonActionManager");
		ButtonActions buttonActionScript = buttonManager.GetComponent("ButtonActions") as ButtonActions;
		serverAddress = buttonActionScript.serverAddress;
		username = buttonActionScript.username;
	}

    void OnGUI() {
        if (uLink.Network.peerType == uLink.NetworkPeerType.Disconnected && connectToServer) {
            uLink.Network.isAuthoritativeServer = true;
            uLink.Network.useNat = true;
            uLink.Network.Connect(serverAddress, serverPort);
        } else {
            string ipadress = uLink.Network.player.ipAddress;
            string port = uLink.Network.player.port.ToString();
            GUI.Label(new Rect(140, 20, 250, 40), "IP Address: " + ipadress + ":" + port);
            if (connectToServer) {
                GUI.Label(new Rect(140, 60, 350, 40), "Running as a client");
            } else {
                GUI.Label(new Rect(140, 60, 350, 40), "Running offline");
            }
        }
    }

    void uLink_OnConnectedToServer() {
        Log.Info("network", "Now connected to server");
        Log.Info("network", "Local port = " + uLink.Network.player.port.ToString());
		networkView.RPC("AddPlayer", uLink.RPCMode.Server, username);
    }
}
