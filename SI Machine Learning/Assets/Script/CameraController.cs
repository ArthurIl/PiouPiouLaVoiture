using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool isFree = false;
    public Transform target, freeTarget;

    public Vector3 cameraLocalPosition;
    public Vector3 localTargetLookAtPosition;

    public float positionLerpSpeed = 0.01f;
    public float lookLerpSpeed = 0.01f;

    Vector3 wantedPos;
    Quaternion wantedRotation;

	public float cameraSensitivity = 90;
	public float normalMoveSpeed = 10;

	private float rotationX = 0.0f;
	private float rotationY = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(isFree == false)
        {
            wantedPos = target.TransformPoint(cameraLocalPosition);
            wantedPos.y = cameraLocalPosition.y + target.position.y;
            transform.position = Vector3.Lerp(transform.position, wantedPos, positionLerpSpeed);

            wantedRotation = Quaternion.LookRotation(target.TransformPoint(localTargetLookAtPosition) - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, lookLerpSpeed);
        }
        else
        {
			rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
			rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
			rotationY = Mathf.Clamp(rotationY, -90, 90);

			transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);


				transform.position +=  transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
				transform.position +=  transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
		}

    }
}
