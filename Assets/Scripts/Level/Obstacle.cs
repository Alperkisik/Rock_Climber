using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //layer 6 is player body layer 
        if(collision.gameObject.layer == 6)
        {
            LevelManager.instance.Event_OnHitByObstacle();
        }
    }
}
