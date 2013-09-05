using UnityEngine;
using System.Collections;

public class NavigationConsole : MonoBehaviour {

    public Player player;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // EnterConsole is called when a unit enters this console. obj is the unit entering the console.
    void EnterConsole(GameObject obj) {
        Debug.Log(Player.getTeamBattlecruiser(1));
        player = obj.GetComponent<Unit>().owner;
        player.setShip(Player.getTeamBattlecruiser(1));
    }
}
