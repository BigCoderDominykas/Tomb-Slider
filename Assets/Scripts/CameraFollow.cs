using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(0f, 0.999f)] public float smoothness;
    [Range(0f, 0.999f)] public float transitionSmoothness;
    public Transform transition;
    public float rotateSpeed;
    public float targetSize;
    float targetSizeCopy = 0;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, 1 - smoothness);
        transition.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        transition.localScale = Vector3.Lerp(transition.localScale, Vector3.one * targetSizeCopy, 1 - transitionSmoothness);
    }
}
