using UnityEngine;

public class DepthSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;
    
    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer == null)
        {
            Debug.LogError("ManagedSprite requires a SpriteRenderer component!");
            enabled = false;
            return;
        }
        
        // Register with manager
        if (DepthManager.Instance != null)
        {
            DepthManager.Instance.RegisterSprite(spriteRenderer);
        }
        
        // Track initial position
        lastPosition = transform.position;
    }
    
    void Update()
    {
        // Check if position changed
        if (Vector3.Distance(transform.position, lastPosition) > 0.001f)
        {
            // Notify manager that this sprite moved
            if (DepthManager.Instance != null)
            {
                DepthManager.Instance.NotifySpriteMoved(spriteRenderer);
            }
            
            lastPosition = transform.position;
        }
    }
    
    void OnDestroy()
    {
        // Unregister from manager when destroyed
        if (DepthManager.Instance != null && spriteRenderer != null)
        {
            DepthManager.Instance.UnregisterSprite(spriteRenderer);
        }
    }
    
    // Optional: Manually update sorting (e.g., after teleporting)
    public void ManualUpdate()
    {
        if (DepthManager.Instance != null && spriteRenderer != null)
        {
            DepthManager.Instance.NotifySpriteMoved(spriteRenderer);
        }
    }
}
