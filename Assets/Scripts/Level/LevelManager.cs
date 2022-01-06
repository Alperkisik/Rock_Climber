using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform _rocks;
    public static LevelManager instance;

    public event EventHandler<OnTabEventArgs> OnTab;
    public class OnTabEventArgs : EventArgs
    {
        public GameObject targetRock;
    }

    public event EventHandler<OnClimbDoneEventArgs> OnClimbDone;
    public class OnClimbDoneEventArgs : EventArgs
    {
        public GameObject hand;
    }

    public event EventHandler OnHitByObstacle;

    List<GameObject> rocks;
    private int rockIndex = 0;
    bool Isclimbing;

    private void Awake()
    {
        instance = this;
        Isclimbing = false;
        rocks = new List<GameObject>();

        for (int i = 0; i < _rocks.childCount; i++)
        {
            rocks.Add(_rocks.GetChild(i).gameObject);
        }
    }

    private void Start()
    {
        rockIndex = 0;
    }
    GameObject rock;
    public void Event_Tab(GameObject targetRock)
    {
        if (Isclimbing) return;

        rock = targetRock;

        for (int i = 0; i < rocks.Count; i++)
        {
            if(rocks[i] == targetRock)
            {
                rockIndex = i;
                break;
            }
        }

        /*if (rockIndex < rocks.Count) rock = rocks[rockIndex];
        else rock = rocks[rockIndex-1];*/

        Isclimbing = true;
        OnTab?.Invoke(this, new OnTabEventArgs { targetRock = rock });
    }

    public void Event_OnClimbDone(GameObject climbingHand)
    {
        Isclimbing = false;

        if(rock.GetComponent<Rock>().IsFinalRock()) LevelComplited();
        //if (rockIndex == rocks.Count-1) LevelComplited();

        OnClimbDone?.Invoke(this, new OnClimbDoneEventArgs { hand = climbingHand });
    }

    public void Event_OnHitByObstacle()
    {
        LevelFailed();
        OnHitByObstacle?.Invoke(this, EventArgs.Empty);
    }

    private void LevelComplited()
    {
        GameManagerr.instance.LevelSuccess();
    }

    private void LevelFailed()
    {
        GameManagerr.instance.LevelFailed();
    }
}
