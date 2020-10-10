using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    private int points = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

        // no need to cache bc this gameobject will be destroyed anyways
        FindObjectOfType<PersistData>().AddPointsToPlayerScore(points);
        Destroy(gameObject);
    }
}
