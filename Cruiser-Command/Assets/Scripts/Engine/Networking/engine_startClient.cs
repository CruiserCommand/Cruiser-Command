using UnityEngine;
using System.Collections;
using CC.eLog;

public class engine_startClient : uLink.MonoBehaviour {
    public string serverAddress = "127.0.0.1";
    public const int serverPort = 9001;
	public string username;
    public GameObject playerManager;
    private engine_startServer serverScript;

    public GameObject[] battlecruisers;

    // This is temporary. As soon as we get a proper lobby set-up, the players should get their player number from the lobby position.
    public int currPlayer;

    public GameObject[] players;

	void Start() {
		GameObject buttonManager = GameObject.Find("ButtonActionManager");
		ButtonActions buttonActionScript = buttonManager.GetComponent("ButtonActions") as ButtonActions;
		serverAddress = buttonActionScript.serverAddress;
		username = buttonActionScript.username;

        players = new GameObject[12];
        battlecruisers = new GameObject[2];
        currPlayer = 0;

        engine_startServer serverScript = GetComponent<engine_startServer>();
	}

    void OnGUI() {
        if (uLink.Network.peerType == uLink.NetworkPeerType.Disconnected) {
            uLink.Network.isAuthoritativeServer = true;
            uLink.Network.useNat = true;
            uLink.NetworkConnectionError error = uLink.Network.InitializeServer(32, serverPort);
            Debug.Log("Got error: " + error);
            bool startedServer = serverScript.startServer();
            if (startedServer == false) {
                uLink.Network.Connect("127.0.0.1", serverPort);
            }
        } else {
            string ipadress = uLink.Network.player.ipAddress;
            string port = uLink.Network.player.port.ToString();
            GUI.Label(new Rect(140, 20, 250, 40), "IP Address: " + ipadress + ":" + port);
            GUI.Label(new Rect(140, 60, 350, 40), "Running as a client");

        }
    }

    void uLink_OnConnectedToServer() {
        Log.Info("network", "Now connected to server");
        Log.Info("network", "Local port = " + uLink.Network.player.port.ToString());
		networkView.RPC("RegisterUser", uLink.RPCMode.Server);
        PlayerManager.InitiatePlayer(uLink.Network.player); // Registers the client as the current player in the Player manager.
        //GameObject playerManager = uLink.Network.Instantiate(uLink.Network.player, "PlayerManager", Vector3.zero, Quaternion.identity, 0);
    }

    void uLink_OnPlayerConnected(uLink.NetworkPlayer player) {
        //battlecruisers[0] = uLink.Network.Instantiate("Battlecruiser", Vector3.zero, Quaternion.identity, 0);
        Log.Info("network", "Player connected from " + player.ipAddress + ":" + player.port);
        GameObject owner = uLink.Network.Instantiate(player, "Player", Vector3.zero, Quaternion.identity, 0);

        uLink.NetworkViewID ID = owner.uLinkNetworkView().viewID;
        networkView.RPC("AddPlayer", uLink.RPCMode.AllBuffered, ID, currPlayer);
        //currPlayer++;
        //GameObject u = uLink.Network.Instantiate(player, proxyPrefab, ownerPrefab, serverPrefab, new Vector3(0f, 1.1f, 0f), Quaternion.identity, 0, currPlayer++);
        if (battlecruisers[0] != null) {
            //u.transform.parent = battlecruisers[0].transform;
        } else {
            Debug.Log("No battlecruiser");
        }
    }
}
