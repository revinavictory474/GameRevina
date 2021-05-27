using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxScript : MonoBehaviour
{
    public Skybox skybox;
    [Range(0, 1)] public float blend;

    void Update()
    {
        skybox.material.SetFloat("_Blend", blend);
    }
}
