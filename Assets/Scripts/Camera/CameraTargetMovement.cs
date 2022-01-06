using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetMovement : MonoBehaviour
{
    [SerializeField] float speed;
    bool isMoving = false;
    Transform target;

    private void Start()
    {
        Listener();
    }

    void Update()
    {
        if (isMoving)
        {
            Moving();
        }
    }

    private void Listener()
    {
        LevelManager.instance.OnTab += Event_OnTab;
        LevelManager.instance.OnClimbDone += Event_OnClimbDone;
    }

    private void Event_OnTab(object sender, LevelManager.OnTabEventArgs e)
    {
        target = e.targetRock.transform;
        isMoving = true;
    }

    private void Event_OnClimbDone(object sender, System.EventArgs e)
    {
        isMoving = false;
        transform.position = target.position;
    }

    private void Moving()
    {
        Vector3 movementDirection = (target.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //rightHand.GetComponent<Rigidbody>().velocity = movementDirection * speed;
    }
}
