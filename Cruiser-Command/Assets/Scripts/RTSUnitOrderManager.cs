/*
 * Name: RTS Unit Order
 * Author: James 'Sevion' Nhan
 * Date: 04/07/2013
 * Version: 1.0.0.1
 * Description:
 *		This is a simple RTS unit order system that
 *		handles orders on mouse clicks.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RTSUnitOrderManager : MonoBehaviour {
    public LayerMask Ground;
    private RTSUnitSelectionManager UnitManager;

    void Start() {
        // Get our Singleton Unit Manager
        UnitManager = GameObject.FindWithTag("UnitManager").GetComponent<RTSUnitSelectionManager>();
    }

    void Update() {
        // On Right Click
        if (Input.GetMouseButtonUp(1)) {
            // Raycast from the Camera.main to the ground to find the mouse click point on the ground
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit info;
            Physics.Raycast(ray, out info, Mathf.Infinity, Ground);

            Debug.Log(info.point);

            // For every unit that is selected, issue the order to move to that position
            foreach (GameObject unit in UnitManager.GetSelectedObjects()) {
                RTSUnitOrder.OrderStruct order = new RTSUnitOrder.OrderStruct(RTSUnitOrder.Order.Stop, new Vector3(0, 0, 0));
                if (unit.GetComponent<RTSUnitMovement>() != null) {
                    order = new RTSUnitOrder.OrderStruct(RTSUnitOrder.Order.Move, info.point);
                } else if (unit.GetComponent<RTSShipMovement>() != null) {
                    order = new RTSUnitOrder.OrderStruct(RTSUnitOrder.Order.Face, info.point);
                }
                unit.SendMessage("IssueOrder", order);
            }
        }
    }
}
