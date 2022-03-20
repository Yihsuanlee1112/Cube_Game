using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCube : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    public bool dragEnabled = true;

    void Start()
    {

    }
    void OnMouseDown()
    {
        if (!dragEnabled) return;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //print("this " + gameObject.name + "isNOTkinematic");
        
        //print(this.gameObject.name);
        mZCoord = Camera.main.WorldToScreenPoint(
            gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if (!dragEnabled) return;

        transform.position = GetMouseAsWorldPoint() + mOffset;
    }
}

