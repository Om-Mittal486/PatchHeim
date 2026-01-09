using UnityEngine;
using System.Collections;

public class PlayerCloneManager : MonoBehaviour
{
    public static PlayerCloneManager Instance { get; private set; }

    [Header("Clone Setup")]
    public Sprite playerSprite;
    public RuntimeAnimatorController cloneAnimatorController;

    [Header("Audio Sources")]
    public AudioSource aiDeployedAudioSource;
    public AudioSource aiDisappearedAudioSource;

    private bool hasSpawned = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
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

        // Setup SpriteRenderer
        SpriteRenderer cloneSR = clone.AddComponent<SpriteRenderer>();
        SpriteRenderer originalSR = GetComponent<SpriteRenderer>();
        cloneSR.sprite = playerSprite;
        cloneSR.sortingLayerID = originalSR.sortingLayerID;
        cloneSR.sortingOrder = originalSR.sortingOrder;
        cloneSR.flipX = originalSR.flipX;

        // Setup Collider & Animator
        clone.AddComponent<PolygonCollider2D>();
        Animator cloneAnimator = clone.AddComponent<Animator>();
        cloneAnimator.runtimeAnimatorController = cloneAnimatorController;

        // Set transform
        clone.transform.position = transform.position;
        clone.transform.localScale = transform.localScale;

        hasSpawned = true;

        // Play spawn audio
        aiDeployedAudioSource?.Play();

        StartCoroutine(DestroyAfterDelay(clone, 10f));
    }

    IEnumerator DestroyAfterDelay(GameObject clone, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Play disappear audio
        aiDisappearedAudioSource?.Play();

        Destroy(clone);

        // Set "Found" bool on all Glitched objects
        foreach (GameObject glitchedObj in GameObject.FindGameObjectsWithTag("Glitched"))
        {
            Animator anim = glitchedObj.GetComponent<Animator>();
            if (anim != null)
                anim.SetBool("Found", true);
        }
    }
}
