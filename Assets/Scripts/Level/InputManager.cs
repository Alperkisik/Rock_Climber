using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (GameManagerr.instance.IslevelFinished) return;

        Tab();
    }

    private void Tab()
    {
        //touch input
        if (Input.touchCount > 0)
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hitInfo);
            if (hit)
            {
                if (hitInfo.transform.gameObject.tag == "Rock")
                {
                    LevelManager.instance.Event_Tab(hitInfo.transform.gameObject);
                }
            }
        }

        //mouse input
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.transform.gameObject.tag == "Rock")
                {
                    LevelManager.instance.Event_Tab(hitInfo.transform.gameObject);
                }
            }
        }
    }
}
