using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraBounds : MonoBehaviour
{
    [SerializeField] private Vector3 minBounds;
    [SerializeField] private Vector3 maxBounds;

    // Update is called once per frame
    void Update()
    {
        float restrictedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
              restrictedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y),
              restrictedZ = Mathf.Clamp(transform.position.z, minBounds.z, maxBounds.z);

        transform.position = new Vector3(restrictedX, restrictedY, restrictedZ);
    }
}
