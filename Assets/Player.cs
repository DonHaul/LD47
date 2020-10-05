using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : PortalTraveller
{

    public static Player instance;

    public float forwardspeed = 5f;
    public float sideSpeed = 2f;
    public float jumpforce = 200f;

    public float dropsTime=0.2f;

    public bool alive = true;

    public float offset;
    public Vector3 groundColSize;

    Rigidbody rb;

    public GameObject droppedPrefab;

    public bool grounded = false;

    public LayerMask mask;

    // Start is called before the first frame update

    bool prevgrounded;



    private void Awake()
    {
        instance = this;

    }

    void Start()
    {


        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }

        if (GameManager.instance.gameState == 1)
        {
            float side = Input.GetAxisRaw("Horizontal");

        side = side * sideSpeed;

        //Debug.Log(rb.velocity);

        

        
        grounded = Physics.CheckBox(transform.position + Vector3.down * offset, groundColSize * 0.5f,Quaternion.identity,mask);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpforce);
            AudionManager.instance.PlaySound("jump");
        }

        if(grounded == true  && prevgrounded==false)
        {
            AudionManager.instance.PlaySound("land");
        }




            rb.velocity = new Vector3(side, rb.velocity.y , forwardspeed);



            if (transform.position.y < -5 || transform.position.z > 213)
        {
            GameManager.instance.Death();
        }

        if(transform.position.z>203 && transform.position.z<211)
            {
                Debug.Log("Wonky Reset");
                transform.position = Vector3.Scale(transform.position, new Vector3(1, 1, 0))+Vector3.forward*-10f;
            }

        prevgrounded = grounded;
        }
    }




    void OnCollisionEnter (Collision col)
    {
        //Debug.Log(col.contacts[0].normal);
        if (rb.velocity.z < forwardspeed *0.05f)
        {

            GameManager.instance.Death();

            rb.AddForce(Vector3.back * 50f);
        }
        /*//if its frontally
        if (col.contacts[0].normal.z < -0.8f)
        {
            //GameManager.instance.Death();

            rb.AddForce(Vector3.back * 50f);
        }*/
    }

    void OnCollisionStay(Collision col)
    {
        //Debug.Log(col.contacts[0].normal);
        if (rb.velocity.z < forwardspeed *0.3f)
        {
            GameManager.instance.Death();

            rb.AddForce(Vector3.back * 50f);
        }
        /*//if its frontally
        if (col.contacts[0].normal.z < -0.8f)
        {
            //GameManager.instance.Death();

            rb.AddForce(Vector3.back * 50f);
        }*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;


        Gizmos.DrawCube(transform.position + Vector3.down * offset, groundColSize);
    }
}
