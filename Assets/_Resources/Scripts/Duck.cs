using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Duck : MonoBehaviour
{
    public bool isMove { get; set; }
    [SerializeField] private bool isFacingRight;
    [SerializeField] private float moveSpeedUnitsPerSecond;
    private Vector3 startPos;
    private Quaternion startRot;
    private Tween animationTween;

    private void Start()
    {
        animationTween =
        transform.DOMoveY(transform.position.y + 0.5f, 1f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);

        animationTween.Pause();

        startPos = transform.position;
        startRot = transform.rotation;

        if (transform.rotation.y > 0)
            isFacingRight = true;
        else
            isFacingRight = false;
    }

    void Update()
    {
        if (isMove)
        {
            transform.position += transform.right * moveSpeedUnitsPerSecond * Time.deltaTime;
        }
    }

    public void Activate()
    {
        isMove = true;
        this.gameObject.SetActive(true);
        animationTween.Play();
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
        Return();
    }

    private void Return()
    {
        animationTween.Pause();
        isMove = false;
        transform.position = startPos;
        transform.rotation = startRot;
        GameManager.Instance.ActivateDucks();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((isFacingRight && other.gameObject.CompareTag("EndLeft")) || (!isFacingRight && other.gameObject.CompareTag("EndRight")))
        {
            Return();
        }
    }
}