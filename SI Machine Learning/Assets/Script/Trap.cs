using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public LayerMask LayerMask;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 10 )
        {
            crash(col.gameObject);
        }
    }

    void crash(GameObject Go)
    {
        Go.GetComponentInParent<CarControler>().motorForce = 0;        
    }
}
