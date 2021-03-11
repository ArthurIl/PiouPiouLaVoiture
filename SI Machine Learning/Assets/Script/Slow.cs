using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    public LayerMask LayerMask;
    public int BoostValue = -200;
    public int minBoostValue = 300;
    public int BoostDuration = 5;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 10)
        {
            StartCoroutine(Nerfer(col.gameObject));
        }
    }

    IEnumerator Nerfer(GameObject Go)
    {
        float val;
        val = Go.GetComponentInParent<CarControler>().motorForce;
        if (Go.GetComponentInParent<CarControler>().motorForce > minBoostValue)
        {
            Go.GetComponentInParent<CarControler>().motorForce += BoostValue;
            yield return new WaitForSeconds(BoostDuration);
            Go.GetComponentInParent<CarControler>().motorForce = val;
        }

    }

}
