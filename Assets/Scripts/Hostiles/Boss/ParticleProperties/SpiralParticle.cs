using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralParticle : MonoBehaviour
{
    public float speed = 1.0f;     
    public float radiusIncrease = 0.1f;
    private float angle = 0f;     

    void Update()
    {
        float radius = radiusIncrease * angle;
        angle += speed * Time.deltaTime;

        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
