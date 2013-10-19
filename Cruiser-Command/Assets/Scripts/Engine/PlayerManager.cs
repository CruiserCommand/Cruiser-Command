using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    public static int id = 0;

    public static uLink.NetworkPlayer clientPlayer;
    public static List<Player> Players = new List<Player>();


    /**
     * Used to initiate the player playing on _this_ client so the client can differentiate the player from all other players.
     */
    public static void InitiatePlayer(uLink.NetworkPlayer player) {
        clientPlayer = player;
        Debug.Log("Instantiated player");
    }

    public void AddResource(int Amount, Resource Type) {
        ResourceManager.Resources[id, (int)Type] += Amount;
    }

    public void SubtractResource(int Amount, Resource Type) {
        ResourceManager.Resources[id, (int)Type] -= Amount;
    }

    public void SetResource(int Amount, Resource Type) {
        ResourceManager.Resources[id, (int)Type] = Amount;
    }

    public int GetResource(Resource Type) {
        return ResourceManager.Resources[id, (int)Type];
    }

    /**
 * RPC sent by server for each player in the game. Will be sent to everyone to update their static players list.
 */
    [RPC]
    public void AddPlayer(uLink.NetworkViewID netid, int i, uLink.NetworkMessageInfo info) {
        Debug.Log("ADDED A PLAYER. WOO");
        // Find the gameobject with the provided netid.
        GameObject player = uLink.NetworkView.Find(netid).gameObject;
        Player p = player.GetComponent<Player>();
        Players.Add(p);
        p.id = i;
    }

    public static Player GetPlayer(int id) {
        foreach (Player player in Players) {
            if (id == player.id) {
                return player;
            }
        }

        return null;
    }

    public static Player GetCurrentPlayer() {
        foreach (Player player in Players) {
            if (uLink.Network.player == PlayerManager.clientPlayer) {
                return player;
            }
        }
        return null;
    }
}
