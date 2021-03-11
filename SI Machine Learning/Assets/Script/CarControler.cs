using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CarControler : MonoBehaviour
{
    public Rigidbody rb;
    public Transform centerOfMass;

    public Transform wheelFrontLeftMode, wheelFrontRightMode, wheelRearLeftMode, wheelRearRightMode;
    public WheelCollider wheelFrontLeftCollider, wheelFrontRightCollider, wheelRearLeftCollider, wheelRearRightCollider;

    public float horizontalInput;
    public float verticalInput;
    public float shootInput;

    public float maxSteerAngle = 42;
    public float motorForce = 500;

    public Transform bulletPoint;
    public float shootDistance;
    public ParticleSystem bulletEffect, explosionEffect;
    public LayerMask layer;
    public Collider carCollider;
    public float cooldown;
    bool canShoot = true;

    private void Start()
    {
        rb.centerOfMass = centerOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        Steer();
        Accelerate();
        UpdateWheelPoses();

        if(shootInput >= 0.5f && canShoot == true || shootInput <= -0.5f && canShoot == true)
        StartCoroutine(Shoot());
    }

    void Steer()
    {
        wheelFrontLeftCollider.steerAngle = horizontalInput * maxSteerAngle;
        wheelFrontRightCollider.steerAngle = horizontalInput * maxSteerAngle;
    }

    void Accelerate()
    {
        wheelRearLeftCollider.motorTorque = verticalInput * motorForce;
        wheelRearRightCollider.motorTorque = verticalInput * motorForce;
    }

    void UpdateWheelPoses()
    {
        UpdateWheelPose(wheelFrontLeftCollider, wheelFrontLeftMode);
        UpdateWheelPose(wheelFrontRightCollider, wheelFrontRightMode);
        UpdateWheelPose(wheelRearLeftCollider, wheelRearLeftMode);
        UpdateWheelPose(wheelRearRightCollider, wheelRearRightMode);
    }

    Vector3 pos;
    Quaternion quat;

    void UpdateWheelPose(WheelCollider col, Transform tr)
    {
        pos = tr.position;
        quat = tr.rotation;

        col.GetWorldPose(out pos, out quat);

        tr.position = pos;
        tr.rotation = quat;

    }

    public void Reset()
    {
        horizontalInput = 0;
        verticalInput = 0;
        motorForce = 500;
        if(obstacleDestroyed.Count != 0)
        {
            for (int i = 0; i < obstacleDestroyed.Count; i++)
            {
                if (obstacleDestroyed[i] != null)
                {
                    Physics.IgnoreCollision(carCollider, obstacleDestroyed[i].GetComponent<Collider>(), false);
                    obstacleDestroyed.Remove(obstacleDestroyed[i]);
                }
            }
        }
    }

    public List<Collider> obstacleDestroyed = new List<Collider>();
    public IEnumerator Shoot()
    {
        canShoot = false;
        RaycastHit hit;

        if (Physics.Raycast(bulletPoint.position, transform.forward, out hit, shootDistance, layer))
        {
            bool addIt = true;
            for (int i = 0; i < obstacleDestroyed.Count; i++)
            {
                if(hit.transform.gameObject != obstacleDestroyed[i])
                {
                    addIt = false;
                }
            }
            if (addIt == true)
            {
                Physics.IgnoreCollision(carCollider, hit.collider.gameObject.GetComponent<Collider>(), true);
                obstacleDestroyed.Add(hit.collider.gameObject.GetComponent<Collider>());
                Instantiate(bulletEffect, bulletPoint.position, Quaternion.identity);
                Instantiate(explosionEffect, hit.transform.position, Quaternion.identity);
            }
            Debug.DrawRay(bulletPoint.position, transform.forward * hit.distance, Color.white);

        }
        yield return new WaitForSeconds(cooldown);
        canShoot = true;

    }
}
