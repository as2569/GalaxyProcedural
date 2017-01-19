
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector3 position;
    public GameObject gameObject;
}

[System.Serializable]
public class Edge
{
    public Node A;
    public Node B;
    public GameObject gameObject;
}

public class Spawn : MonoBehaviour
{
    public int numNodes = 16; //Number of nodes
    public float xmax = 20.0f; //Max distance from origin at X axis
    public float ymax = 20.0f; //Max distance from origin at Y axis
    public float min_dis = 3.0f; //Min distance between nodes
    //private int next_node_index = 1; //Index for next available node
    //private int next_edge_index = 1; //Index for next edge

    List<Node> nodes = new List<Node>();
    List<Edge> edges = new List<Edge>();
    public GameObject nodePrefab;
    public GameObject edgePrefab;

    //Create a random node
    public Vector3 RandomNode()
    {
        //Random values within max distance
        float xpos = Random.Range(0, xmax);
        float ypos = Random.Range(0, ymax);

        //Proposed node
        Vector3 next_node;
        next_node = new Vector3(xpos, ypos, 0);
        foreach(Node n in nodes)
        {
            float dist = Vector3.Distance(n.position, next_node);
            if (dist <= min_dis)
            {
                //if overlap, generate new node and iterate from beginning
                return Vector3.zero;
            }
        }
        return next_node;
    }

    

    public void GenerateEdges()
    {
        for (int i = 1; i < nodes.Count; i++)
        {
            AddEdge(nodes[i - 1], nodes[i]);
        }
    }

    //Add an gameobject with linerenderer to array
    //DOES NOT check for collision 
    public void AddEdge(Node A, Node B)
    {
        Edge e = new Edge();
        e.A = A;
        e.B = B;
        e.gameObject = Instantiate<GameObject>(edgePrefab);
        LineRenderer r = e.gameObject.GetComponent<LineRenderer>();
        r.SetPosition(0, A.position);
        r.SetPosition(1, B.position);
        //Debug.Log("Edge added at index " + next_edge_index);
        edges.Add(e);
        return;
    }

    //Add a new node to nodes array
    //Check for overlap 
    public void AddNode()
    {
        Vector3 randy;
        
        while((randy = RandomNode()) == Vector3.zero);

        //add node to array
        GameObject nObj = Instantiate<GameObject>(nodePrefab);
        Node n = new Node();
        nObj.transform.position = randy;
        n.position = randy;
        n.gameObject = nObj;
        nodes.Add(n);
        Debug.Log("Node added at index" + (nodes.Count -1));
        return;
    }
    
	// Use this for initialization
	void Start ()
    {
        AddNode();
        
        //Create a valid node 
        //TEST
        for (int j = 0; j <= numNodes; j++)
        {
            AddNode();
            Debug.Log("Node instantiated");
            //Instantiate(edges[j]);
            //Debug.Log("Edge instantiated");
        }
        GenerateEdges();
    }

}
