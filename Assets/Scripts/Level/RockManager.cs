using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    [SerializeField] Transform _rocks;
    List<GameObject> rocks;
    GameObject targetRock;

    void Start()
    {
        Listener();

        rocks = new List<GameObject>();

        for (int i = 0; i < _rocks.childCount; i++)
        {
            rocks.Add(_rocks.GetChild(i).gameObject);
        }
    }
    private void Listener()
    {
        LevelManager.instance.OnTab += Event_OnTab;
        LevelManager.instance.OnClimbDone += Event_OnClimbDone;
    }

    private void Event_OnTab(object sender, LevelManager.OnTabEventArgs e)
    {
        targetRock = e.targetRock;
        foreach (GameObject rock in rocks)
        {
            rock.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Event_OnClimbDone(object sender, System.EventArgs e)
    {
        foreach (GameObject rock in rocks)
        {
            rock.GetComponent<BoxCollider>().enabled = true;
        }
        targetRock.GetComponent<BoxCollider>().enabled = false;
    }
}
