/*
 * Name: RTS Resources
 * Author: James 'Sevion' Nhan and Erik 'Siretu' Ihren
 * Date: 17/07/2013
 * Version: 1.0.0.1
 * Description:
 * 		This is a simple RTS movement script that handles
 * 		player resources.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Resource {
    Yellow, Blue, Red, Green
};

public class ResourceManager : MonoBehaviour {
    public static int[,] Resources = new int[4, 4];
    public UILabel Yellow;
    public UILabel Blue;
    public UILabel Red;
    public UILabel Green;

    public void AddResource(int id, Resource Resource, int Amount) {
        Resources[id, (int)Resource] += Amount;
    }

    public void SubtractResource(int id, Resource Resource, int Amount) {
        Resources[id, (int)Resource] -= Amount;
    }

    public void SetResource(int id, Resource Resource, int Amount) {
        Resources[id, (int)Resource] = Amount;
    }

    public int GetResource(int id, Resource Resource) {
        return Resources[id, (int)Resource];
    }

    void Start() {
        Resources[1, (int)Resource.Yellow] = 0;
        Resources[1, (int)Resource.Blue] = 0;
        Resources[1, (int)Resource.Red] = 0;
        Resources[1, (int)Resource.Green] = 0;
        Resources[2, (int)Resource.Yellow] = 0;
        Resources[2, (int)Resource.Blue] = 0;
        Resources[2, (int)Resource.Red] = 0;
        Resources[2, (int)Resource.Green] = 0;
    }

    void Update() {
        Yellow.text = Resources[1, (int)Resource.Yellow].ToString();
        Blue.text = Resources[1, (int)Resource.Blue].ToString();
        Red.text = Resources[1, (int)Resource.Red].ToString();
        Green.text = Resources[1, (int)Resource.Green].ToString();
    }
}
