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

public class RTSResources : MonoBehaviour {
    public static int[,] Resources = new int[2, 4];
    public UILabel Yellow;
    public UILabel Blue;
    public UILabel Red;
    public UILabel Green;

    public void AddResource(PlayerNumber PlayerNumber, Resource Resource, int Amount) {
        Resources[(int)PlayerNumber, (int)Resource] += Amount;
    }

    public void SubtractResource(PlayerNumber PlayerNumber, Resource Resource, int Amount) {
        Resources[(int)PlayerNumber, (int)Resource] -= Amount;
    }

    public void SetResource(PlayerNumber PlayerNumber, Resource Resource, int Amount) {
        Resources[(int)PlayerNumber, (int)Resource] = Amount;
    }

    public int GetResource(PlayerNumber PlayerNumber, Resource Resource) {
        return Resources[(int)PlayerNumber, (int)Resource];
    }

    void Start() {
        Resources[(int)PlayerNumber.One, (int)Resource.Yellow] = 0;
        Resources[(int)PlayerNumber.One, (int)Resource.Blue] = 0;
        Resources[(int)PlayerNumber.One, (int)Resource.Red] = 0;
        Resources[(int)PlayerNumber.One, (int)Resource.Green] = 0;
        Resources[(int)PlayerNumber.Two, (int)Resource.Yellow] = 0;
        Resources[(int)PlayerNumber.Two, (int)Resource.Blue] = 0;
        Resources[(int)PlayerNumber.Two, (int)Resource.Red] = 0;
        Resources[(int)PlayerNumber.Two, (int)Resource.Green] = 0;
    }

    void Update() {
        Yellow.text = Resources[(int)PlayerNumber.One, (int)Resource.Yellow].ToString();
        Blue.text = Resources[(int)PlayerNumber.One, (int)Resource.Blue].ToString();
        Red.text = Resources[(int)PlayerNumber.One, (int)Resource.Red].ToString();
        Green.text = Resources[(int)PlayerNumber.One, (int)Resource.Green].ToString();
    }
}
