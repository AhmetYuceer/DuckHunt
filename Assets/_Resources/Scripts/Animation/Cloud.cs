using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.position;
        transform.DOMoveY(transform.position.y - 0.5f, 0.5f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }

}