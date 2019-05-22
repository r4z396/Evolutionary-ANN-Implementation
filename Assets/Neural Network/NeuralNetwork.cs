using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NeuralNetwork : MonoBehaviour
{
    [SerializeField] bool runningGeneration=false;
    [Header("Genetic Algorithm")]
    [SerializeField] int populationSize = 10;
    [SerializeField] float mutationRate = 0.01f;
    [SerializeField] int elitism = 5;
    [SerializeField] int bestUnitsCrossOver = 5;
    [SerializeField] int NewUnits = 5;
    [SerializeField] int neuralNetworkSize = 30;

    [Header("Population")]
    [SerializeField] List<Network> population;
    [SerializeField] GameObject carPrefab;
    [SerializeField] List<GameObject> carPopulation;
    [Header("Other")]
    private GeneticAlgorithm<double> ga;
    private System.Random random;


    [Header("Neural Network Options")]
    Network network;
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] public int[] size ={ 3, 3 };
    [SerializeField] public double[] test = { 3, 3 };

    [Header("DIAGRAM")]
    
    [SerializeField] Font fontText;


    //Diagram
    [SerializeField] Sprite neuronSprite;
    [SerializeField] RectTransform diagramContainer;
    public List<GameObject> neuronTextList;
    public List<GameObject> connectionTextList;

    [SerializeField]
    Text generationText;
    int generation = 1;

    void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("neuron", typeof(Image));
        gameObject.transform.SetParent(diagramContainer, false);
        gameObject.GetComponent<Image>().sprite = neuronSprite;

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        RectTransform rectTransformParent = gameObject.transform.parent.GetComponent<RectTransform>();
       // Debug.Log("ANCHORED POSITION " + anchoredPosition.x + "," + anchoredPosition.y);
        float xPos = /*(rectTransformParent.anchoredPosition.x+ rectTransformParent.sizeDelta.x/2)+*/ anchoredPosition.x;
        float yPos =/* (rectTransformParent.anchoredPosition.y+ rectTransformParent.sizeDelta.y/2 )+*/ anchoredPosition.y;
        rectTransform.anchoredPosition = new Vector2(xPos, yPos);
       // rectTransform.anchoredPosition = new Vector2(xPos,yPos);
        rectTransform.sizeDelta = new Vector2(24, 24);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        //Create Value Text
        GameObject gameObjectText = new GameObject("neuronText", typeof(Text));
        gameObjectText.transform.SetParent(diagramContainer, false);
        gameObjectText.GetComponent<Text>().text="TEST";
        gameObjectText.GetComponent<Text>().font = fontText;
        gameObjectText.GetComponent<Text>().color = Color.black;
        rectTransform = gameObjectText.GetComponent<RectTransform>();
        rectTransformParent = gameObjectText.transform.parent.GetComponent<RectTransform>();
        // Debug.Log("ANCHORED POSITION " + anchoredPosition.x + "," + anchoredPosition.y);
        xPos = /*(rectTransformParent.anchoredPosition.x+ rectTransformParent.sizeDelta.x/2)+*/ anchoredPosition.x;
        yPos =/* (rectTransformParent.anchoredPosition.y+ rectTransformParent.sizeDelta.y/2 )+*/ anchoredPosition.y;
        rectTransform.anchoredPosition = new Vector2(xPos, yPos);
        // rectTransform.anchoredPosition = new Vector2(xPos,yPos);
        rectTransform.sizeDelta = new Vector2(40, 20);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        neuronTextList.Add(gameObjectText);

    }


    float  GetAngleFromVectorFloat(Vector2 dir)
    {
        float difference = dir.x / dir.y;
        float angleRot = Mathf.Atan(difference) * 180 / Mathf.PI;
        return angleRot;
    }


    void CreateConnection(Vector2 positionA, Vector2 positionB)
    {
        GameObject gameObject = new GameObject("connection", typeof(Image));
        gameObject.transform.SetParent(diagramContainer, false);


        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(100, 0.5f);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        Vector2 dir = (positionB - positionA).normalized;// Vector2.Angle(positionA, positionB);
        RectTransform rectTransformParent = gameObject.transform.parent.GetComponent<RectTransform>();
        float xPos = /*(rectTransformParent.anchoredPosition.x + rectTransformParent.sizeDelta.x / 2) +*/ positionA.x;
        float yPos = /*(rectTransformParent.anchoredPosition.y + rectTransformParent.sizeDelta.y / 2) +*/ positionA.y;

        //float xPos = (rectTransform.anchoredPosition.x + rectTransform.sizeDelta.x)/* + anchoredPosition.x*/+positionA.x;
        //float yPos = (rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y)/* + anchoredPosition.y*/+positionA.y;

        float distance = Vector2.Distance(positionA, positionB);
        rectTransform.sizeDelta = new Vector2(distance, 2f);
        rectTransform.anchoredPosition = /*positionA + dir * distance * 0.5f*/ (positionA+positionB)*0.5f;// new Vector2(xPos,yPos);
       // print();
        rectTransform.localEulerAngles=(new Vector3(0, 0, AngleBetweenVector2(positionA, positionB)));


        //Create Value Text
        GameObject gameObjectText = new GameObject("weightText", typeof(Text));
        gameObjectText.transform.SetParent(diagramContainer, false);
        gameObjectText.GetComponent<Text>().text = "TEST";
        gameObjectText.GetComponent<Text>().font = fontText;
        gameObjectText.GetComponent<Text>().color = Color.blue;
        rectTransform = gameObjectText.GetComponent<RectTransform>();
        rectTransformParent = gameObjectText.transform.parent.GetComponent<RectTransform>();
        // Debug.Log("ANCHORED POSITION " + anchoredPosition.x + "," + anchoredPosition.y);
        Vector2 middlePoint = new Vector2((positionB.x+positionA.x)/2, (positionB.y + positionA.y) / 2);
        
       xPos = /*(rectTransformParent.anchoredPosition.x + rectTransformParent.sizeDelta.x / 2) +*/ middlePoint.x;
        yPos = /*(rectTransformParent.anchoredPosition.y + rectTransformParent.sizeDelta.y / 2) +*/ middlePoint.y;
        rectTransform.anchoredPosition = new Vector2(xPos, yPos);
        // rectTransform.anchoredPosition = new Vector2(xPos,yPos);
        rectTransform.sizeDelta = new Vector2(40, 20);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        connectionTextList.Add(gameObjectText);

    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    void DisplayDiagram()
    {

        float diagramHeight = diagramContainer.sizeDelta.y;
        
        for (int i = 0; i < network.layerList.Count; i++)
        {
            for (int j = 0; j < network.layerList[i].neuronsList.Count; j++)
            {


                float xPos = network.layerList[i].neuronsList[j].diagramLocation.x;
                float yPos = /*(network.neuronsList[i].diagramLocation.y/yMax)*diagramHeight*/ network.layerList[i].neuronsList[j].diagramLocation.y;
                CreateCircle(new Vector2(xPos, yPos));


                for (int k = 0; k < network.layerList[i].neuronsList[j].connections.Count; k++)
                {

                    CreateConnection(network.layerList[i].neuronsList[j].connections[k].a.diagramLocation, network.layerList[i].neuronsList[j].connections[k].b.diagramLocation);
                }
            }

        }
    }
    // Start is called before the first frame update

    void Start()
    {
        
        //UpdateValues();

        //Network a=new Network( network.exportNeuralNetwork());
        //a.exportNeuralNetwork();



        neuralNetworkSize =calculateDNASize();
        random = new System.Random();
        ga = new GeneticAlgorithm<double>(populationSize, neuralNetworkSize, random, GetRandomDouble, FitnessFunction, elitism, bestUnitsCrossOver,NewUnits,mutationRate);
        population = new List<Network>();
        generationText.text ="Generation: "+ generation;
        initPopulation();

        StartCoroutine(StartCountdown());


        SetupNeuralNetwork();
        DisplayDiagram();
        //SetupNeuralNetwork();
        //DisplayDiagram();
       UpdateValues();

    }
    void initPopulation()
    {
        

        for (int i = 0; i < populationSize; i++)
        {
            Network temp = new Network(size, ga.Population[i].Genes);
           population.Add(temp);
            //GameObject gameObject = new GameObject("Car "+ i);
            
            carPopulation.Add(Instantiate(carPrefab, new Vector3(0, 1, 0), Quaternion.identity));
            carPopulation[i].transform.name = "Car: " + i;
            ga.Population[i].ID = i;

        }

        runningGeneration = true;

    }

    void resetPopulation()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Destroy(carPopulation[i]);
        }

            population.Clear();
        carPopulation.Clear();
        for (int i = 0; i < populationSize; i++)
        {

            
            Network temp = new Network(size, ga.Population[i].Genes);
            population.Add(temp);
            //GameObject gameObject = new GameObject("Car "+ i);

            carPopulation.Add(Instantiate(carPrefab, new Vector3(0, 1, 0), Quaternion.identity));
            carPopulation[i].transform.name = "Position:"+i+" Car ID: " +ga.Population[i].ID ;
            ga.Population[i].ID = i;
            //Debug.Log("ID " + ga.Population[i].ID);
            

        }
    }

    void updateNewBrainsPopulation()
    {
        
        for (int i = 0; i < populationSize; i++)
        {           
            ga.Population[i].Fitness = carPopulation[i].GetComponent<CarController>().getFitness();
        }
        
        ga.NewGeneration();
        
        network.updateNewWeights(ga.Population[0].Genes);
        //UpdateValues();
        resetPopulation();
        //for (int i = 0; i < populationSize; i++)
        //{

        //    //carPopulation[i].transform.name = "Car: " + i;
        //    Debug.Log(ga.Population[i].Genes.ToString());
        //    population[i].updateNewWeights(ga.Population[i].Genes);


        //    carPopulation[i].transform.position=(Vector3.zero);
        //    carPopulation[i].transform.rotation = Quaternion.Euler(Vector3.zero);
            
        //    carPopulation[i].GetComponent<CarController>().Reset();


            

        //}

        generation++;
        generationText.text = "Generation: " + generation;
       
    }
    
    int calculateDNASize()
    {
        int sizeDNA = /*(size.Length+1)+*/(size.Length);
        for (int i = 0; i < size.Length-1; i++)
        {
            sizeDNA += size[i]*size[i+1];
        }

        Debug.Log("SIZE DNA: "+sizeDNA);
        
        return sizeDNA;
    }
    private void Reset()
    {
        for (int i = 1; i < network.layerList.Count; i++)
        {
            for (int j = 0; j < network.layerList[i].neuronsList.Count; j++)
            {

                network.layerList[i].neuronsList[j].value = 0;

                
            }

        }
    }


    private double GetRandomDouble()
    {
        //double i = random.NextDouble();
        return UnityEngine.Random.Range(-10f,10f);
    }

    private float FitnessFunction(int index)
    {
        float score = 0;
        DNA<double> dna = ga.Population[index];

        //for (int i = 0; i < dna.Genes.Length; i++)
        //{
        //    if (dna.Genes[i] == targetString[i])
        //    {
        //        score += 1;
        //    }
        //}

        //score /= targetString.Length;
        //score = (Mathf.Pow(2, score) - 1) / (2 - 1);


        return score;
    }



    private double Sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }




    void FeedForward()
    {
        Reset();

        for (int i = 0; i < 1 ; i++)
        {
            for (int j = 0; j < network.layerList[i].neuronsList.Count; j++)
            {
                network.layerList[i].neuronsList[j].value = UnityEngine.Random.Range(-10.0f, 10.0f);
            }
        }

        for (int i = 0; i < network.layerList.Count; i++)
        {
            for (int j = 0; j < network.layerList[i].neuronsList.Count; j++)
            {

                if (i != 0)
                {
                    network.layerList[i].neuronsList[j].value += network.layerList[i].Bias;
                    network.layerList[i].neuronsList[j].value = Sigmoid(network.layerList[i].neuronsList[j].value);
                }
                    

                for (int k = 0; k < network.layerList[i].neuronsList[j].connections.Count; k++)
                {
                    network.layerList[i].neuronsList[j].connections[k].b.value += network.layerList[i].neuronsList[j].value * network.layerList[i].neuronsList[j].connections[k].weight;

                    //  CreateConnection(network.layerList[i].neuronsList[j].connections[k].a.diagramLocation, network.layerList[i].neuronsList[j].connections[k].b.diagramLocation);
                }
            }

        }
        UpdateValues();
    }

    void UpdateValues()
    {

        int neuronCount = 0;
        int connectionCount = 0;
        for (int i = 0; i < network.layerList.Count; i++)
        {
            for (int j = 0; j < network.layerList[i].neuronsList.Count; j++)
            {


                neuronTextList[neuronCount].GetComponent<Text>().text =  network.layerList[i].neuronsList[j].value.ToString();


                neuronCount++;
                for (int k = 0; k < network.layerList[i].neuronsList[j].connections.Count; k++)
                {
                    connectionTextList[connectionCount].GetComponent<Text>().text = network.layerList[i].neuronsList[j].connections[k].weight.ToString();
                    connectionCount++;
                  //  CreateConnection(network.layerList[i].neuronsList[j].connections[k].a.diagramLocation, network.layerList[i].neuronsList[j].connections[k].b.diagramLocation);
                }
            }

        }
    }

    void SetupNeuralNetwork()
    {
        //Debug.Log(diagramContainer.sizeDelta.x + " " + diagramContainer.sizeDelta.y);
        //network = new Network(diagramContainer.sizeDelta.x, diagramContainer.sizeDelta.y, 4);

        //network.createNeurons(0, 2);
        //network.createNeurons(1, 4);
        //network.createNeurons(2, 3);
        //network.createNeurons(3, 2);



        //network = new Network(diagramContainer.sizeDelta.x, diagramContainer.sizeDelta.y, size);
        network = new Network(diagramContainer.sizeDelta.x, diagramContainer.sizeDelta.y, size, ga.Population[0].Genes);
       // network = population[0];

    }


    float currCountdownValue;
    public IEnumerator StartCountdown(float countdownValue = 20)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        runningGeneration = false;
        currCountdownValue = 2;
        while (currCountdownValue > 0)
        {
            Debug.Log("Updating " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        //}
        //Debug.Log("-----------------------");
       
        updateNewBrainsPopulation();
        currCountdownValue = 2;
        while (currCountdownValue > 0)
        {
            Debug.Log("Start " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        runningGeneration = true;
        StartCoroutine(StartCountdown());

    }

    private void Update()
    {


       //print("random" +random.NextDouble());
        
        if (Input.GetKeyDown("space"))
        {

            network.FeedForward(test);
            UpdateValues();

            print("FeedForward");
        }

        if (runningGeneration)
        {
            for (int i = 0; i < populationSize; i++)
            {
                //carPopulation[i].GetComponent<CarController>().getInputs();
                carPopulation[i].GetComponent<CarController>().activate(population[i].FeedForward(carPopulation[i].GetComponent<CarController>().getInputs()));
            }
        }
       
    }
}
