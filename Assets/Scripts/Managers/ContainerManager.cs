using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public static ContainerManager Instance;
    [SerializeField] private List<ContainerController> containerList = new List<ContainerController>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying duplicate ContainerManager Instance");
            Destroy(this);
        }
        else
        {
            //Debug.Log("Setting ContainerManager Instance");
            Instance = this;
        }
    }
    
    public ContainerController GetContainer(int index)
    {
        return containerList[index];
    }
}