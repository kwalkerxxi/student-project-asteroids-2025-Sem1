using UnityEngine;
using DG.Tweening;

public class PlayerCodedAnimation : MonoBehaviour
{
    Rigidbody cachedRigidbody;
    bool isMoving = false;
    float startPositionY = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cachedRigidbody = GetComponent<Rigidbody>();
        startPositionY = transform.position.y;
        transform.DOMoveY(transform.position.y - 0.2f, 0.3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
