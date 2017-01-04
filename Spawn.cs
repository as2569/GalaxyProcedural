
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public const int NUM_NODES = 16; //Number of nodes
    public float xmax = 20.0f; //Max distance from origin at x axis
    public float ymax = 20.0f; //Max distance from origin at y axis
    public float min_dis = 3.0f; //Min distance between nodes
    private int next_node_index = 1; //Index for next avilable node
    private int next_edge_index = 1; //Index for next edge

    Vector3[] nodes = new Vector3[NUM_NODES];
    GameObject[] edges = new GameObject[NUM_NODES];
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

    //Create a random edge between 2 nodes
    public GameObject random_edge()
    {
        GameObject new_edge = new GameObject();
        LineRenderer line_rend = new_edge.AddComponent<LineRenderer>();

        //lineRenderers vertices, beginning and end of line
        Vector3 point1 = new Vector3();
        Vector3 point2 = new Vector3();
        point1 = nodes[next_node_index - 1];
        point2 = nodes[next_node_index];

        line_rend.SetPosition(0, point1);
        line_rend.SetPosition(1, point2);
        return new_edge;  
    }

    public void add_edge()
    {
        edges[next_node_index] = random_edge();
        next_edge_index += 1;
        Debug.Log("Edge created");
        return;
    }

    //Add a new node to nodes array
    //Check for overlap 
    public void add_node()
    {
        Vector3 randy = random_node();
        //iterate over all existing nodes
        for(int i = 0; i < NUM_NODES; i++)
        {
            float dist = Vector3.Distance(nodes[i], randy);
            if (dist <= min_dis)
            {
                //if overlap, generate new node and iterate from beginning
                i = 0;
                randy = random_node();
            }
        }
        //add node to array
        nodes[next_node_index] = randy;
        next_node_index += 1;
        Debug.Log("Node created");
        return;
    }
    

	// Use this for initialization
	void Start ()
    {
        //temp test node
        nodes[13] = new Vector3(5, 5, 0);
        Instantiate(node, nodes[13], Quaternion.identity);
        Debug.Log("t1");

        nodes[14] = new Vector3(10, 10, 0);
        Instantiate(node, nodes[14], Quaternion.identity);
        Debug.Log("t2");

        nodes[15] = new Vector3(15, 15, 0);
        Instantiate(node, nodes[15], Quaternion.identity);
        Debug.Log("t3");


        //Create node at index 0
        nodes[0] = new Vector3(0, 0, 0);
        Instantiate(node, nodes[0], Quaternion.identity);
        Debug.Log("Initial node");

        //Create a valid node 
        for (int j = 0; j < 5; j++)
        {
            add_node();
            add_edge();
            Instantiate(node, nodes[j], Quaternion.identity);
            Debug.Log("Node instantiated");
            Instantiate(edges[j]);
            Debug.Log("Edge instantiated");
        }
    }

}
