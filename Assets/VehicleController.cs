using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public Type VehicleAmmoType;
    [SerializeField] private GameObject _modelPrefab;
    public enum Type
    {
        Small,
        Medium,
        Heavy
    }
    
}
