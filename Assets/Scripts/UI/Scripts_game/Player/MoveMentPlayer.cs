﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMentPlayer : MonoBehaviour
{
    public float moveSpeed = 3;//tôc độ di chuyển 
    [HideInInspector] public Vector3 dir;
    float hzInput, vInput;//kiểu di chuyển 
    CharacterController controller;

    [SerializeField] float groundOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetDirectionAndMove();
        Gravity();
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;

        controller.Move(dir * moveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask))
        {
            return true;
        }
        else
            return false;
    }

    void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spherePos, controller.radius - 0.05f);
    }*/
}
