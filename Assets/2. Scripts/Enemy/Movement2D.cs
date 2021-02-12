using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [Header("Enemy Moving Speed")]
    public float moveSpeed = 0.0f;
    private float currentSpeed;

    public Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        transform.position += moveDirection * currentSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }

    public void initSpeed()
    {
        currentSpeed = moveSpeed;
    }

    public void changeSpeed(float percent)
    {
        currentSpeed*=percent;
    }
}
