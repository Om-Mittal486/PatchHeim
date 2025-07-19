using UnityEngine;
using System.Collections;

public class PlayerCloneManager : MonoBehaviour
{
    public static PlayerCloneManager Instance { get; private set; }

    public Sprite playerSprite;

    public RuntimeAnimatorController cloneAnimatorController;

    private bool hasSpawned = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !hasSpawned)
        {
            SpawnClone();
        }
    }

    

    void SpawnClone()
    {
        GameObject clone = new GameObject("PlayerClone");

        // Add SpriteRenderer
        SpriteRenderer sr = clone.AddComponent<SpriteRenderer>();
        SpriteRenderer originalSR = GetComponent<SpriteRenderer>();

        sr.sprite = playerSprite;
        sr.sortingLayerID = originalSR.sortingLayerID;
        sr.sortingOrder = originalSR.sortingOrder;
        sr.flipX = originalSR.flipX;
        sr.flipY = originalSR.flipY;
        sr.color = originalSR.color;

        // Add PolygonCollider2D
        PolygonCollider2D polygonCollider = clone.AddComponent<PolygonCollider2D>();

        // Add Animator
        Animator animator = clone.AddComponent<Animator>();
        animator.runtimeAnimatorController = cloneAnimatorController;

        // Position and scale
        clone.transform.position = transform.position;
        clone.transform.localScale = transform.localScale;

        hasSpawned = true;

        // Optional: Destroy after animation time
        StartCoroutine(DestroyAfter(clone, 10f));
    }


    IEnumerator DestroyAfter(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
