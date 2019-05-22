using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class Network 
{
    //public List<Neuron> neuronsList;
    public List<Layer> layerList;

    Vector2 diagramLocation;
    Vector2 sizeDiagram;
   //public Network(float width, float height, int numberLayers)
   // {
   //     sizeDiagram = new Vector2(width, height);
   //     layerList = new List<Layer>();
   //     createLayers(numberLayers);
   //     Debug.Log("Created Network");

   // }

    public Network(float width, float height, int []neurons)
    {
        sizeDiagram = new Vector2(width, height);
        layerList = new List<Layer>();

        createNeuronalNetwork(neurons);
        Debug.Log("Created Network");
        connectNeurons();

    }

    public Network(float width, float height, int[] neurons, double[] tempList)
    {
        sizeDiagram = new Vector2(width, height);
        layerList = new List<Layer>();

        createNeuronalNetwork(neurons);
        Debug.Log("Created Network from export");
        connectNeurons(tempList);

    }

    public Network(List<double> tempList)
    {
        sizeDiagram = Vector2.zero;
        layerList = new List<Layer>();
        int[] neurons = new int[(int)tempList[0]];
        for (int i = 0; i < tempList[0]; i++)
        {
            neurons[i] = (int)tempList[i + 1];
        }
        tempList.RemoveRange(0, (((int)tempList[0]*2)+1));
        createNeuronalNetwork(neurons);
        Debug.Log("Created Network from export");
        connectNeurons(tempList);

    }

    //public Network(double[] tempList)
    //{
    //    sizeDiagram = Vector2.zero;
    //    layerList = new List<Layer>();


    //    int[] neurons = new int[(int)tempList[0]];
    //    Debug.Log(tempList[0]);
    //    for (int i = 0; i < tempList[0]; i++)
    //    {
    //        neurons[i] = (int)tempList[i + 1];
    //    }
    //    //tempList.RemoveRange(0, (((int)tempList[0] * 2) + 1));
    //   // createNeuronalNetwork(neurons);
    //    Debug.Log("Created Network from export");
    //   // connectNeurons(tempList);

    //}

    public Network(int[] neurons,double[] tempList)
    {
        sizeDiagram = Vector2.zero;
        layerList = new List<Layer>();


       
        //Debug.Log(tempList[0]);
        //for (int i = 0; i < tempList[0]; i++)
        //{
        //    neurons[i] = (int)tempList[i + 1];
        //}
        //tempList.RemoveRange(0, (((int)tempList[0] * 2) + 1));
        createNeuronalNetwork(neurons);
        //Debug.Log("Created Network from export");
        connectNeurons(tempList);

    }

    void createNeuronalNetwork(int[] neurons)
    {
        float lenghtEachLayer = (sizeDiagram.x / neurons.Length);
        for (int i = 0; i < neurons.Length; i++)
        {
            Layer temp = new Layer((lenghtEachLayer * (i + 1)) - (lenghtEachLayer / 2), sizeDiagram.y / 2, lenghtEachLayer, sizeDiagram.y);

            layerList.Add(temp);
            layerList[i].createNeurons(neurons[i]);
           // Debug.Log("Created Neurons"+ layerList[i].neuronsList.Count);
        }
        //Debug.Log("Created Layers "+layerList.Count );

    }


    public void connectNeurons()
    {
        for (int i = 0; i < layerList.Count; i++)
        {

            
            //Took a look every Neuron in every Layer to connect to next one
            if ((i+1) == layerList.Count)
            {
                Debug.Log("Next Layer is ouput");
            }
            else
            {
                for (int j = 0; j < layerList[i].neuronsList.Count; j++)
                {
                    for (int k = 0; k < layerList[(i+1)].neuronsList.Count; k++)
                    {
                        connect(layerList[i].neuronsList[j], layerList[(1 + i)].neuronsList[k]);
                        layerList[i].connectionsInThisLayer += 1;
                        Debug.Log("Connecting Neuron "+j +" To "+ k + " In Layer "+i+ " To Layer"+ (1+i)+" Number of actual Connection in this layer:"+ layerList[i].connectionsInThisLayer);
                    }
                }
            }

        }
    }

    public void connectNeurons(List<double> connectionsWeights)
    {
        int connectionNumber=0;
        for (int i = 0; i < layerList.Count; i++)
        {


            //Took a look every Neuron in every Layer to connect to next one
            if ((i + 1) == layerList.Count)
            {
                Debug.Log("Next Layer is ouput");
                //layerList[i].Bias = connectionsWeights[connectionNumber];
            }
            else
            {
                for (int j = 0; j < layerList[i].neuronsList.Count; j++)
                {
                    for (int k = 0; k < layerList[(i + 1)].neuronsList.Count; k++)
                    {
                        connect(layerList[i].neuronsList[j], layerList[(1 + i)].neuronsList[k],connectionsWeights[connectionNumber]);
                        layerList[i].connectionsInThisLayer += 1;
                        Debug.Log("Connecting Neuron " + j + " To " + k + " In Layer " + i + " To Layer" + (1 + i) + " Number of actual Connection in this layer:" + layerList[i].connectionsInThisLayer);
                        connectionNumber++;
                    }
                    
                    
                }
                layerList[i].Bias = connectionsWeights[connectionNumber];
                connectionNumber++;
            }


        }
    }
    public void updateNewWeights(double[] connectionsWeights)
    {
        int connectionNumber = 0;
        for (int i = 0; i < layerList.Count; i++)
        {


            //Took a look every Neuron in every Layer to connect to next one
            if ((i + 1) == layerList.Count)
            {
                //Debug.Log("Next Layer is ouput");
                //layerList[i].Bias = connectionsWeights[connectionNumber];
            }
            else
            {
                for (int j = 0; j < layerList[i].neuronsList.Count; j++)
                {


                    
                    for (int k = 0; k < layerList[i].neuronsList[j].connections.Count; k++)
                    {
                        layerList[i].neuronsList[j].connections[k].weight = connectionsWeights[connectionNumber];
                       // layerList[i].connectionsInThisLayer += 1;
                        //Debug.Log("Connecting Neuron " + j + " To " + k + " In Layer " + i + " To Layer" + (1 + i) + " Number of actual Connection in this layer:" + layerList[i].connectionsInThisLayer);
                        connectionNumber++;
                    }           


                }
                layerList[i].Bias = connectionsWeights[connectionNumber];
                connectionNumber++;
            }


        }
    }
    public void connectNeurons(double[] connectionsWeights)
    {
        int connectionNumber = 0;
        for (int i = 0; i < layerList.Count; i++)
        {


            //Took a look every Neuron in every Layer to connect to next one
            if ((i + 1) == layerList.Count)
            {
                //Debug.Log("Next Layer is ouput");
                //layerList[i].Bias = connectionsWeights[connectionNumber];
            }
            else
            {
                for (int j = 0; j < layerList[i].neuronsList.Count; j++)
                {
                    for (int k = 0; k < layerList[(i + 1)].neuronsList.Count; k++)
                    {
                        connect(layerList[i].neuronsList[j], layerList[(1 + i)].neuronsList[k], connectionsWeights[connectionNumber]);
                        layerList[i].connectionsInThisLayer += 1;
                        //Debug.Log("Connecting Neuron " + j + " To " + k + " In Layer " + i + " To Layer" + (1 + i) + " Number of actual Connection in this layer:" + layerList[i].connectionsInThisLayer);
                        connectionNumber++;
                    }


                }
                layerList[i].Bias = connectionsWeights[connectionNumber];
                connectionNumber++;
            }


        }
    }

    public void connect (Neuron a, Neuron b)
    {

        Connection c = new Connection(a, b, UnityEngine.Random.Range(-10f, 10.0f));
        a.addConnection(c);
    }

    public void connect(Neuron a, Neuron b,double weight)
    {

        Connection c = new Connection(a, b, weight);
        a.addConnection(c);
    }

    private double Sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }

    private void Reset()
    {
        for (int i = 1; i < layerList.Count; i++)
        {
            for (int j = 0; j <layerList[i].neuronsList.Count; j++)
            {

                layerList[i].neuronsList[j].value = 0;

               
            }

        }
    }
    private string DoubleArrayToString(List<double> doubleArray)
    {
        var sb = new StringBuilder();
        foreach (var c in doubleArray)
        {
            sb.Append(c);
        }

        return sb.ToString();
    }
    public void readNeuralNetworkExport(List<double> neuralnetwork)
    {

        //Debug.Log(DoubleArrayToString(neuralnetwork));
        for (int i = 0; i < neuralnetwork.Count; i++)
        {
            Debug.Log("value "+i+":"+neuralnetwork[i].ToString());
        }

    }
    public List<double> exportNeuralNetwork()
    {
        List<double> neuralnetworkExport= new List<double>();
        
        
        //We add the number of layers and number of neurons in each layer
        neuralnetworkExport.Add(layerList.Count);

        for (int i = 0; i < neuralnetworkExport[0]; i++)
        {
            neuralnetworkExport.Add(layerList[i].neuronsList.Count);
        }


        //neuralnetworkExport = new double[getSizeNeuralNetwork()];
        //we add the number of layer connections
        neuralnetworkExport.Add(  layerList.Count - 1);
        Debug.Log("fdsfsdfdsf" + ((int)neuralnetworkExport[0] - 1));
        for (int i = 0; i <( neuralnetworkExport[0]-1); i++)
        {

            Debug.Log("klmklghmh");
                //we assign the number of connection in each layer
               neuralnetworkExport.Add( layerList[i].connectionsInThisLayer);            
           
               
        }
        
        //Loop to go thought the numbers of each layer connection
        for (int i = 0; i < neuralnetworkExport[0]-1; i++)
        {

           neuralnetworkExport.AddRange( layerList[i].getConnectionsWeights());

        }

        readNeuralNetworkExport(neuralnetworkExport);
        return neuralnetworkExport;
    }


    public int getSizeNeuralNetwork()
    {
        int temp=0;
        //We add the first number to the array that indicate the number of the next numbers in te array that showthe number of weight
        temp += 1;
        //We add the number of connections
        temp += layerList.Count - 1;
        //We add the bias of each layer
        temp += (layerList.Count - 1);
        //we add the value of connections
        for (int i = 0; i < layerList.Count; i++)
        {
            temp += layerList[i].connectionsInThisLayer;
        }

        Debug.Log("Total size this neural network " + temp);
        return temp;
    }


    public double[] FeedForward(double [] inputs)
    {
        Reset();

        double[] outputs= new double[layerList[layerList.Count-1].neuronsList.Count];

        //Debug.Log("Outputs:" + outputs.Length);
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < layerList[i].neuronsList.Count; j++)
            {
                //layerList[i].neuronsList[j].value = UnityEngine.Random.Range(-10.0f, 10.0f);
                layerList[i].neuronsList[j].value = inputs[j];

            }
        }

        for (int i = 0; i < layerList.Count; i++)
        {
            for (int j = 0; j < layerList[i].neuronsList.Count; j++)
            {
                //We check if is the hidden layers
                if (i != 0)
                {
                    layerList[i].neuronsList[j].value += layerList[i].Bias;
                    layerList[i].neuronsList[j].value = Sigmoid(layerList[i].neuronsList[j].value);
                }

                //we check if is the output  layer
                if(i==layerList.Count - 1)
                {
                    outputs[j] = layerList[i].neuronsList[j].value;
                    //Debug.Log("Last layer neuron");
                }
                for (int k = 0; k < layerList[i].neuronsList[j].connections.Count; k++)
                {
                    layerList[i].neuronsList[j].connections[k].b.value += layerList[i].neuronsList[j].value * layerList[i].neuronsList[j].connections[k].weight;

                    //  CreateConnection(network.layerList[i].neuronsList[j].connections[k].a.diagramLocation, network.layerList[i].neuronsList[j].connections[k].b.diagramLocation);
                }


            }

        }

        //for (int i = 0; i < outputs.Length; i++)
        //{
        //    Debug.Log("Value: " + outputs[i]);
        //}
        return outputs;
       
    }


}
