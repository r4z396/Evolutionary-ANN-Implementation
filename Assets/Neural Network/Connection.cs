using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    public Neuron a;
    public Neuron b;
    public double weight;
    public Connection(Neuron a, Neuron b, double w)
    {

        weight = w;
        this.a = a;
       this.b = b;
    }


}
