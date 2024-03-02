using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Duck : MonoBehaviour
{
    public List<GameObject> bullets = new List<GameObject>();
    public bool isMove { get; set; }
    [SerializeField] private bool isFacingRight;
    [SerializeField] private float moveSpeedUnitsPerSecond;
    [SerializeField] private int scoreAmount;
    private Vector3 startPos;
    private Quaternion startRot;
    private Tween animationTween;

    private void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;

        if (transform.rotation.y > 0)
            isFacingRight = true;
        else
            isFacingRight = false;
    }

    void Update()
    {
        if (isMove && GameManager.Instance.isPlay)
            transform.position += transform.right * moveSpeedUnitsPerSecond * Time.deltaTime;
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeedUnitsPerSecond = newSpeed;
    }

    public void Activate()
    {
        foreach (var bullet in bullets)
        {
            Destroy(bullet);
        }
        bullets.Clear();

        animationTween = transform.DOMoveY(transform.position.y + 0.5f, 1f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
        animationTween.Play();
        isMove = true;
        this.gameObject.SetActive(true);
    }

    public void Hunted()
    {
        if (isMove)
        {
            isMove = false;
            animationTween.Pause();
            GameManager.Instance.AddScore(scoreAmount);
            HuntedAnimation();
            GameManager.Instance.SpeedUpDucks();
        }
    }

    private void HuntedAnimation()
    {
        transform.DOMoveY(transform.position.y - 1f, 1f)
        .SetRelative(true)
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            this.gameObject.SetActive(false);
            Return();
        });
    }

    private void Return()
    {
        isMove = false;
        animationTween.Pause();
        transform.position = startPos;
        transform.rotation = startRot;
        GameManager.Instance.ActivateDucks();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((isFacingRight && other.gameObject.CompareTag("EndLeft")) || (!isFacingRight && other.gameObject.CompareTag("EndRight")))
            Return();
    }
}