using UnityEngine;
using System.Collections;

public class PlayerCloneManager : MonoBehaviour
{
    public static PlayerCloneManager Instance { get; private set; }

    public Sprite playerSprite;

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

        SpriteRenderer sr = clone.AddComponent<SpriteRenderer>();
        SpriteRenderer originalSR = GetComponent<SpriteRenderer>();

        sr.sprite = playerSprite;
        sr.sortingLayerID = originalSR.sortingLayerID;
        sr.sortingOrder = originalSR.sortingOrder;
        sr.flipX = originalSR.flipX;
        sr.flipY = originalSR.flipY;
        sr.color = originalSR.color;

        clone.transform.position = transform.position;
        clone.transform.localScale = transform.localScale;

        hasSpawned = true;
        StartCoroutine(DestroyAfter(clone, 10f));
    }

    IEnumerator DestroyAfter(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
