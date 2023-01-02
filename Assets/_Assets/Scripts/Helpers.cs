using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static AudioClip GetRandomAudioClipFromCollection(AudioClip[] _collection)
    {
        return _collection[Random.Range(0, _collection.Length)];
    }
}