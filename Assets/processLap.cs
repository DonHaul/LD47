using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class processLap : MonoBehaviour
{
    public GameObject meshTrailfab;

    Mesh mesh;

    public Material[] mats;

    public float sideSize = 0.4f;

    
    int triIndex;


    // Start is called before the first frame update
    Mesh GenerateMesh()
    {
        mesh = new Mesh();


        int[] triangles;
        Vector3[] vertices;

        //one corner of square per position
        vertices = new Vector3[GameManager.instance.LapPositions.Count * 4];

        //triangles = new int[(positions.Length - 1) *8];

        //all slices * Triangles per slice(8) * vertices per triangle (3)  + (2 triangles in beginnig)6

        Debug.Log(GameManager.instance.LapPositions.Count - 1);
        triangles = new int[(GameManager.instance.LapPositions.Count - 1) * 8 * 3 + 6];

        //create vertices
        for (int i = 0; i < GameManager.instance.LapPositions.Count; i++)
        {
            //Debug.Log(i);
            vertices[i * 4 + 0] = GameManager.instance.LapPositions[i] + new Vector3(sideSize, sideSize);
            vertices[i * 4 + 1] = GameManager.instance.LapPositions[i] + new Vector3(sideSize, -sideSize);
            vertices[i * 4 + 2] = GameManager.instance.LapPositions[i] + new Vector3(-sideSize, -sideSize);
            vertices[i * 4 + 3] = GameManager.instance.LapPositions[i] + new Vector3(-sideSize, sideSize);
        }



        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        //create right
        for (int i = 0; i < GameManager.instance.LapPositions.Count - 1; i++)
        {

            int vertIndex = i * 4;

            //2x3 corrreposds to front face offset
            int triindex = 2 * 3 + i * 8 * 3;

            //right
            triangles[triindex] = vertIndex;
            triangles[triindex + 1] = vertIndex + 3;
            triangles[triindex + 2] = vertIndex + 4;

            triangles[triindex + 3] = vertIndex + 4;
            triangles[triindex + 4] = vertIndex + 3;
            triangles[triindex + 5] = vertIndex + 7;

            //up            
            triangles[triindex + 6] = vertIndex + 3;
            triangles[triindex + 7] = vertIndex + 2;
            triangles[triindex + 8] = vertIndex + 6;

            triangles[triindex + 9] = vertIndex + 3;
            triangles[triindex + 10] = vertIndex + 6;
            triangles[triindex + 11] = vertIndex + 7;


            //left         
            triangles[triindex + 12] = vertIndex + 1;
            triangles[triindex + 13] = vertIndex + 5;
            triangles[triindex + 14] = vertIndex + 6;

            triangles[triindex + 15] = vertIndex + 1;
            triangles[triindex + 16] = vertIndex + 6;
            triangles[triindex + 17] = vertIndex + 2;


            //down         
            triangles[triindex + 18] = vertIndex + 4;
            triangles[triindex + 19] = vertIndex + 5;
            triangles[triindex + 20] = vertIndex + 1;

            triangles[triindex + 21] = vertIndex + 4;
            triangles[triindex + 22] = vertIndex + 1;
            triangles[triindex + 23] = vertIndex + 0;



        }



        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;


        return mesh;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;


        AudioManager.instance.PlaySound("finish");

        GameObject meshGo = Instantiate(meshTrailfab,Vector3.zero,Quaternion.identity);

        mesh = GenerateMesh();
        meshGo.GetComponent<MeshRenderer>().sharedMaterial = mats[Random.Range(0,mats.Length)];
        meshGo.GetComponent<MeshFilter>().mesh = mesh;
        meshGo.GetComponent<MeshCollider>().sharedMesh = mesh;

        Player.instance.forwardspeed += 2f;

        Debug.Log("Clear");


        GameManager.instance.Lap();

       

        UImanager.instance.SetLaps(GameManager.instance.lapCount);
    }
}
