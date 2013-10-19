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
    
    public int id = 0;

    private static GameObject[] battlecruiser;

    private GameObject ship = null;
    private SpaceMovement movementscript = null;
    private UnitSelectionManager selectionManager;

    public uLink.NetworkPlayer networkPlayer;

    void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info) {
        networkPlayer = gameObject.transform.GetComponent<uLinkNetworkView>().owner;
        Debug.Log("Instantiated player and got: " + id);
    }

    void Start() {
        selectionManager = GameObject.FindWithTag("UnitManager").GetComponent<UnitSelectionManager>();
        Debug.Log("Started player and got: " + id);
    }

    void Awake() {
        // In the multiplayer we will have to update the list and player ids upon joining a game
        //id = Players.IndexOf(this);
        //Players.Add(this);
        //Debug.Log("There are " + PlayerManager.Players.Count + " players. This is player: " + id);
        battlecruiser = GameObject.FindGameObjectsWithTag("Battlecruiser");
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


    /**
     * Gets this player's unique integer identifier.
     */
    public int getId() {
        return id;
    }

    /**
     * Sets this player's unique integer identifier.
     */
    public void setId(int newId) {
        id = newId;
        if (PlayerManager.clientPlayer == networkPlayer) {
            PlayerManager.id = newId;
        }
    }
}
