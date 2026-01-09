using UnityEngine;

public class FixableObject : MonoBehaviour
{
    private bool playerInRange = false;
    private float holdTime = 0f;
    private float requiredHoldTime = 3f;
    private bool isFixed = false;

    private Animator animator;
    public AudioSource fixSuccessAudio; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerInRange && !isFixed && animator.GetBool("Found"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                holdTime += Time.deltaTime;
                if (holdTime >= requiredHoldTime)
                {
                    animator.SetBool("Fixed", true);
                    isFixed = true;

                    if (fixSuccessAudio != null)
                        fixSuccessAudio.Play();
                }
            }
            else
            {
                holdTime = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
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
