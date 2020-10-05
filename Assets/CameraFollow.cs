using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;

    public float lerpSpeed;

    public Vector3 offset = new Vector3(0,10,13);
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 lerpy = Vector3.Lerp(transform.position, target.position, lerpSpeed);

        //transform.position = Vector3.Lerp(transform.position, target.position+target_Offset, 0.1f);

        Vector3 lerpy  = Vector3.Lerp(transform.position, target.position + offset, lerpSpeed);


        transform.position = new Vector3(lerpy.x,lerpy.y, target.position.z+offset.z);
    }
}
