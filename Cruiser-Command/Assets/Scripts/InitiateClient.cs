using UnityEngine;
using System.Collections;

public class InitiateClient : MonoBehaviour {
    public string serverAddress = "127.0.0.1";
    public int serverPort = 5666;

    void OnGUI(){
        if (uLink.Network.peerType == uLink.NetworkPeerType.Disconnected) {
            uLink.Network.isAuthoritativeServer = true;
            uLink.Network.Connect(serverAddress, serverPort);
        } else {
            string ipadress = uLink.Network.player.ipAddress;
            string port = uLink.Network.player.port.ToString();
            GUI.Label(new Rect(140, 20, 250, 40), "IP Address: " + ipadress + ":" + port);
            GUI.Label(new Rect(140, 60, 350, 40), "Running as a client");
        }

    }

    void uLink_OnConnectedToServer() {
        Debug.Log("Now connected to server");
        Debug.Log("Local port = " + uLink.Network.player.port.ToString());
    }
}
