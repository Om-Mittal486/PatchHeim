using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool isClone = false;


    void Awake()
    {
        // Singleton Setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isClone) return; // Don’t move if it’s a clone

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}
