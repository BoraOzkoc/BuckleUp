using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    void GetCollected(ContainerController containerController);
    void GetTransferred(Transform parent);
}
