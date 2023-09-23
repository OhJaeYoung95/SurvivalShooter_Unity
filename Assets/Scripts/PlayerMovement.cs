using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Animator playerAnim;
    public float speed = 5f;

    Vector3 dir;

    public LayerMask layerMask;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerAnim.SetBool("IsAlive", true);
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Roatate();
        MoveAnim();
    }

    private void Move()
    {
        dir = new Vector3(PlayerInput.Instance.MoveX, 0f, PlayerInput.Instance.MoveZ);
        if (dir.magnitude > 1f)
            dir.Normalize();
        Debug.Log(dir.magnitude);

        Vector3 movePos = playerRigidbody.position;
        movePos += dir * speed * Time.deltaTime;
        playerRigidbody.MovePosition(movePos);
    }

    private void MoveAnim()
    {
        playerAnim.SetFloat("Speed", dir.magnitude);
    }

    private void Roatate()
    {
        Ray ray = PlayerInput.Instance.Look;
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask))
        {
            Vector3 lookPoint = hitInfo.point;
            lookPoint.y = transform.position.y;
            var look = lookPoint - playerRigidbody.position;
            playerRigidbody.MoveRotation(Quaternion.LookRotation(look.normalized));
        }
    }
}
