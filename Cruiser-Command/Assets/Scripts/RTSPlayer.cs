/*
 * Name: RTS Player
 * Author: James 'Sevion' Nhan and Erik 'Siretu' Ihren
 * Date: 17/07/2013
 * Version: 1.0.0.1
 * Description:
 * 		This is a simple RTS movement script that handles
 * 		players.
 */

using UnityEngine;
using System.Collections;

public class RTSPlayer : MonoBehaviour {
    public enum Player {
        One, Two, Three, Four, Five, Six, Seven, Eight
    };

    public const int NUM_PLAYERS = 2;
}
