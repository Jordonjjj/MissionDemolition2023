using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;

    [Header("Inscribed")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    [Header("Dynamic")]
    public float camZ;

    void Awake()
    {
        camZ = this.transform.position.z;
    }

    //Update is called once per frame 
    void FixedUpdate()
    {
        
        //get the position of the poi
        Vector3 destination = Vector3.zero;

        if (POI != null)
        {
            Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
            if( (poiRigid != null) && (poiRigid.IsSleeping() ) )
            {// must be sleeping, stop following it
                POI = null;
            }
        }

        if (POI != null)
        {
            destination = POI.transform.position;
        }
        //limit the minimum values of destination.x & destination.y
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;

    }
}
