using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer
{
    public List<Neuron> neuronsList;
    Vector2 layerLocation;
    Vector2 layerSize;
    public double Bias;

    public int connectionsInThisLayer;

    public Layer(float x, float y, float width, float height)
    {
        layerLocation = new Vector2(x, y);
        layerSize = new Vector2(width, height);

       
        neuronsList = new List<Neuron>();
        Bias = Random.Range(0f, 10.0f);


    }

    public void createNeurons(int numberNeurons)
    {

        float lenghtEachNeuron = (layerSize.y / numberNeurons);
        for (int i = 0; i < numberNeurons; i++)
        {
            Neuron temp = new Neuron(layerLocation.x, (lenghtEachNeuron * (i+1)) - (lenghtEachNeuron / 2) );
            
            neuronsList.Add(temp);
        }
    }

    


    public List<double> getConnectionsWeights()
    {
        List<double> connectionWeights= new List<double>();
        for (int j = 0; j < neuronsList.Count; j++)
        {


            
            for (int k = 0; k < neuronsList[j].connections.Count; k++)
            {
                connectionWeights.Add(neuronsList[j].connections[k].weight);
            }
        }

        connectionWeights.Add(Bias);
        return connectionWeights;
    }


}
