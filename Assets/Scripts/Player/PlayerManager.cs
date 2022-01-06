using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject playerRig;
    Collider[] ragdollColliders;

    bool firstTab;

    private void Awake()
    {
        ragdollColliders = playerRig.GetComponentsInChildren<Collider>(true);
        firstTab = false;
        DoRagdoll(false);
        Listener();
    }

    private void DoRagdoll(bool isRagDoll)
    {
        GetComponent<Animator>().enabled = !isRagDoll;
        foreach (var col in ragdollColliders)
        {
            col.enabled = isRagDoll;
            col.gameObject.GetComponent<Rigidbody>().isKinematic = !isRagDoll;
            col.gameObject.GetComponent<Rigidbody>().useGravity = isRagDoll;
        }
    }

    private void Listener()
    {
        LevelManager.instance.OnTab += Event_OnTab;
        LevelManager.instance.OnClimbDone += Event_OnClimbDone;
    }

    private void Event_OnClimbDone(object sender, System.EventArgs e)
    {
        //DoRagdoll(false);
    }

    private void Event_OnTab(object sender, LevelManager.OnTabEventArgs e)
    {
        if (!firstTab)
        {
            firstTab = true;
            DoRagdoll(true);
        }
    }
}
