using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Objects :")]
    [SerializeField] private Transform Target;
    [Space(10)]

    [Header("Offsets :")]
    [SerializeField] private float XMultiply;
    [SerializeField] private float YMultiply;
    [SerializeField] private float XOffset;
    [SerializeField] private float YOffset;

    private void Update()
    {
        if(Target != null)
        {
            Vector2 TargetPosition = Target.position;
            Vector2 newPosition = new Vector2(TargetPosition.x * XMultiply + XOffset, TargetPosition.y * YMultiply + YOffset);

            transform.position = newPosition;

        }
    }
}
