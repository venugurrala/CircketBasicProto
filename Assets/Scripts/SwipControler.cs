using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SwipControler : MonoBehaviour, IBeginDragHandler, IDragHandler {

    public static SwipControler instance;

    public float minDrag; 

    private Vector2 startTuchPos; 
    private Vector2 newTuchPos;
    void Awake()
    {
        instance = this;
        Input.multiTouchEnabled = false; 
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startTuchPos = eventData.position; 
    }

    public void OnDrag(PointerEventData eventData)
    {

        newTuchPos = eventData.position;
        
        if (  Vector2.Distance(newTuchPos, startTuchPos) >= minDrag)
        {
            
            Vector2 dragDirection = newTuchPos - startTuchPos;
            float dragAngle = Mathf.Atan2(dragDirection.y, dragDirection.x) * Mathf.Rad2Deg;

           // print(dragAngle+"venu 1");
            dragAngle += 90;
           // Debug.LogError(dragAngle+" venu 2");
            dragAngle *= -1;

           // Debug.LogError(dragAngle + " venu 3");

            BatMovement._inst.EnabelBatCollider();


            BatMovement._inst.BatSwipDirection(dragAngle);
        }
    }
}
