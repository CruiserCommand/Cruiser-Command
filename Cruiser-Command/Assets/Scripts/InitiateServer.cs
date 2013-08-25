using UnityEngine;
using System.Collections;

public class InitiateServer : MonoBehaviour {
    public string serverAddress = "127.0.0.1";
    public int serverPort = 5666;
    public GameObject proxyPrefab = null;
    public GameObject ownerPrefab = null;
    public GameObject serverPrefab = null;

    void OnGUI() {
        if (uLink.Network.peerType == uLink.NetworkPeerType.Disconnected) {
            uLink.Network.isAuthoritativeServer = true;
            uLink.Network.useNat = true;
            uLink.Network.InitializeServer(32, serverPort);
        } else {
            string ipadress = uLink.Network.player.ipAddress;
            
            string port = uLink.Network.player.port.ToString();
            GUI.Label(new Rect(140, 20, 250, 40), "IP Address: " + ipadress + ":" + port);
            GUI.Label(new Rect(140, 60, 350, 40), "Running as a server");
        }

    }

    void uLink_OnServerInitialized() {
        Debug.Log("Server successfully started");
    }

    void uLink_OnPlayerDisconnected(uLink.NetworkPlayer player) {
        uLink.Network.DestroyPlayerObjects(player);
        uLink.Network.RemoveRPCs(player);
    }

    void uLink_OnFailedToConnect(uLink.NetworkConnectionError error) {
        Debug.LogError("uLink got error: " + error);
    }

    void uLink_OnPlayerConnected(uLink.NetworkPlayer player) {
        Debug.Log("Player connected from " + player.ipAddress + ":" + player.port);
        //uLink.Network.Instantiate(prefab, new Vector3(-9f, -9f, 9f), Quaternion.identity,0);
        uLink.Network.Instantiate(player, proxyPrefab, ownerPrefab, serverPrefab    , new Vector3(-9f, 1f, 9f), Quaternion.identity, 0);
    }
}
