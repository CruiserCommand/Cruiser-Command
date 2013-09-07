using UnityEngine;
using System.Collections.Generic;

// Represents a unit (e.g. ship, crew); health, other unit-stuff.
public class UnitNotInUse : MonoBehaviour
{
    // The health of this Unit.
    public double health { get; set; }

    // List of all abilities this unit has.
    private List<Ability> abilities = new List<Ability>();

    // List of all instances of Unit:s.
    private static List<GameObject> allUnits = new List<GameObject>();

    // Get all abilities this unit has.
    public List<Ability> GetAbilities()
    {
        return new List<Ability>(abilities);
    }

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