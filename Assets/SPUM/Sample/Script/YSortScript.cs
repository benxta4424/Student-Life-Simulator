using UnityEngine;

public class YSortScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the sprite renderer component from this object
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (spriteRenderer != null)
        {
            // Set the sortingOrder based on the character's Y position.
            // Lower Y position (closer to bottom) results in a higher sortingOrder (drawn in front).
            spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
        }
    }
}