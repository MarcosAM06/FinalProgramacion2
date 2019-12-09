using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehaviour : MonoBehaviour
{
    [SerializeField] Transform _followTarget;
    [SerializeField] float lerpSpeed;


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _followTarget.position, lerpSpeed);
    }
}
