using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveY(transform.position.y - 0.2f, 2f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }
}