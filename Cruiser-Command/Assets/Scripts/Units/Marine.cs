using UnityEngine;
using System.Collections.Generic;

// Main player unit.
public class Marine : Crew {
    // Console this marine is in.
    private GameObject console = null;

    public bool InConsole()
    {
        return console == null;
    }

    public GameObject GetConsole()
    {
        return console;
    }

    public void EnterConsole(GameObject console)
    {
        this.console = console;
    }

    public void LeaveConsole()
    {
        this.console = null;
    }

    // Run once for each marine.
    public void Awake()
    {
        Marine marine = gameObject.GetComponent<Marine>();
        allMarines.Add(gameObject);

        marine.abilities.Add(new GroundMove(gameObject));
    }

    // List of all marines that have been created.
    private static List<GameObject> allMarines;

    // Get a list of all marines that have been created.
    public static List<GameObject> GetAllMarines()
    {
        return new List<GameObject>(allMarines);
    }
}