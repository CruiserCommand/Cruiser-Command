/*
 * Name: Resources UI
 * Author: James 'Sevion' Nhan
 * Date: 08/09/2013
 * Version: 1.0.0.0
 * Description:
 * 		Controls the resources UI
 */

using UnityEngine;
using System.Collections;

public class ResourcesUI : MonoBehaviour {
	public Transform ResourceIconPrefab;
	public Transform ResourceLabelPrefab;
	public Transform BackgroundPrefab;
	public int Width;
	public int Height;
	public int SpriteSize;

	void SetResourceLabel(int i, UILabel label) {
		switch (i) {
			case 2:
				gameObject.GetComponent<ResourceManager>().Yellow = label;
				break;
			case 4:
				gameObject.GetComponent<ResourceManager>().Blue = label;
				break;
			case 6:
				gameObject.GetComponent<ResourceManager>().Red = label;
				break;
			case 8:
				gameObject.GetComponent<ResourceManager>().Green = label;
				break;
		}
	}

	void SetResourceColor(int i, UISprite sprite) {
		switch (i) {
			case 1:
				sprite.color = Color.yellow;
				break;
			case 3:
				sprite.color = Color.blue;
				break;
			case 5:
				sprite.color = Color.red;
				break;
			case 7:
				sprite.color = Color.green;
				break;
		}
	}

	void Awake () {
		GameObject go = NGUITools.AddChild(gameObject, BackgroundPrefab.gameObject);
		go.transform.localPosition = new Vector3(-Width / 2, -Height / 2);
		go.transform.localScale = new Vector3(Width, Height);
		go = NGUITools.AddChild(gameObject, ResourceLabelPrefab.gameObject);
		for (int i = 1; i <= 8; ++i) {
			if (i % 2 == 0) {
				go = NGUITools.AddChild(gameObject, ResourceLabelPrefab.gameObject);
				go.transform.localPosition = new Vector3((Width * (i-9)) / 9, -Height / 2, 0.01f);
				go.transform.localScale = new Vector3(18, 18);
				SetResourceLabel(i, go.GetComponent<UILabel>());
			} else {
				go = NGUITools.AddChild(gameObject, ResourceIconPrefab.gameObject);
				go.transform.localPosition = new Vector3((Width * (i-9)) / 9, -Height / 2, 0.01f);
				go.transform.localScale = new Vector3(SpriteSize, SpriteSize);
				SetResourceColor(i, go.GetComponent<UISprite>());
			}
		}
	}
}