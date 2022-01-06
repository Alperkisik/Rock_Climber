using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerr : MonoBehaviour
{
    [SerializeField] List<GameObject> Levels;

    public static GameManagerr instance;

    public event EventHandler OnLevelFailed;
    public event EventHandler OnLevelSuccess;
    public event EventHandler OnNextLevel;
    public event EventHandler OnRetryLevel;

    int levelIndex = 0;
    GameObject level;
    [HideInInspector] public bool IslevelFinished;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        IslevelFinished = false;
        InstantiateLevel(levelIndex);
    }

    public void LevelSuccess()
    {
        IslevelFinished = true;
        OnLevelSuccess?.Invoke(this, EventArgs.Empty);
    }

    public void LevelFailed()
    {
        IslevelFinished = true;
        OnLevelFailed?.Invoke(this, EventArgs.Empty);
    }

    private void DestroyCurrentLevel()
    {
        Destroy(level);
    }

    private void InstantiateLevel(int levelIndex)
    {
        IslevelFinished = false;
        level = Instantiate(Levels[levelIndex], Vector3.zero, Quaternion.identity);
    }

    public void NextLevel()
    {
        levelIndex++;
        if (levelIndex >= Levels.Count) levelIndex = 0;
        DestroyCurrentLevel();
        InstantiateLevel(levelIndex);
    }

    public void RetryLevel()
    {
        DestroyCurrentLevel();
        InstantiateLevel(levelIndex);
    }
}
