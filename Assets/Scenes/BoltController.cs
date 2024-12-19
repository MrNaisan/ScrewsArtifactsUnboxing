using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;

public class BoltController : MonoBehaviour
{
    public float unscrewSpeed = 10f;
    public float liftSpeed = 1f;
    public float unscrewThreshold = 5f;
    public float liftHeight = 1f;
    private float currentRotation = 0f;
    private bool isInteracting = false;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isInteracting)
        {
            float rotationAmount = unscrewSpeed * Time.deltaTime;
            transform.Rotate(0, -rotationAmount, 0);


            transform.Translate(0, liftSpeed * Time.deltaTime, 0, Space.Self);


            if (Vector3.Distance(transform.position, initialPosition) >= liftHeight)
            {
                UnscrewComplete();
            }
        }
    }

    private void UnscrewComplete()
    {
        isInteracting = false;
        Debug.Log("Bolt unscrewed");
        Destroy(gameObject); 
    }

    private void OnMouseDown()
    {
        isInteracting = true; // Активируем вращение после клика
    }
}


