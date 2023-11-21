using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    [SerializeField] private List<Transform> containerList = new List<Transform>();

    public Vector3 GetContainerPos(int index)
    {
        return containerList[index].position;
    }

    public void TriggerBounceAnimation(int index)
    {
        Transform container = containerList[index];
        container.localScale = Vector3.one;
        container.DOScale(Vector3.one * 1.5f, 0.3f).SetEase(Ease.InBounce).SetLoops(2, LoopType.Yoyo);
    }
}