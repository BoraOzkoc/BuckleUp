using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;

public class AmmoController : MonoBehaviour, ICollectable
{
    public Type AmmoType;
    [SerializeField] private List<GameObject> _modelList = new List<GameObject>();
    private AmmoAnimationController _ammoAnimationController;

    public enum Type
    {
        Small,
        Medium,
        Heavy
    }

    public void PrepareAmmo(int index)
    {
        AmmoType = (Type)index;
        ActivateModel();
        PlaySpawnAnimation();
    }

    public void PlaySpawnAnimation()
    {
        Vector3 startScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(startScale, 0.40f).SetEase(Ease.OutBounce);
    }

    private void Awake()
    {
        _ammoAnimationController = GetComponent<AmmoAnimationController>();
    }

    private void ActivateModel()
    {
        for (int i = 0; i < _modelList.Count; i++)
        {
            int index = (int)AmmoType;
            if (i != index)
            {
                _modelList[i].SetActive(false);
            }
            else _modelList[i].SetActive(true);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void GetCollected(ContainerController containerController)
    {
        containerController.AddAmmo(this);
        transform.SetParent(containerController.transform);
        transform.DOLocalJump(Vector3.zero, 6, 1, 0.45f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            TriggerContainer(containerController);
            Hide();
        });
    }

    private void TriggerContainer(ContainerController containerController)
    {
        containerController.PlayBounceAnim();
    }

    public void GetTransferredToVehicle(Transform parent, VehicleController vehicleController, bool isLastAmmo)
    {
        Show();
        transform.SetParent(parent);

        transform.DOLocalJump(Vector3.zero, 7, 1, 0.55f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            vehicleController.UpdateText();
            if (isLastAmmo) vehicleController.CheckOrderAmount();
            DestroyProtocol();
        });
    }

    private void DestroyProtocol()
    {
        ResourceManager.Instance.GetResourcePoolController().PullFromList(transform);

        transform.DOKill();
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        ActivateModel();
    }
}