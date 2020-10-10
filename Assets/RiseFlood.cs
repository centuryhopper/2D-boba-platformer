using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseFlood : MonoBehaviour
{
    [Tooltip("game units per second")]
    [SerializeField] float risingRate = .2f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * risingRate * Time.deltaTime);
    }
}
