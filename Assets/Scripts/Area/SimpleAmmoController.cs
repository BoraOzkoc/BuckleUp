using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SimpleAmmoController : MonoBehaviour, ICollectable
{
    public void GetCollected(Transform parent)
    {
        transform.SetParent(parent);
        transform.DOLocalMove(Vector3.zero, 0.25f);
    }
}
