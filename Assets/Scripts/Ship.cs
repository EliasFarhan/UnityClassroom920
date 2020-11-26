using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Rigidbody2D body_;
    private Vector3 moveDir_ = new Vector2();

    private const float shipSpeed = 2.0f;
    private const float angularSpeed = 45.0f;

    private Transform transform_;

    private float angle_ = 0.0f; // up is 0 degree
    // Start is called before the first frame update
    void Start()
    {
        transform_ = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir_ = Quaternion.AngleAxis(angle_, Vector3.forward) * Vector3.up;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab, transform_.position, Quaternion.identity);
            bullet.Direction = moveDir_;
        }
        

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle_ += angularSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle_ -= angularSpeed * Time.deltaTime;
        }

        var moveIntensity = 0.0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveIntensity += 1.0f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveIntensity -= 1.0f;
        }

        moveDir_ = moveDir_ * moveIntensity;

        var eulerAngles = transform_.eulerAngles;
        eulerAngles.z = angle_;
        transform_.eulerAngles = eulerAngles;
    }

    private void FixedUpdate()
    {
        body_.velocity = moveDir_ * shipSpeed;
    }
}
