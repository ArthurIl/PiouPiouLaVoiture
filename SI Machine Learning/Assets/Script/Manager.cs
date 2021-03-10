using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int populationSize;
    public float trainingDuration = 30;
    public float mutationRate = 5;

    public Agent agentPrefab;
    public Transform agentGroup;

    Agent agent;
    List<Agent> agents = new List<Agent>();

    public CameraController cameraController;

    void Start()
    {
        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        NewGeneration();
        InitNeuralNetworkViewer();
        Focus();
        yield return new WaitForSeconds(trainingDuration);
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        NewGeneration();
        Focus();
        yield return new WaitForSeconds(trainingDuration);
        StartCoroutine(Loop()); 
    }

    private void NewGeneration()
    {
        agents.Sort();
        AddOrRemoveAgents();
        Mutate();
        Reset();
        SetMaterial();
    }

    void AddOrRemoveAgents()
    {
        if (agents.Count != populationSize)
        {
            int dif = populationSize - agents.Count;

            if (dif > 0)
            {
                for (int i = 0; i < dif; i++)
                {
                    AddAgent();
                }
                
            }
            else
            {
                for (int i = 0; i > dif; i++)
                {
                    RemoveAgent();
                }
            }
        }
    }
    
    void AddAgent()
    {
        agent = Instantiate(agentPrefab, Vector3.zero, Quaternion.identity, agentGroup);
        agent.net = new NeuralNetwork(agentPrefab.net.layers);
        
        agents.Add((agent));
    }
    
    private void RemoveAgent()
    {
        Destroy((agents[agents.Count-1]).transform);
        agents.RemoveAt(agents.Count -1);
    }
    
    private void Mutate()
    {
        for (int i = agents.Count/2; i < agents.Count; i++)
        {
            agents[i].net.CopyNet(agents[i-agents.Count/2].net);
            agents[i].net.Mutate(mutationRate);
            agents[i].SetMutatedMaterial();
        }
    }
    
    private void Reset()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].ResetAgent();
        }
    }
    
    private void SetMaterial()
    {
        agents[0].SetFirstMaterial();

        for (int i = 1; i < agents.Count/2; i++)
        {
            agents[i].SetDefaultMaterial();
        }
    }
    
    private void Focus()
    {
        cameraController.target = agents[0].transform;
            
        NeuralNetworkViewer.instance.agent = agents[0];
        NeuralNetworkViewer.instance.RefreshAxons();
    }

    public void ReFocus()
    {
        agents.Sort();
        Focus();
    }

    public void End()
    {
        StopAllCoroutines();
        StartCoroutine(Loop());
    }

    public void ResetNets()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].net = new NeuralNetwork(agentPrefab.net.layers);
        }
        
        End();
    }

    public void Save()
    {
        List<NeuralNetwork> nets = new List<NeuralNetwork>();

        for (int i = 0; i < agents.Count; i++)
        {
            nets.Add(agents[i].net);
        }
        
        DataManager.instance.Save(nets);
    }

    public void Load()
    {
        Data data = DataManager.instance.Load();

        if (data != null)
        {
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].net = data.nets[i];
            }
        }
        
        End();
    }
    
    void InitNeuralNetworkViewer()
    {
        NeuralNetworkViewer.instance.Init(agents[0]);
    }

}
