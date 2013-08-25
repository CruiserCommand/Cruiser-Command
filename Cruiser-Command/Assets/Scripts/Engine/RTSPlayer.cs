/*
 * Name: RTS Player
 * Author: James 'Sevion' Nhan and Erik 'Siretu' Ihren
 * Date: 17/07/2013
 * Version: 1.0.1.0
 * Description:
 * 		This is a simple script that handles players
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerNumber {
    One, Two, Three, Four, Five, Six, Seven, Eight
};

public class RTSPlayer : MonoBehaviour {
    private static int NumPlayers = 0;
    private static List<RTSPlayer> Players = new List<RTSPlayer>();
    private int Number = 0;

    void Start() {
        // In the multiplayer we will have to update the list and player numbers upon joining a game
        Number = NumPlayers++;
        Players.Add(this);
    }

    public void AddResource(int Amount, Resource Type) {
        RTSResources.Resources[Number, (int)Type] += Amount;
    }

    public void SubtractResource(int Amount, Resource Type) {
        RTSResources.Resources[Number, (int)Type] -= Amount;
    }

    public void SetResource(int Amount, Resource Type) {
        RTSResources.Resources[Number, (int)Type] = Amount;
    }

    public int GetResource(Resource Type) {
        return RTSResources.Resources[Number, (int)Type];
    }

    public static RTSPlayer GetPlayer(PlayerNumber number) {
        foreach (RTSPlayer player in Players) {
            if ((int)number == player.Number) {
                return player;
            }
        }

        return null;
    }

    public static RTSPlayer GetPlayer(int number) {
        foreach (RTSPlayer player in Players) {
            if (number == player.Number) {
                return player;
            }
        }

        return null;
    }
}
