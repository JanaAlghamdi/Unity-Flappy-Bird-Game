using UnityEngine;
using System;
using System.Collections.Generic;

public class UISfxManagerScript : MonoBehaviour
{
    [Serializable]
    public struct NamedClip
    {
        public string key;      
        public AudioClip clip;  
        [Range(0f, 1f)] public float volume;
    }

    public static UISfxManagerScript Instance;

    [Header("Output")]
    public AudioSource output;       

    [Header("Clips")]
    public NamedClip[] clips;

    private Dictionary<string, NamedClip> map;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        map = new Dictionary<string, NamedClip>(StringComparer.OrdinalIgnoreCase);
        foreach (var c in clips)
            if (!string.IsNullOrEmpty(c.key) && c.clip != null)
                map[c.key] = c;

        if (output != null)
        {
            output.playOnAwake = false;
            output.loop = false;
            output.spatialBlend = 0f;
        }
    }

    public void PlayByKey(string key)
    {
        if (output == null || string.IsNullOrEmpty(key)) return;
        if (map != null && map.TryGetValue(key, out var nc))
            output.PlayOneShot(nc.clip, nc.volume <= 0f ? 1f : nc.volume);
    }
}

