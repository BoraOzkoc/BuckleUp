using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    void GetCollected(Transform parent);
    void GetTransferred(Transform parent);
}
