# Evolutionary-ANN-Implementation
!!!!!!!!!!!OPTIMIZE AND CLEAN CODE!!!!!!!!!!

Thesis Project Bachelor
https://www.youtube.com/watch?v=z1GyOucgAME&t=10s

1 Introduction

In this implementation the main objective was to hybridize Artificial Neural Networks with Genetic Algorithms and implement it in a simulator successfully. 
The first thing that we needed for this implementation was a Neural Network Framework and a Genetic Algorithm. The quickest thing should had been, to choose one of the multiple frameworks that can be found in Internet, but with all the research that has been done during this investigation it was easy to build one from scratch and the best way to learn something is to do it by oneself. 
No external frameworks were used, and it was done one from scratch in the programming language C#. 
After solving the problem with the base of the framework, we needed to find which simulator we will use and after study multiple choices, a car simulator was selected. 
The implementation was done using a game engine “Unity 3D”. 

 
2 Elements 
2.1 Car Simulator The Car simulator is a simple simulator with basic functions. 
• Accelerate / Decelerate • Turn Left/ Turn Right 
With this function, is enough to control the car. The car has 5 sensors in different angles to measure several distances to obstacles. 
All the cars race in the same race circuit at the same time. 

 
2.2 The hybridization 
To evolve the Neural Network has been used Neuroevolution. As we studied before we could evolve the Neural Networks in different ways, for this implementation we evolve the weights of the connections between Neurons and the Bias of each layer. 
The Network is composed by 4 Layers, the Input Layer has 6 Neurons, the Output Layer has 2 Neurons and the Hidden Layer has 6 and 4 Neurons. 
As Input has been used 5 front sensors that each car has plus the own speed of the car. From the Outputs we obtain two values, the first one represents the acceleration/deceleration and the second one represents the steering. 
Each Neural Network was encoded using a Weight Encoding technique, using real numbers. This let the Genetic algorithm work with an easier representation of the chromosomes. 

 
2.3 Main Steps Main steps of our implementation: 
1. Create initial population of 20 cars with random artificial neural networks 
2. Let all units play the game simultaneously by using their own neural networks 
3. Once all cars have crash, calculate its fitness to measure its quality for each unit. 
4. Apply a replacement strategy using genetic operators.
5. Go back to step 2 
 
 
2.4 Fitness Function

As we want to evolve a population, we should choose the fittest cars. The population must be sorted before each new generation. This order has to be done by checking the quality of each unit. 
Usually the fitness function is the way to measure the quality(fitness) of an object. While we have a fitness function to measure the fitness of each car, we can sort and select the fittest cars to reproduce and mutate for the next generation. 
In this implementation, each car is rewarded equally to its travelled distance through the race circuit, to select the quickest car we use the average speed over the race. So, in that way, we difference the cars that travelled the same distance. 
To conclude, our fitness function is the sum of the total distance covered by a car plus the average speed. 
 
 
2.5 Activation Function 

In this implementation the Tanh Function was chosen because like the Sigmoid Function, it is sigmoidal (“s” shaped) but the outputs range between (1,1) instead of (0,1). Strongly negative inputs will be mapped to negative outputs. Another advantage is that only zero-valued inputs will be mapped to near zero outputs. These properties will avoid the Neural Network to get “stuck” during the training. 
 
 
2.6 Replacement Strategy

As summary, the best cars will survive, and their children will replace the worst cars in the new generation. 
1. The population it is sorted by their fitness. 
2. Best 2 cars are selected and pass directly to the new generation. 
3. Create 12 child as a crossover of the best two and apply a mutation. 
4. Create 2 cars as a crossover of two random previous cars and apply a mutation. 
5. Create 4 cars with random genes.
