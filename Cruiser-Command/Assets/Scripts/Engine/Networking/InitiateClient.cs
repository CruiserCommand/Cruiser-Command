using UnityEngine;
using System.Collections;
using CC.eLog;

public class InitiateClient : MonoBehaviour {
    public string serverAddress = "178.174.215.92";
    public int serverPort = 5666;
    public bool connectToServer = true;

    void OnGUI(){
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
    }
}
