using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSystem : MonoBehaviour
{
    public static CollectibleSystem Instance;
    private Dictionary<string, int> collectibles = new();
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Add(string id, int amount)
    {
        if (!collectibles.ContainsKey(id))
        {
            collectibles[id] = 0;
        }
        collectibles[id] += amount;
        int gold = Get("gold");
        Debug.Log("Gold Amount " + gold);
    }

    public int Get(string id)
    {
        return collectibles.TryGetValue(id, out int value) ? value : 0;
    }
}
