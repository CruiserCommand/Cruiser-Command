using UnityEngine;
using System.Collections;

public class Units : MonoBehaviour {

    public static Units instance;

    public ArrayList playerUnits;

    void Awake() {
        if (instance != null) {
            Debug.LogError("Cannot have two instances of singleton. Self destruction in 3...");
            return;
        }
        instance = this;
    }

    void Start() {
        playerUnits = new ArrayList();
        foreach (GameObject obj in GameObject.FindObjectsOfType(typeof(GameObject))) {
            if (obj.name == "PlayerUnit") {
                playerUnits.Add(obj);
                Debug.Log("Added unit");
            }
        }
    }



}
