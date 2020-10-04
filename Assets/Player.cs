﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PortalTraveller
{

    public float forwardspeed = 5f;
    public float sideSpeed = 2f;
    public float jumporce = 200f;

    public float dropsTime=0.2f;

    public bool alive = true;

    Rigidbody rb;

    public GameObject droppedPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(TrailObjs());
    }

    // Update is called once per frame
    void Update()
    {

        float side = Input.GetAxisRaw("Horizontal");

        side = side * sideSpeed;



        rb.velocity = new Vector3(side, rb.velocity.y , forwardspeed);


        if (transform.position.y < -5)
        {
            GameManager.instance.Death();
        }
    }

    IEnumerator TrailObjs()
    {

        while(alive)
        {
            yield return new WaitForSeconds(dropsTime);
            Instantiate(droppedPrefab, transform.position, transform.rotation);
        }
    }


    void OnCollisionEnter (Collision col)
    {
        Debug.Log(col.contacts[0].normal);

        //if its frontally
        if (col.contacts[0].normal.z < -0.8f)
        {
            GameManager.instance.Death();
        }
    }
}
