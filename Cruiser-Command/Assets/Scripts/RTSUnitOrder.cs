/*
 * Name: RTS Unit Order
 * Author: James 'Sevion' Nhan and Erik 'Siretu' Ihrén
 * Date: 04/07/2013
 * Version: 1.0.0.1
 * Description:
 *    	This is a simple RTS unit order system that
 *		handles orders on mouse clicks. It automatically
 *      does relative movement if a parent is specified.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RTSUnitOrder : MonoBehaviour {
	
    // The possible orders
    public enum Order {
        Move,
        Stop,
        // Still need to implement:
        HoldPosition,
        Patrol,
        Attack,
        Smart
    };

    // OrderStruct for issuing orders
    public struct OrderStruct {
        public Order order;
        public Vector3 target;

        public OrderStruct(Order order, Vector3 target) {
            this.order = order;
            this.target = target;
        }
    };
	
	// Will default to the gameObject's direct parent if not set.
	public GameObject Parent;

    private Order CurrentOrder = Order.Stop;
    private GameObject TargetObject;
    private Vector3 TargetPosition;
    private const float MOVESPEED = 5.0f;
	
	void Start() {
		if(Parent == null && gameObject.transform.parent != null){
			Parent = gameObject.transform.parent.gameObject;
		}
	}

    void Update() {
        if (CurrentOrder == Order.Move) {
			Vector3 relativeTargetPosition = TargetPosition;
			if(Parent != null){
				relativeTargetPosition += gameObject.transform.parent.transform.position;
			}
			
            // This is the new position with respect to time
            Vector3 NewPosition = (relativeTargetPosition - gameObject.transform.position).normalized * MOVESPEED * Time.deltaTime;
            // Clamping
            if (Vector3.Distance(gameObject.transform.position, relativeTargetPosition) > MOVESPEED * Time.deltaTime) {
                gameObject.transform.position += NewPosition;
            } else {
                CurrentOrder = Order.Stop;
                gameObject.transform.position = relativeTargetPosition;
            }
        }
    }

    // Issue an order to the unit
    public void IssueOrder(OrderStruct order) {
        // If it's either of the non-motion orders, there shouldn't be a target
        if (order.order == Order.Stop || order.order == Order.HoldPosition) {
            CurrentOrder = order.order;
            TargetPosition = gameObject.transform.position;
            TargetObject = gameObject;
        } else {
            CurrentOrder = order.order;
            // Make sure the target doesn't cause the unit to "sink" in to the ground
			order.target += new Vector3(0, gameObject.transform.position.y, 0);
            TargetPosition = order.target;
            if (gameObject.transform.parent != null) {
                TargetPosition -= gameObject.transform.parent.transform.position;
            }
        }
    }

    public Order GetCurrentOrder() {
        return CurrentOrder;
    }

    public Vector3 GetOrderPosition() {
        return TargetPosition;
    }

    public GameObject GetOrderTarget() {
        return TargetObject;
    }
}
