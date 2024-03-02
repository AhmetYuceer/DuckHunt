using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class water : MonoBehaviour
{
    [SerializeField] private bool isFacingRight;

    void Start()
    {
        if (isFacingRight)
        {
            transform.DOMoveX(transform.position.x - 1f, 1.5f)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            transform.DOMoveX(transform.position.x + 1f, 1.5f)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}