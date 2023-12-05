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

    public void MoveTo(AmmoCollector ammoCollector)
    {
        Vector3 startPos = transform.position;
        float x = Random.Range(0f, 4f);
        float y = Random.Range(1f, 3f);
        float z = Random.Range(0f, 4f);
        Vector3 targetPos = new Vector3(startPos.x + x, startPos.y + y, startPos.z + z);

        transform.DOMove(targetPos, 1).OnComplete(() =>
        {
            transform.DOJump(ammoCollector.transform.position, 2, 1, 0.3f).OnComplete(GetPulledToPool);
        });
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