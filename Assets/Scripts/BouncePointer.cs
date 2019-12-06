using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePointer : MonoBehaviour
{
    public Camera _cam;
    RaycastHit hitinfo = new RaycastHit();

    bool isPointerPic = false;

    float _xval,_zval;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.GetComponent<BoxCollider>().enabled = true;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitinfo))
            {
                
                if (hitinfo.collider.tag == "BouncePoint")
                    isPointerPic = true;
               

                _xval = hitinfo.point.x;
                _zval = hitinfo.point.z;

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            transform.GetComponent<BoxCollider>().enabled = false;
            isPointerPic = false;
        }

        if (isPointerPic)
        {
           // Debug.Log("here");
            transform.position = new Vector3(Mathf.Clamp(_xval, -4, 4), 0.06f, Mathf.Clamp(_zval, -5, 2));

        }


    }
}
