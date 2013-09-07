/*
 * Name: RTS Player
 * Author: James 'Sevion' Nhan and Erik 'Siretu'
 * Date: 17/07/2013
 * Version: 1.0.1.0
 * Description:
 * 		Player handling script
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using uLink;

public class Player : uLink.MonoBehaviour {
    private static int NumPlayers = 0;
    private static List<Player> Players = new List<Player>();
    public int id = 0;

    private static GameObject[] battlecruiser;

    private GameObject ship = null;
    private SpaceMovement movementscript = null;
    private UnitSelectionManager selectionManager;

    private uLink.NetworkPlayer networkPlayer;

    void Awake() {
        // In the multiplayer we will have to update the list and player ids upon joining a game
        id = NumPlayers++;
        Players.Add(this);
        Debug.Log("There are " + NumPlayers + " players. This is player: " + id);
        battlecruiser = GameObject.FindGameObjectsWithTag("Battlecruiser");
    }

    void Start() {
        selectionManager = GameObject.FindWithTag("UnitManager").GetComponent<UnitSelectionManager>();
    }

    void Update() {
        if (ship != null) {
            if (Input.GetKey(KeyCode.W)) {
                movementscript.addThrottle(0.1f * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S)) {
                movementscript.addThrottle(-0.1f * Time.deltaTime);
            }
        }
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
     * Returns a list of the selected units' Unit component.
     */
    public List<Unit> GetSelection() {
        List<Unit> units = new List<Unit>();
        foreach (GameObject obj in GetSelectedUnits()) {
            units.Add(obj.GetComponent<Unit>());
        }
        return units;
    }

    /**
     * Returns an array of the selected gameobjects.
     * 
     */
    public GameObject[] GetSelectedUnits() {
        return selectionManager.GetSelectedObjects();
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
            if (uLink.Network.player == player.networkPlayer) {
                return player;
            }
        }
        return null;
    }

    void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info) {
        networkPlayer = gameObject.transform.GetComponent<uLinkNetworkView>().owner;
    }

    // Returns the battlecruiser of the player.
    public static GameObject getTeamBattlecruiser(int player){
        return battlecruiser[0]; // TO-DO: Should actually distinguish between team 1 and team 2's battlecruiser.
    }

    public GameObject getShip() {
        return ship;
    }

    public void setShip(GameObject newShip) {
        ship = newShip;
        movementscript = ship.GetComponent<SpaceMovement>();
    }
}
