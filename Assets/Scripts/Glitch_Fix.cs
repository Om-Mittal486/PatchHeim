using UnityEngine;

public class FixableObject : MonoBehaviour
{
    private bool playerInRange = false;
    private float holdTime = 0f;
    private float requiredHoldTime = 3f;
    private bool isFixed = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Only allow fixing if 'Found' is true and not already fixed
        if (playerInRange && !isFixed && animator.GetBool("Found"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                holdTime += Time.deltaTime;

                if (holdTime >= requiredHoldTime)
                {
                    animator.SetBool("Fixed", true);
                    isFixed = true;
                }
            }
            else
            {
                holdTime = 0f; // reset if E is released early
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            holdTime = 0f;
        }
    }
}
