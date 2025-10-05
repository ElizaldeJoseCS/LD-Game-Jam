using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    float movementX;
    float movementY;
    [SerializeField] float speed = 6;

    [SerializeField] Rigidbody2D rb;

    public Transform Aim;

    Vector2 lastDirection = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;
    }

    void FixedUpdate()
    {
        float XmoveDistance = movementX * speed * Time.fixedDeltaTime;
        float YmoveDistance = movementY * speed * Time.fixedDeltaTime;

        transform.position = new Vector2(transform.position.x + XmoveDistance, transform.position.y + YmoveDistance);

        Vector3 vector3 = Vector3.left * movementX + Vector3.down * movementY;

        if (movementX == 0 && movementY == 0)
        {
            vector3 = new Vector3(lastDirection.x, lastDirection.y, 0);
        }
        else if (Mathf.Abs(movementX) > Mathf.Abs(movementY))
        {
            movementY = 0;
            if (movementX > 0)
            {
                movementX = 1;
            }
            else
            {
                movementX = -1;
            }
        }
        else if (Mathf.Abs(movementX) <= Mathf.Abs(movementY))
        {
            movementX = 0;
            if (movementY > 0)
            {
                movementY = 1;
            }
            else
            {
                movementY = -1;
            }
        }

        lastDirection = new Vector2(vector3.x, vector3.y);
        Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
    }
}