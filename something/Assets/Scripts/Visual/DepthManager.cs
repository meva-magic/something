using UnityEngine;
using System.Collections.Generic;

public class DepthManager : MonoBehaviour
{
    // Singleton pattern for easy access
    public static DepthManager Instance { get; private set; }
    
    // List of all registered sprites
    private List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    
    // Optimization: Track moved sprites to only sort what changed
    private List<SpriteRenderer> movedSprites = new List<SpriteRenderer>();
    
    // Optimization: Only sort every N frames
    [SerializeField] private int sortInterval = 2; // Sort every 2nd frame
    private int frameCount = 0;
    
    void Awake()
    {
        // Set up singleton
        if (Instance == null)
        {
            Instance = this;
            // Optional: Don't destroy on scene load
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Public method to register a sprite
    public void RegisterSprite(SpriteRenderer sprite)
    {
        if (!sprites.Contains(sprite))
        {
            sprites.Add(sprite);
            // Initial sort order
            UpdateSpriteOrder(sprite);
        }
    }
    
    // Public method to unregister a sprite (when destroyed)
    public void UnregisterSprite(SpriteRenderer sprite)
    {
        if (sprites.Contains(sprite))
        {
            sprites.Remove(sprite);
            movedSprites.Remove(sprite);
        }
    }
    
    // Call this when a sprite moves
    public void NotifySpriteMoved(SpriteRenderer sprite)
    {
        if (sprites.Contains(sprite) && !movedSprites.Contains(sprite))
        {
            movedSprites.Add(sprite);
        }
    }
    
    void LateUpdate()
    {
        frameCount++;
        
        // Only sort at intervals for performance
        if (frameCount % sortInterval != 0 && movedSprites.Count < sprites.Count / 4)
            return;
        
        SortAllSprites();
        frameCount = 0;
    }
    
    void SortAllSprites()
    {
        if (movedSprites.Count == 0 && sprites.Count > 0)
            return;
        
        // OPTION A: Sort only moved sprites (Most Efficient)
        if (movedSprites.Count < sprites.Count / 2)
        {
            foreach (SpriteRenderer sprite in movedSprites)
            {
                UpdateSpriteOrder(sprite);
            }
        }
        // OPTION B: Sort all sprites (if many moved)
        else
        {
            // Sort all sprites by Y position (higher Y = rendered behind)
            sprites.Sort((a, b) => 
            {
                float diff = b.transform.position.y - a.transform.position.y;
                if (Mathf.Abs(diff) < 0.001f) return 0;
                return diff > 0 ? 1 : -1;
            });
            
            // Apply new sorting order
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].sortingOrder = i;
            }
        }
        
        // Clear moved sprites list
        movedSprites.Clear();
    }
    
    void UpdateSpriteOrder(SpriteRenderer sprite)
    {
        // Simple Y-based sorting
        // Negative because higher Y should render behind
        sprite.sortingOrder = -(int)(sprite.transform.position.y * 100f);
    }
    
    // Optional: Force a sort (e.g., when adding many sprites at once)
    public void ForceSort()
    {
        movedSprites.Clear();
        movedSprites.AddRange(sprites);
        SortAllSprites();
    }
    
    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
