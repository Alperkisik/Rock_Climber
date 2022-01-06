using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public enum Direction
    {
        Left, Right
    }

    [SerializeField] float movementSpeed;
    [SerializeField] private float rotatingSpeed;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;
    Direction direction;
    bool isMoving;

    private void Start()
    {
        isMoving = true;
        direction = Direction.Right;
    }

    void Update()
    {
        Rotating();
        if (isMoving) Movement();
    }

    private void Rotating()
    {
        transform.Rotate(Vector3.up * rotatingSpeed * Time.deltaTime);
    }

    private void Movement()
    {
        if (direction.Equals(Direction.Right))
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPoint.position, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, rightPoint.position) <= 0.1f) direction = Direction.Left;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPoint.position, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, leftPoint.position) <= 0.1f) direction = Direction.Right;
        }
    }
}
