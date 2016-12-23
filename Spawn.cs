using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public const int NUM_NODES = 10; //Number of nodes
    public float xmax = 10.0f; //Max distance from origin at x axis
    public float ymax = 10.0f; //Max distance from origin at y axis
    public float min_dis = 1.0f; //Min distance between nodes
    Vector3[] nodes = new Vector3[NUM_NODES];
    //ArrayList edges = new ArrayList();
    public GameObject planet;

    //Create a random node after checking it won't overlap any other nodes
    public Vector3 random_node()
    {
        //Random values within max distance
        float xpos = Random.Range(0, xmax);
        float ypos = Random.Range(0, ymax);

        //Proposed node
        Vector3 next_node;
        next_node = new Vector3(xpos, ypos, 0);

        //Check each existing node for overlap
        for(int i = 0; i < NUM_NODES; i++)
        {
            float dist = Vector3.Distance((nodes[i]), (next_node)); 
            if(dist <= min_dis)
            {
                i = 0;
                xpos = Random.Range(0, xmax);
                ypos = Random.Range(0, ymax);
                next_node = new Vector3(xpos, ypos, 0);
                Debug.Log("Overlap");
            }
        }
        Debug.Log("Success. Returning valid node");
        return next_node;
    }

	// Use this for initialization
	void Start ()
    {
        //Create first node at origin
        nodes[0] = new Vector3(0, 0, 0);
        Instantiate(planet, nodes[0], Quaternion.identity);
        Debug.Log("Initial node");

        //Create a valid node 
        for (int j = 0; j < 10; j++)
        {
            random_node();
            Instantiate(planet, random_node(), Quaternion.identity);
        }
    }

}
