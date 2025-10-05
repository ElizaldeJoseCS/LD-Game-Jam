using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    int jumps = 0;
    float movementX;
    float movementY;
    [SerializeField] float speed = 6;

    [SerializeField] Rigidbody2D rb;

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

    }
}