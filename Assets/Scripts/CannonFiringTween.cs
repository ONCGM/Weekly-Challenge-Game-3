using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CannonFiringTween : MonoBehaviour
{
    [SerializeField] private float movementDistance = 1f;
    [SerializeField] private float animationSpeed = 0.5f;

    public IEnumerator PlayTween() {
        transform.DOLocalMoveZ(transform.localPosition.z - movementDistance, animationSpeed / 3f, false);
        yield return new WaitForSeconds(animationSpeed / 3f);
        ReverseTween();
    }

    private void ReverseTween() {
        transform.DOLocalMoveZ(transform.localPosition.z + movementDistance, animationSpeed * 2f, false);
    }
}
