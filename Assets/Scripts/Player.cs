using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D), typeof (BoxCollider2D))]
public class Player : MonoBehaviour
{
    public float Speed = 1.0f;

    private Camera MainCamera;

    Vector2 Movement;
    Rigidbody2D Rigidbody;

    Transform WeaponTransform;

    private void Start()
    {
        MainCamera = Camera.main;
        Movement = new Vector2();
        Rigidbody = GetComponent<Rigidbody2D>();

        WeaponTransform = transform.Find("Weapon");
    }

    // Update is called once per frame
    void Update ()
    {
        HandleMovement(Time.deltaTime);

        UpdateWeaponRotation();
	}

    void HandleMovement(float DeltaTime)
    {
        Movement.x = Input.GetAxis("Horizontal") * Speed * DeltaTime;
        Movement.y = Input.GetAxis("Vertical") * Speed * DeltaTime;

        transform.Translate(Movement.x, Movement.y, 0);

        MainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, MainCamera.transform.position.z);
    }

    void UpdateWeaponRotation()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0.0f;

        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 diff = mousePosition - playerPosition;

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        WeaponTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}