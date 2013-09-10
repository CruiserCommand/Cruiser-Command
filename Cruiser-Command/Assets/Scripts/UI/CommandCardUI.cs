/*
 * Name: Command Card UI
 * Author: James 'Sevion' Nhan
 * Date: 07/09/2013
 * Version: 1.0.0.0
 * Description:
 * 		Controls the command card UI
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandCardUI : MonoBehaviour {
    public Transform ButtonPrefab, BackgroundPrefab;
	public int Width, Height, Rows, Columns;
	public List<GameObject> CommandCardButtons = new List<GameObject>();

	void Awake() {
		// Create the background and add it to the panel
		GameObject go = NGUITools.AddChild(gameObject, BackgroundPrefab.gameObject);
		go.transform.localPosition = new Vector3(-Width / 2, Height / 2);
		go.transform.localScale = new Vector3(Width, Height);
		// For each icon, create it and add it to the panel.
		for (int i = 1; i <= Columns; ++i) {
			for (int j = 1; j <= Rows; ++j) {
				go = NGUITools.AddChild(gameObject, ButtonPrefab.gameObject);
				go.transform.localPosition = new Vector3(-(Width / Columns) * i + (Width / Columns) / 2, (Height / Rows) * j - (Height / Rows) / 2, -0.1f);
				foreach (Transform child in go.transform) {
					child.transform.localScale = new Vector3(Width / Columns - 5, Height / Rows - 5);
				}
				// Refresh the collider
				NGUITools.AddWidgetCollider(go);
				// Add the button the list of buttons
				CommandCardButtons.Add(go);
			}
		}
	}
}