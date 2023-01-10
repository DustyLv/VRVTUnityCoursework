using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static AudioClip GetRandomAudioClipFromCollection(AudioClip[] collection)
    {
        return collection[Random.Range(0, collection.Length)];
    }

    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
