using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public LayerMask LayerMask;
    public int BoostValue = 200;
    public int maxBoostValue = 700;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 10 )
        {
                StartCoroutine(Booster(col.gameObject));
        }
    }

    IEnumerator Booster(GameObject Go)
    {
        float val;
        val = Go.GetComponentInParent<CarControler>().motorForce;
        if (Go.GetComponentInParent<CarControler>().motorForce < maxBoostValue)
        {
            Go.GetComponentInParent<CarControler>().motorForce += BoostValue;
            yield return new WaitForSeconds(5);
            Go.GetComponentInParent<CarControler>().motorForce = val;
        }

    }

}
