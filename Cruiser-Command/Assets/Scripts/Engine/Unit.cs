using UnityEngine;
using System.Collections.Generic;

// Represents a unit; health, other unit-stuff.
public class UnitNotInUse : MonoBehaviour
{
    // The health of this Unit.
    public double health { get; set; }

    // Console that this Unit is currently attached to.
    public GameObject console;

    // Player that owns this unit.
    public Player owner;

    // List of all instances of Unit:s.
    private static List<GameObject> allUnits = new List<GameObject>();

    // List of all abilities this unit has.
    public List<Ability> abilities = new List<Ability>();

    // Run once for each instance of this; i.e. once for each time this is attached as a component.
    public void Awake()
    {
        allUnits.Add(gameObject);
    }

    // Get all abilities this unit has.
    public List<Ability> GetAbilities()
    {
        return new List<Ability>(abilities);
    }

    // Get the GameObjects of all Unit:s that have been created.
    public static List<GameObject> GetAllUnitsObjects() {
        return new List<GameObject>(allUnits);
    }

    void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info) {
        //abilities.Add(new Move());
        if (info == null) {
            Debug.Log("info == null");
        }

        if (info.networkView == null) {
            Debug.Log("networkView == null");
        }

        if (info.networkView.initialData == null) {
            Debug.Log("initialData == null");
        } else {
            Debug.Log("initialData NOT null");
        }
        int num = info.networkView.initialData.ReadInt32();
        Debug.Log("Got number: " + num);
        owner = Player.GetPlayer(num);
        //Debug.Log("Got owner: " + owner.number);
    }
}