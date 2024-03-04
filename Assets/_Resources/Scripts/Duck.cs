using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Duck : MonoBehaviour
{
    public bool isMove { get; set; }
    public List<GameObject> bullets = new List<GameObject>();
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

        animationTween = transform.DOMoveY(transform.position.y + 0.5f, 1f)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
        animationTween.Pause();
    }

    void Update()
    {
        if (GameManager.Instance.isPlay)
        {
            if (isMove)
                transform.position += transform.right * moveSpeedUnitsPerSecond * Time.deltaTime;
        }
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

        isMove = true;
        this.gameObject.SetActive(true);
        animationTween.Play();
    }

    public void Hunted()
    {
        if (isMove)
        {
            animationTween.Pause();
            isMove = false;
            HuntedAnimation();
            GameManager.Instance.AddScore(scoreAmount);
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
            Return();
        });
    }

    private void Return()
    {
        animationTween.Pause();
        this.gameObject.SetActive(false);
        transform.position = startPos;
        transform.rotation = startRot;
        GameManager.Instance.ActivateDucks();
        isMove = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((isFacingRight && other.gameObject.CompareTag("EndLeft")) || (!isFacingRight && other.gameObject.CompareTag("EndRight")))
        {
            HealthSystem.Instance.TakeDamage();
            Return();
        }
    }
}