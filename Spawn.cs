using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public int num_nodes = 10; //Number of nodes
    public float xmax = 10.0f; //Max distance from origin at x axis
    public float ymax = 10.0f; //Max distance from origin at y axis
    public float min_dis = 1.0f; //Min distance between nodes
    ArrayList nodes = new ArrayList();
    ArrayList edges = new ArrayList();
    public GameObject node;

    //Propose a node at random positions and make sure it will not overlap other nodes
    public bool random_node()
    {
        //Random values within max distance
        float xpos = Random.Range(0, xmax);
        float ypos = Random.Range(0, ymax);

        //Proposed node
        Vector3 proposed_node = new Vector3(xpos, ypos, 0);

        //Check each existing node for overlap
        foreach(Vector3 node in nodes)
        {
            
            float dist = Vector3.Distance((node), (proposed_node)); 

            if(dist < min_dis)
            {
                return false;
            }
            else
            {
                nodes.Add(proposed_node);
                return true;
            }
        }
        return false;
    }

	// Use this for initialization
	void Start ()
    {
        //Create first node at origin
        //nodes[0] = new Vector3(0,0,0);
        nodes.Add(new Vector3(0, 0, 0));

        //Create an array of valid nodes 
        for (int i = 1; i <= num_nodes; i++)
        {
            if(!random_node())
            {
                i--;
            }
            
            Debug.Log("Creating node number " + i);
        }

        for (int i = 0; i <= num_nodes; i++)
        {
            Instantiate(node, nodes[i]);
        }
	}

}
