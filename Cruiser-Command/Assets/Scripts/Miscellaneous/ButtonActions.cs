/*
 * Name: RTS Resources
 * Author: James 'Sevion' Nhan and Erik 'Siretu'
 * Date: 05/08/2013
 * Version: 1.0.0.0
 * Description:
 * 		This is a simple function script that handles
 * 		button actions.
 */

using UnityEngine;
using System.Collections;

public class ButtonActions : MonoBehaviour {
    public void ConnectBtn() {
        Debug.Log("Connect to a server!");
    }

    public void StartServerBtn() {
        Debug.Log("Start a server!");
    }

    public void QuitBtn() {
        Debug.Log("I quit!");
        Application.Quit();
    }
}
