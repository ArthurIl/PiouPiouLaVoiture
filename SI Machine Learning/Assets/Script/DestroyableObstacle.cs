using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObstacle : MonoBehaviour
{
    public Agent[] agents;
    public GameObject agentGroup;
    
    public void GetAllAgent()
    {
        for (int i = 0; i < agentGroup.transform.childCount; i++)
        {
            agents[i] = agentGroup.transform.GetChild(i).GetComponent <Agent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
