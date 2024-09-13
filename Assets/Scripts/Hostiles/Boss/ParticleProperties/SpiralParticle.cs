using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralParticle : MonoBehaviour
{
    public float speed = 1.0f;
    public float radiusIncrease = 0.1f;
    private float angle = 0f;
    private Vector3 initialPosition;  // Store the initial position of the particle

    void Awake()
    {
        // Save the initial position of the object
        initialPosition = transform.position;
    }

    void Update()
    {
        Debug.Log("Orb Position: " + transform.position);

        float radius = radiusIncrease * angle;
        angle += speed * Time.deltaTime;

        // Calculate new x and y based on the angle and radius
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);

        // Update the position, but add the offset to the initial position
        transform.position = new Vector3(initialPosition.x + x, initialPosition.y + y, initialPosition.z);
    }
}
