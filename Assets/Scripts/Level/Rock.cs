using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] bool isFinalRock;

    public bool IsFinalRock()
    {
        return isFinalRock;
    }
}
