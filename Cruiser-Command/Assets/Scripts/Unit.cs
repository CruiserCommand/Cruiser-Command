using UnityEngine;
using System.Collections.Generic;

// Represents a unit; health, other unit-stuff.
public class Unit : MonoBehaviour
{
    // The health of this Unit.
    public double health { get; set; }

    // Console that this Unit is currently attached to.
    public GameObject console;

    // List of all instances of Unit:s.
    private static List<GameObject> allUnits = new List<GameObject>();

    // Get the GameObjects of all Unit:s that have been created.
    public static List<GameObject> GetAllUnitsObjects()
    {
        return new List<GameObject>(allUnits);
    }

    // Run once for each instance of this; i.e. once for each time this is attached as a component.
    public void Awake()
    {
        allUnits.Add(gameObject);
    }
}