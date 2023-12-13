using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPoolManager : MonoBehaviour
{
    public static EffectPoolManager Instance;
    private List<GameObject> activatedList = new List<GameObject>();
    private List<GameObject> deactivatedList = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying duplicate EffectPoolManager Instance");
            Destroy(this);
        }
        else
        {
            //Debug.Log("Setting EffectPoolManager Instance");
            Instance = this;
        }
    }
}
