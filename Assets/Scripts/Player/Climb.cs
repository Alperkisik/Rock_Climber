using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    public enum Hand
    {
        Left, Right
    }

    [SerializeField] float climbSpeed;
    [SerializeField] GameObject rightHand;
    [SerializeField] GameObject leftHand;

    Hand hand;
    bool IsClimbing;
    Transform climbTarget;

    void Start()
    {
        IsClimbing = false;
        Listener();
    }

    private void FixedUpdate()
    {
        if (IsClimbing)
        {
            if (!IsClimbDone()) Climbing();
            else ClimbDone();
        }
    }

    private void Listener()
    {
        LevelManager.instance.OnTab += Event_OnTab;
        LevelManager.instance.OnClimbDone += Event_OnClimbDone;
        LevelManager.instance.OnHitByObstacle += Event_OnHitByObstacle;
    }

    private void Event_OnHitByObstacle(object sender, System.EventArgs e)
    {
        if (IsClimbing) IsClimbing = false;
        rightHand.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void Event_OnTab(object sender, LevelManager.OnTabEventArgs e)
    {
        climbTarget = e.targetRock.transform;

        if (climbTarget.position.x >= GameObject.Find("CameraTarget").transform.position.x) hand = Hand.Right;
        else hand = Hand.Left;
        /*
        float distanceToRight = Vector3.Distance(rightHand.transform.position, climbTarget.position);
        float distanceToLeft = Vector3.Distance(leftHand.transform.position, climbTarget.position);

        if (distanceToRight < distanceToLeft) hand = Hand.Right;
        else hand = Hand.Left;*/

        IsClimbing = true;
    }

    private void Event_OnClimbDone(object sender, System.EventArgs e)
    {
        IsClimbing = false;
    }

    private bool IsClimbDone()
    {
        float distance;
        if (hand == Hand.Right) distance = Vector3.Distance(climbTarget.position, rightHand.transform.position);
        else distance = Vector3.Distance(climbTarget.position, leftHand.transform.position);

        if (distance < 0.3f) return true;
        else return false;
    }

    private void Climbing()
    {
        if (hand == Hand.Right)
        {
            Vector3 movementDirection = (climbTarget.position - rightHand.transform.position).normalized;
            rightHand.GetComponent<Rigidbody>().velocity = movementDirection * climbSpeed;
        }
        else
        {
            Vector3 movementDirection = (climbTarget.position - leftHand.transform.position).normalized;
            leftHand.GetComponent<Rigidbody>().velocity = movementDirection * climbSpeed;
        }
    }

    private void ClimbDone()
    {
        GameObject climbingHand;
        if (hand == Hand.Right)
        {
            rightHand.GetComponent<Rigidbody>().velocity = Vector3.zero;
            rightHand.transform.position = climbTarget.position;
            climbingHand = rightHand;
        }
        else
        {
            leftHand.GetComponent<Rigidbody>().velocity = Vector3.zero;
            leftHand.transform.position = climbTarget.position;
            climbingHand = leftHand;
        }

        LevelManager.instance.Event_OnClimbDone(climbingHand);
    }
}
