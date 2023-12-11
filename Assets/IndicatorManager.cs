using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private List<Transform> objectList = new List<Transform>();
    [SerializeField] private List<Image> imageList = new List<Image>();
    
}
