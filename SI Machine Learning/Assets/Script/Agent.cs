using System;
using UnityEngine;

public class Agent : MonoBehaviour, IComparable<Agent>
{
    public NeuralNetwork net;
    public CarControler carControler;

    public float fitness;
    public float distanceTraveled;

    public Rigidbody rb;

    float[] inputs;

    public Transform nextCheckpoint;

    public float nextCheckpointDist;

    public void ResetAgent()
    {
        fitness = 0;
        distanceTraveled = 0;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        inputs = new float[net.layers[0]];
        
        carControler.Reset();

        nextCheckpoint = CheckpointManager.instance.firstCheckpoint;
        nextCheckpointDist = (transform.position - nextCheckpoint.position).magnitude;
    }

    private void FixedUpdate()
    {
        InputUpdate();
        OutputUpdate();
        FitnessUpdate();
    }

    void InputUpdate()
    {
        inputs[0] = RaySensor(transform.position + Vector3.up * 0.2f,transform.forward,4);
        inputs[1] = RaySensor(transform.position + Vector3.up * 0.2f,transform.right,1.5f);
        inputs[2] = RaySensor(transform.position + Vector3.up * 0.2f,-transform.right,1.5f);
        inputs[3] = RaySensor(transform.position + Vector3.up * 0.2f,transform.forward + transform.right,2);
        inputs[4] = RaySensor(transform.position + Vector3.up * 0.2f,transform.forward - transform.right,2);

        inputs[5] = (float)Math.Tanh(rb.velocity.magnitude * 0.05f);
        inputs[6] = (float) Math.Tanh(rb.angularVelocity.y * 0.1f);

        inputs[7] = 1;
        inputs[8] = ObstacleSensor(transform.position + Vector3.up * 0.2f, transform.forward, 4);
    }

    RaycastHit hit;
    float range = 4;
    public LayerMask layerMask, DestroyableMask;
    float RaySensor(Vector3 pos, Vector3 direction, float lenght)
    {
        if (Physics.Raycast(pos, direction, out hit, lenght * range, layerMask))
        {
            Debug.DrawRay(pos, direction * hit.distance,
                Color.Lerp(Color.red, Color.green, (range * lenght - hit.distance) / (range * lenght)));
            return (range * lenght - hit.distance) / (range * lenght);
        }
        else
        {
            Debug.DrawRay(pos, direction * range * lenght, Color.red);
            return 0;
            
        }
    }

    float ObstacleSensor(Vector3 pos, Vector3 direction, float lenght)
    {
        if (Physics.Raycast(pos, direction, out hit, lenght * range, DestroyableMask))
        {
            return 1f;
        }
        else
        {
            return 0;
        }
    }

    void OutputUpdate()
    {
        net.FeedForward(inputs);

        carControler.horizontalInput = net.neurons[net.layers.Length-1][0];
        carControler.verticalInput = net.neurons[net.layers.Length-1][1];
        carControler.shootInput = net.neurons[net.layers.Length - 1][2];
    }

    float currentDistance;
    void FitnessUpdate()
    {
        currentDistance = distanceTraveled +
                          (nextCheckpointDist - (transform.position - nextCheckpoint.position).magnitude);
        if (fitness < currentDistance)
        {
            fitness = currentDistance;
        }
    }
    
    public void CheckpointReached(Transform checkpoint)
    {
        distanceTraveled += nextCheckpointDist;
        nextCheckpoint = checkpoint;
        nextCheckpointDist = (transform.position - checkpoint.position).magnitude;
    }

    public Renderer render, gunBarrel, gunTrigger;
    public Material firstMaterial;
    public Material mutatedMaterial;
    public Material defaultMaterial;
    
    public void SetFirstMaterial()
    {
        render.material = firstMaterial;
        gunBarrel.material = firstMaterial;
        gunTrigger.material = firstMaterial;
    }

    public void SetMutatedMaterial()
    {
        render.material = mutatedMaterial;
        gunBarrel.material = mutatedMaterial;
        gunTrigger.material = mutatedMaterial;
    }

    public void SetDefaultMaterial()
    {
        render.material = defaultMaterial;
        gunBarrel.material = defaultMaterial;
        gunTrigger.material = defaultMaterial;
    }

    public int CompareTo(Agent other)
    {
        if (fitness < other.fitness)
        {
            return 1;
        }

        if (fitness > other.fitness)
        {
            return -1;
        }

        return 0;
    }
}
