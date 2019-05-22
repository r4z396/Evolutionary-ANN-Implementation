using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GeneticAlgorithm  <T> 
{

	public List<DNA<T>> Population { get; private set; }
    public int Generation { get; private set; }
    public float BestFitness { get; private set; }
    public T[] BestGenes { get; private set; }

    public int Elitism;
    public int BestUnitsCrossOver;
    public int NewUnits;
    public float MutationRate;
    private System.Random random;
    List<DNA<T>> newPopulation;
    //private float fitnessSum;

    private int dnaSize;
    private Func<T> getRandomGene;
    private Func<int, float> fitnessFunction;   



    public GeneticAlgorithm(int populationSize, int dnaSize, System.Random random, Func<T> getRandomGene,Func<int, float> fitnessFunction,int elitism, int bestUnitsCrossOver, int newUnits, float mutationRate=0.01f)
    {
        Elitism = elitism;
        Generation = 1;
        MutationRate = mutationRate;
        Population = new List<DNA<T>>(populationSize);
        newPopulation = new List<DNA<T>>(populationSize);
        this.random = random;
        this.NewUnits = newUnits;
        this.dnaSize = dnaSize;
        this.BestUnitsCrossOver = bestUnitsCrossOver;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;

        BestGenes = new T[dnaSize];
        for (int i = 0; i < populationSize; i++)
        {
            
            Population.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction,i, shouldInitGenes: true));
        }
    }


    public void NewGeneration(int numberNewDNA = 0, bool crossoverNewDNA = false)
    {
        int finalCount = Population.Count + numberNewDNA;

        if (finalCount <= 0)
        {
            return;
        }

        if (Population.Count > 0)
        {
            CalculateFitness();
            //Debug.Log(Population[0].ID + " Best DNA");
            //Debug.Log(Population[1].ID + " Best DNA");
            //Population.Sort(CompareDNA);
            Population = Population.OrderByDescending(x => x.Fitness).ToList();
        }
       
        newPopulation.Clear();

        for (int i = 0; i < finalCount; i++)
        {
            Debug.Log("Position: " + i + " ID: " + Population[i].ID);
            if (i < Elitism&&i<Population.Count)
            {
                newPopulation.Add(Population[i]);

                // Debug.Log(Population[i].Fitness);
                Debug.Log(Population[i].Fitness);
                
                Debug.Log(Population[i].ID+" Best DNA saved");
                
            }
            else if (i < (Elitism + BestUnitsCrossOver) && i < Population.Count)
            {
                DNA<T> parent1 = ChooseParentBest();
                DNA<T> parent2 = ChooseParentBest();

                DNA<T> child = parent1.Crossover(parent2, Population[i].ID);

                child.Mutate(MutationRate);
                newPopulation.Add(child);
                Debug.Log("Child of Best DNAs");
            }
            else if (i < (Elitism + BestUnitsCrossOver+NewUnits) && i < Population.Count)
            {
                Debug.Log("Complete Random DNA");
                newPopulation.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction,Population[i].ID, shouldInitGenes: true));
            }
            else if(i<Population.Count/*||crossoverNewDNA*/)
            {
                DNA<T> parent1 = ChooseParent();
                DNA<T> parent2 = ChooseParent();

                DNA<T> child = parent1.Crossover(parent2, Population[i].ID);

                child.Mutate(MutationRate);
                newPopulation.Add(child);
                Debug.Log("Child of Random Parent in the Population");
            }
            else
            {
                Population.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction, Population[i].ID, shouldInitGenes: true));
            }
            


        }
        List<DNA<T>> tmpList = Population;
        Population = newPopulation;
        newPopulation = tmpList;


        Generation++;
    }



    public int CompareDNA(DNA<T> a, DNA<T> b)
    {
        if (a.Fitness > b.Fitness)
        {
            return -1;
        }
        else if(a.Fitness>b.Fitness)
        {
            return 1;
        }
        else
        {
            return 1;
        }
        
    }
    public void CalculateFitness()
    {
        //fitnessSum = 0;
        DNA<T> best = Population[0];

        //for (int i = 0; i < Population.Count; i++)
        //{
        //    fitnessSum += Population[i].CalculateFitness(i);

        //    if (Population[i].Fitness > best.Fitness)
        //    {
        //        best = Population[i];
        //    }
        //}

        BestFitness = best.Fitness;
        best.Genes.CopyTo(BestGenes, 0);
    }

    private DNA<T> ChooseParentBest()
    {
        return Population[random.Next(0, Elitism)];
        //double randomNumber = random.Next() * fitnessSum;
        //double randomNumber = random.NextDouble()* fitnessSum;
        //randomNumber = 3;
        //return Population[3];
        //for (int i = 0; i < Population.Count; i++)
        //{
        //    if (randomNumber < Population[i].Fitness)
        //    {
               
        //        return Population[i];
                
        //    }
        //    randomNumber -= Population[i].Fitness;
        //}
        
        return null;
    }
    private DNA<T> ChooseParent()
    {
        return Population[random.Next(0, Population.Count)];
        //double randomNumber = random.Next() * fitnessSum;
        //double randomNumber = random.NextDouble() * fitnessSum;
        ////randomNumber = 3;
        ////return Population[3];
        //for (int i = 0; i < Population.Count; i++)
        //{
        //    if (randomNumber < Population[i].Fitness)
        //    {

        //        return Population[i];

        //    }
        //    randomNumber -= Population[i].Fitness;
        //}

        //return null;
    }
}
