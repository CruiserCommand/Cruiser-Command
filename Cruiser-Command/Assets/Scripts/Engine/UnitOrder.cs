/*
 * Name: RTS Unit Order
 * Author: James 'Sevion' Nhan and Erik 'Siretu'
 * Date: 21/07/2013
 * Version: 1.0.0.3
 * Description:
 *		This is a simple RTS unit order system that
 *		handles orders on mouse clicks.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using CC.eLog;
using Pathfinding;

public class UnitOrder : uLink.MonoBehaviour {

    // The possible orders
    public enum Order {
        Move,
        Stop,
        Face,
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

    private Order CurrentOrder = Order.Stop;
    private GameObject TargetObject;
    private Vector3 TargetPosition;
	private UnitMovement MovementManager;
    private GameObject networkManager;
    private bool connectToServer = true;
    
	void Start() {
        MovementManager = GetComponent<UnitMovement>();

        networkManager = GameObject.FindWithTag("NetworkManager");

        if (networkManager != null) {
            connectToServer = networkManager.GetComponent<InitiateClient>().connectToServer;
        }
	}

    // Issue an order to the unit
    public void IssueOrder(OrderStruct order) {
        if (uLink.NetworkView.Get(gameObject).isOwner || !connectToServer) {
            // If it's either of the non-motion orders, there shouldn't be a target
            if (order.order == Order.Stop || order.order == Order.HoldPosition) {
                CurrentOrder = order.order;
                TargetPosition = gameObject.transform.position;
                TargetObject = gameObject;
            } else if (order.order == Order.Face) {
                gameObject.transform.LookAt(order.target + new Vector3(0, gameObject.transform.position.y - order.target.y, 0));
            } else {
                gameObject.transform.LookAt(order.target + new Vector3(0, gameObject.transform.position.y - order.target.y, 0));
                CurrentOrder = order.order;
                TargetPosition = order.target;
                GameObject console = gameObject.GetComponent<Unit>().console;
                if (console != null) {
                    console.GetComponent<ConsoleControls>().DisconnectConsole();
                }
                if (connectToServer) {
                    networkView.RPC("S_OrderMove", uLink.RPCMode.Server, TargetPosition);
                } else {
                    this.C_OrderMove(TargetPosition);
                }
            }
        }
    }

    [RPC]
    public void S_OrderMove(Vector3 pos, uLink.NetworkMessageInfo info) {
        if (info.sender == uLink.NetworkView.Get(gameObject).owner) {
            MovementManager.Move(pos);
            networkView.RPC("C_OrderMove", uLink.RPCMode.Others, pos);
        }
    }

    [RPC]
    public void C_OrderMove(Vector3 pos) {
        MovementManager.Move(pos);
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