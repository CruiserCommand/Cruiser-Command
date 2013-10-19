using UnityEngine;
using System.Collections;
using CC.eLog;

public class InitiateServer : uLink.MonoBehaviour {
	public const int serverPort = 9001;
	public UILabel serverText;
    public GameObject proxyPrefab = null;
    public GameObject ownerPrefab = null;
    public GameObject serverPrefab = null;

    public GameObject[] battlecruisers;

    // This is temporary. As soon as we get a proper lobby set-up, the players should get their player number from the lobby position.
    public int currPlayer;

    public GameObject[] players;

	void Start() {
        players = new GameObject[12];
        battlecruisers = new GameObject[2];
        currPlayer = 0;
    }

    void OnGUI() {
		if (uLink.Network.peerType == uLink.NetworkPeerType.Disconnected) {
            uLink.Network.isAuthoritativeServer = true;
            uLink.Network.useNat = true;
            uLink.Network.InitializeServer(32, serverPort);
			//uLink.Network.Instantiate("Console", new Vector3(-11f, 1f, 20f), Quaternion.identity, 0);
			serverText.text = "Server IP: " + uLink.Network.player.ipAddress + ":" + serverPort.ToString();
        } else {
			string ipadress = uLink.Network.player.ipAddress;
            
            string port = uLink.Network.player.port.ToString();
			GUI.Label(new Rect(140, 20, 250, 40), "IP Address: " + ipadress + ":" + port);
			GUI.Label(new Rect(140, 60, 350, 40), "Running as a server");
        }

    }

    void uLink_OnServerInitialized() {  
        Log.Fatal("general", "Server successfully started");
        
    }

    void uLink_OnPlayerDisconnected(uLink.NetworkPlayer player) {
        uLink.Network.DestroyPlayerObjects(player);
        uLink.Network.RemoveRPCs(player);
    }

    void uLink_OnFailedToConnect(uLink.NetworkConnectionError error) {
        Log.Error("network", "uLink got error: " + error);
    }

    void uLink_OnPlayerConnected(uLink.NetworkPlayer player) {
        //battlecruisers[0] = uLink.Network.Instantiate("Battlecruiser", Vector3.zero, Quaternion.identity, 0);
        Log.Info("network", "Player connected from " + player.ipAddress + ":" + player.port);
        GameObject owner = uLink.Network.Instantiate(player,"Player", Vector3.zero, Quaternion.identity, 0);

        uLink.NetworkViewID ID = owner.uLinkNetworkView().viewID;
        networkView.RPC("AddPlayer", uLink.RPCMode.AllBuffered, ID, currPlayer);
        //currPlayer++;
        GameObject u = uLink.Network.Instantiate(player, proxyPrefab, ownerPrefab, serverPrefab, new Vector3(0f, 1.1f, 0f), Quaternion.identity, 0, currPlayer++);
        if (battlecruisers[0] != null) {
            //u.transform.parent = battlecruisers[0].transform;
        } else {
            Debug.Log("No battlecruiser");
        }
    }

	[RPC]
	public void RegisterUser(string Name, uLink.NetworkMessageInfo info) {
		UILabel PlayerList = GameObject.Find("PlayerList").GetComponent<UILabel>() as UILabel;
        PlayerList.text += "\n    " + Name + "\n        IP: " + info.sender.ipAddress + "\n        id: " + info.sender.id;
        
	}
}