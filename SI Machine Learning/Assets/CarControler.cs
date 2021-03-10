using UnityEngine;

public class CarControler : MonoBehaviour
{
    public Rigidbody rb;
    public Transform centerOfMass;

    public Transform wheelFrontLeftMode, wheelFrontRightMode, wheelRearLeftMode, wheelRearRightMode;
    public WheelCollider wheelFrontLeftCollider, wheelFrontRightCollider, wheelRearLeftCollider, wheelRearRightCollider;

    public float horizontalInput;
    public float verticalInput;

    public float maxSteerAngle = 42;
    public float motorForce = 500;

    private void Start()
    {
        rb.centerOfMass = centerOfMass.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Steer();
        Accelerate();
        UpdateWheelPoses();
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
    }
}
