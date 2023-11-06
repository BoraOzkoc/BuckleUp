using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCreationArea : AreaController
{
    public override void TriggerArea()
    {
        base.TriggerArea();
        AreaTriggered();
    }

    public void AreaTriggered()
    {
        Debug.Log("child executed");

    }
}
