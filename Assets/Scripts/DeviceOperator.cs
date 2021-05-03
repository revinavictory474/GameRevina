using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{

    public float radius = 1.5f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hitCollider in colliders)
            {
                Vector3 direction = hitCollider.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, direction) > 0.5f)
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

}
