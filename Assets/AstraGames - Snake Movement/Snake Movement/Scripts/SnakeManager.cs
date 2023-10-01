using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] float speed = 280;
    [SerializeField] float turnSpeed = 180;


    

   

    void FixedUpdate()
    {
        CharacterMovement();
    }

 
    private void CharacterMovement()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed * Time.deltaTime;
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal")));
        }
    }
}