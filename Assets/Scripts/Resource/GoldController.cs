using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GoldController : MonoBehaviour
{
    [SerializeField] private GameObject _mesh;

    public void Activate(Vector3 pos)
    {
        transform.position = pos;
        _mesh.SetActive(true);
    }

    public void Deactivate()
    {
        _mesh.SetActive(false);
        TeleportTo(transform.position);
    }

    public void TransferGold(Vector3 pos)
    {
        transform.DOJump(pos, 2, 1, 0.25f);
    }
    public void MoveTo(Vector3 pos)
    {
        transform.DOJump(pos, 2, 1, 0.25f).OnComplete(GetPulledToPool);
    }

    private void GetPulledToPool()
    {
        ResourceManager.Instance.AddGold();
        ResourceManager.Instance.GetResourcePoolController().PushToList(this);
    }
    public void TeleportTo(Vector3 pos)
    {
        transform.position = pos;
    }
}