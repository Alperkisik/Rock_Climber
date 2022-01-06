using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] GameObject successed;
    [SerializeField] GameObject failed;

    void Start()
    {
        Listener();
    }

    private void Listener()
    {
        GameManagerr.instance.OnLevelFailed += Event_OnLevelFailed;
        GameManagerr.instance.OnLevelSuccess += Event_OnLevelSuccess;
    }

    private void Event_OnLevelSuccess(object sender, System.EventArgs e)
    {
        SetActive_LevelEndUI(true, false);
    }

    private void Event_OnLevelFailed(object sender, System.EventArgs e)
    {
        SetActive_LevelEndUI(false, true);
    }

    public void NextLevel()
    {
        SetActive_LevelEndUI(false, false);
        GameManagerr.instance.NextLevel();
    }

    public void RetryLevel()
    {
        SetActive_LevelEndUI(false, false);
        GameManagerr.instance.RetryLevel();
    }

    private void SetActive_LevelEndUI(bool success,bool fail)
    {
        successed.SetActive(success);
        failed.SetActive(fail);
    }
}
