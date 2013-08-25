/*
 * Name: Collider update
 * Author: Erik 'Siretu'
 * Date: 21/07/2013
 * Version: 1.0.0.0
 * Description:
 * 		This is a temporary script to update the A* grid
 * 		graph with collider's new position. This should
 * 		only be used on moving obstacles and it's bad
 * 		and should be remade in the future.
 */

using UnityEngine;
using System.Collections;

public class ColliderUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		AstarPath.active.UpdateGraphs(gameObject.collider.bounds);
	}
}
