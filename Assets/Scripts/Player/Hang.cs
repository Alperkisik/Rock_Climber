using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hang : MonoBehaviour
{
    [SerializeField] GameObject rightHand;

    bool isHanging = false;
    Vector3 hangingPosition;
    GameObject rock;
    bool firstTab = false;

    void Start()
    {
        firstTab = false;
        Listener();
    }

    private void Listener()
    {
        LevelManager.instance.OnTab += Event_OnTab;
        LevelManager.instance.OnClimbDone += Event_OnClimbDone;
        LevelManager.instance.OnHitByObstacle += Event_OnHitByObstacle;
    }

    private void Event_OnHitByObstacle(object sender, System.EventArgs e)
    {
        rock.GetComponent<CharacterJoint>().connectedBody = null;
    }

    private void Event_OnTab(object sender, LevelManager.OnTabEventArgs e)
    {
        if (!firstTab)
        {
            firstTab = true;
        }
        else rock.GetComponent<CharacterJoint>().connectedBody = null;

        rock = e.targetRock;
        isHanging = false;
    }

    private void Event_OnClimbDone(object sender, LevelManager.OnClimbDoneEventArgs e)
    {
        isHanging = true;
        GameObject hand = e.hand;
        //hangingPosition = rightHand.transform.position;
        rock.GetComponent<BoxCollider>().enabled = false;
        rock.GetComponent<CharacterJoint>().connectedBody = hand.GetComponent<Rigidbody>();
    }
}
