/*
 * Name: RTS Unit Movement
 * Author: Erik 'Siretu'
 * Date: 21/07/2013
 * Version: 1.0.0.0
 * Description:
 *		This is a movement system that handles moving
 *		normal units inside the battlecruiser. If the unit
 *		has a parent or if the parent variable is set, it will
 *		move "inside" that parent with relative position
 *		and rotation.
 */

using UnityEngine;
using System.Collections;

using Pathfinding;

public class RTSUnitMovement : MonoBehaviour {
	
	// Will default to the gameObject's direct parent if not set.
	public GameObject Parent;
	
	private RTSUnitOrder OrderManager;

	
    private RTSUnitOrder.Order CurrentOrder = RTSUnitOrder.Order.Stop;
    private GameObject TargetObject;
    private Vector3 TargetPosition;
	private Vector3 OriginalParentPos;
	private Vector3 OriginalRelativeTarget;
	private Quaternion OriginalParentRot;
    private const float MOVESPEED = 10.0f;
    
    private Seeker seeker;
    private CharacterController controller;
 
    //The calculated path
    public Path path;
 
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;
	
	// Use this for initialization
	void Start () {
		OrderManager = GetComponent<RTSUnitOrder>();
		seeker = GetComponent<Seeker>();
		if(Parent == null && gameObject.transform.parent != null){
			Parent = gameObject.transform.parent.gameObject;
	    }
	}
	
	// Update is called once per frame
	void Update () {
        if (OrderManager.GetCurrentOrder() == RTSUnitOrder.Order.Move) {
			
			if (path == null) {
            	//We have no path to move after yet
            	return;
        	}
			
			if(currentWaypoint >= path.vectorPath.Count){
				path = null;
				return;
			}
			
			Vector3 currPath = path.vectorPath[currentWaypoint];	
			
			// Make sure the target doesn't cause the unit to "sink" in to the ground.
			currPath.y = gameObject.transform.localPosition.y;
			
			if(Parent != null){
				// Transform the vector to local vector.
				currPath -= OriginalParentPos;
				
				// Inverse the rotation back to normal.
				currPath = Quaternion.Inverse(OriginalParentRot) * currPath;

			}
            // This is the vector towards the new position with respect to time.
            Vector3 NewPosition = (currPath - gameObject.transform.localPosition).normalized * MOVESPEED * Time.deltaTime;
			gameObject.transform.localPosition += NewPosition;
			
			// Go on to the next waypoint.
            if (Vector3.Distance(gameObject.transform.localPosition, currPath) <= 2) {
				if(currentWaypoint == path.vectorPath.Count - 1){
					
					// Clamp when it gets close enough.
					if(Vector3.Distance(gameObject.transform.localPosition, currPath) <= MOVESPEED * Time.deltaTime){
						gameObject.transform.localPosition = currPath;
						currentWaypoint++;
					}
				} else {
					currentWaypoint++;
				}
            }
        }	
	}

	public void OnPathComplete(Path p){
        if (!p.error) {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }		
	}	
	
	public void Move(Vector3 pos){
		seeker.StartPath (transform.position,pos,OnPathComplete);
        if(Parent != null){
			OriginalParentPos = Parent.transform.position;
			OriginalParentRot = Parent.transform.rotation;
	    }		
	}
}
