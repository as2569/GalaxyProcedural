
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public const int NUM_NODES = 10; //Number of nodes
    public float xmax = 10.0f; //Max distance from origin at x axis
    public float ymax = 10.0f; //Max distance from origin at y axis
    public float min_dis = 3.0f; //Min distance between nodes
    private int next_node = 1; //Index for next avilable node

    Vector3[] nodes = new Vector3[NUM_NODES];
    public GameObject node;

    //Create a random node
    public Vector3 random_node()
    {
        //Random values within max distance
        float xpos = Random.Range(0, xmax);
        float ypos = Random.Range(0, ymax);

        //Proposed node
        Vector3 next_node;
        next_node = new Vector3(xpos, ypos, 0);
        return next_node;
    }

    //Check if given vector3 overlaps any other node
    public void check_overlap()
    {
        Vector3 randy = random_node();
        for(int i = 0; i < NUM_NODES; i++)
        {
            float dist = Vector3.Distance(nodes[i], randy);
            if (dist <= min_dis)
            {
                i = 0;
                randy = random_node();
            }
        }

        nodes[next_node] = randy;
        next_node += 1;
        Debug.Log("Node created");
    }
    
        //Check each existing node for overlap
    //    for (int i = 0; i < NUM_NODES; i++)
    //    {
    //        float dist = Vector3.Distance((nodes[i]), (next_node));
    //        //if proposed node is too close to another node, create new node and start iterating from index 0
    //        if (dist <= min_dis)
    //        {
    //            i = 0;
    //            xpos = Random.Range(0, xmax);
    //            ypos = Random.Range(0, ymax);
    //            next_node = new Vector3(xpos, ypos, 0);
    //            Debug.Log("Overlap");
    //        }
    //    }
    //    nodes[nodes.GetUpperBound(0)] = next_node;
    //    Debug.Log("Success. Returning valid node");
    //    return next_node;
    //}
       
  

	// Use this for initialization
	void Start ()
    {
        //foreach(Vector3 item in nodes)
        //{
        //    Debug.Log(nodes[0]);
        //}
        
        //Create first node at origin
        nodes[0] = new Vector3(0, 0, 0);
        Instantiate(node, nodes[0], Quaternion.identity);
        Debug.Log("Initial node");

        //Create a valid node 
        for (int j = 1; j < 10; j++)
        {
            check_overlap();
            Instantiate(node, nodes[j], Quaternion.identity);
        }
    }

}
