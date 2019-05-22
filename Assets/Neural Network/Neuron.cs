using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron 
{

    public Vector2 diagramLocation;
    public List<Connection> connections;
    public double value;

    public Neuron(float x,float y)
    {
        diagramLocation = new Vector2(x, y);

        //neuronsList = new List<Neuron>();
        connections = new List<Connection>();
        value = Random.Range(0.0f, 1.0f);
    }

    public void addConnection(Connection c)
    {

        connections.Add(c);
    }
}
