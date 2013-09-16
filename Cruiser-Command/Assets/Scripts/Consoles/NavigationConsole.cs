using UnityEngine;
using System.Collections;

public class NavigationConsole : MonoBehaviour {

    public Player player;
    private UnitSelectionManager UnitManager;


	// Use this for initialization
	void Start () {
        UnitManager = GameObject.FindWithTag("UnitManager").GetComponent<UnitSelectionManager>();
        //gameObject.GetComponent<Unit>().abilities.Add(new Move());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // EnterConsole is called when a unit enters this console. obj is the unit entering the console.
    void EnterConsole(GameObject obj) {
        UnitManager.ClearSelection();
        UnitManager.SelectUnit(gameObject);
        //Debug.Log(Player.getTeamBattlecruiser(1));
        //player = obj.GetComponent<Unit>().owner;
        //Debug.Log(player);
        //player.setShip(Player.getTeamBattlecruiser(1));
    }
}
