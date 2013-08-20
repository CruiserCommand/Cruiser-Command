using UnityEngine;
using System.Collections;
using System;
using TNet;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Net;

public class InitiateServer : MonoBehaviour {
    /// <summary>
    /// Application entry point -- parse the parameters.
    /// </summary>


    public static UPnP up;

    public static GameServer gameServer = null;
    public static LobbyServer lobbyServer = null;

    public bool active = true;

    void Start() {
        // TODO: Make it possible for the lobby servers to save & load files.
        // - Client connects to the lobby.
        // - Client gets a list of servers.
        // - Client may request an AccountID if one was not found.
        // - Lobby will provide an ever-incrementing AccountID for that player. This needs to be saved on server shutdown.
        // - Player will pass this AccountID back to the server in order to save their progress (achievements and such).
        // - This AccountID identifier makes it possible to send PMs, mail, and /friend the player.


        string name = "TNet Server";
        int tcpPort = 5666;
        int udpPort = 5667;
        string lobbyAddress = null;
        int lobbyPort =5668;
        bool tcpLobby = false;

        StartServer(name, tcpPort, udpPort, lobbyAddress, lobbyPort, tcpLobby);
    }

    void Update() {
        if (Input.GetKey(KeyCode.Q) && active) {
            active = false;
            Debug.Log("Shutting down...");

            // Close all opened ports
            if (up != null) {
                up.Close();
                up.WaitForThreads();
                up = null;
            }

            // Stop the game server
            if (gameServer != null) {
                gameServer.SaveTo("server.dat");
                gameServer.Stop();
                gameServer = null;
            }

            // Stop the lobby server
            if (lobbyServer != null) {
                lobbyServer.Stop();
                lobbyServer = null;
            }


            Debug.Log("There server has shut down.");
            Application.Quit();
        }
    }

    /// <summary>
    /// Start the server.
    /// </summary>

    static void StartServer(string name, int tcpPort, int udpPort, string lobbyAddress, int lobbyPort, bool useTcp) {
        Debug.Log("IP Addresses\n------------");
        Debug.Log("External: " + Tools.externalAddress);
        Debug.Log("Internal: " + Tools.localAddress);
        {
            // Universal Plug & Play is used to determine the external IP address,
            // and to automatically open up ports on the router / gateway.
            up = new UPnP();
            up.WaitForThreads();

            if (up.status == UPnP.Status.Success) {
                Debug.Log("Gateway:  " + up.gatewayAddress + "\n");
            } else {
                Debug.Log("Gateway:  None found\n");
                up = null;
            }


            if (tcpPort > 0) {
                gameServer = new GameServer();
                gameServer.name = name;

                if (!string.IsNullOrEmpty(lobbyAddress)) {
                    // Remote lobby address specified, so the lobby link should point to a remote location
                    IPEndPoint ip = Tools.ResolveEndPoint(lobbyAddress, lobbyPort);
                    if (useTcp) gameServer.lobbyLink = new TcpLobbyServerLink(ip);
                    else gameServer.lobbyLink = new UdpLobbyServerLink(ip);

                } else if (lobbyPort > 0) {
                    // Server lobby port should match the lobby port on the client
                    if (useTcp) {
                        lobbyServer = new TcpLobbyServer();
                        lobbyServer.Start(lobbyPort);
                        if (up != null) up.OpenTCP(lobbyPort, OnPortOpened);
                    } else {
                        lobbyServer = new UdpLobbyServer();
                        lobbyServer.Start(lobbyPort);
                        if (up != null) up.OpenUDP(lobbyPort, OnPortOpened);
                    }

                    // Local lobby server
                    gameServer.lobbyLink = new LobbyServerLink(lobbyServer);
                }

                // Start the actual game server and load the save file
                gameServer.Start(tcpPort, udpPort);
                gameServer.LoadFrom("server.dat");
            } else if (lobbyPort > 0) {
                if (useTcp) {
                    if (up != null) up.OpenTCP(lobbyPort, OnPortOpened);
                    lobbyServer = new TcpLobbyServer();
                    lobbyServer.Start(lobbyPort);
                } else {
                    if (up != null) up.OpenUDP(lobbyPort, OnPortOpened);
                    lobbyServer = new UdpLobbyServer();
                    lobbyServer.Start(lobbyPort);
                }
            }

            // Open up ports on the router / gateway
            if (up != null) {
                if (tcpPort > 0) up.OpenTCP(tcpPort, OnPortOpened);
                if (udpPort > 0) up.OpenUDP(udpPort, OnPortOpened);
            }

        }
    }

    void OnKeyDown() {

    }

    /// <summary>
    /// UPnP notification of a port being open.
    /// </summary>

    static void OnPortOpened(UPnP up, int port, ProtocolType protocol, bool success) {
        if (success) {
            Debug.Log("UPnP: " + protocol.ToString().ToUpper() + " port " + port + " was opened successfully.");
        } else {
            Debug.Log("UPnP: Unable to open " + protocol.ToString().ToUpper() + " port " + port);
        }
    }
}
