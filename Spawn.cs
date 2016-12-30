
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public const int NUM_NODES = 10; //Number of nodes
    public float xmax = 20.0f; //Max distance from origin at x axis
    public float ymax = 20.0f; //Max distance from origin at y axis
    public float min_dis = 3.0f; //Min distance between nodes
    private int next_node = 0; //Index for next avilable node

    Vector3[] nodes = new Vector3[NUM_NODES];
    LineRenderer[] edges = new LineRenderer[NUM_NODES * 4];
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
    public void create_edge(int i)
    {
        //LineRenderer edge = new LineRenderer();
        edges[i] = gameObject.AddComponent<LineRenderer>();
        Vector3 point1 = new Vector3();
        Vector3 point2 = new Vector3();
        
        point1 = nodes[0];
        point2 = nodes[1];

        edges[i].SetPosition(0, point1);
        edges[i].SetPosition(1, point2);
        Instantiate(edges[i]);
    }

    //Check if randomly created Vector3 overlaps any other node
    public void check_overlap()
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
        nodes[next_node] = randy;
        next_node += 1;
        Debug.Log("Node created");
        return;
    }
    

	// Use this for initialization
	void Start ()
    {

        //Create a valid node 
        for (int j = 0; j < 3; j++)
        {
            check_overlap();
            Instantiate(node, nodes[j], Quaternion.identity);
            Debug.Log("Node created");
            create_edge(j);
        }
        //create_edge();
    }

}
