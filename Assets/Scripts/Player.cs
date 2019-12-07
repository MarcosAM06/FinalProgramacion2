using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] FixedJoystick _joystick;
    [SerializeField] float _movementSpeed;
    [SerializeField] Transform _worldForward;

    [SerializeField] TMP_Text debugText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            MovePlayer(_joystick.Horizontal, _joystick.Vertical);
    }

    public void RotatePlayer(float HAxis)
    {
        // Test x Transform
        //transform.position += transform.forward * MovementSpeed * HAxis * Time.deltaTime;
    }

    public void MovePlayer(float HAxis, float VAxis)
    {
        //Dirección de la cámara.
        debugText.text = string.Format("Valores del axis son Horizontal: {0} y Vertical: {1}", HAxis, VAxis);

        Vector3 dir = _worldForward.forward * VAxis + _worldForward.right * HAxis;
        transform.forward = dir;
        transform.position += dir * _movementSpeed * Time.deltaTime;
    }
}
