using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehaviour : MonoBehaviour
{
    [SerializeField] Transform _followTarget = null;
    [SerializeField] float lerpSpeed = 0.1f;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _followTarget.position, lerpSpeed);
    }
}
