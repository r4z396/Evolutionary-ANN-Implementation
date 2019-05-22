using System.Collections;
using System.Collections.Generic;
using System;


public class DNA<T>
{
    public T[] Genes
    {
        get;
        private set;
    }
    public float Fitness
    {
        get;
         set;
    }
    public int ID
    {
        get;
        set;
    }
    private Random random;
    private Func<T> getRandomGene;
    private Func<int, float> fitnessFunction;

    //Constructor recieve size of genes, seed random and a function to get a random gene
    public DNA(int size, Random random,Func<T> getRandomGene, Func<int, float> fitnessFunction,int id, bool shouldInitGenes=true)
    {
       Genes = new T[size];
		this.random = random;
		this.getRandomGene = getRandomGene;
		this.fitnessFunction = fitnessFunction;
        this.ID = id;
        //initialize genes if are new
        if (shouldInitGenes)
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                Genes[i] = getRandomGene();
            }
        }
        
    }

    public float CalculateFitness(int index)
    {
        //Fitness = fitnessFunction(index);
        return Fitness;
        
    }
    public void RandomDNA()
    {
        for (int i = 0; i < Genes.Length; i++)
        {
            Genes[i] = getRandomGene();
        }
       
    }
    public DNA<T> Crossover(DNA<T> otherParent,int id)
    {
        DNA<T> child = new DNA<T>(Genes.Length,random,getRandomGene,fitnessFunction,id,shouldInitGenes:false);      

        for (int i=0; i < Genes.Length; i++)
        {
            //50% to get each parent gene
            child.Genes[i] = random.NextDouble()<0.5 ? Genes[i]: otherParent.Genes[i];
            
        }
        return child;
    }
    public void Mutate(float mutationRate)
    {
        for (int i = 0; i < Genes.Length; i++)
        {
            if (random.NextDouble() < mutationRate)
            {
                Genes[i] = getRandomGene();
            }
        }
    }
}
