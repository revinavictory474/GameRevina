﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObj = hit.transform.gameObject;
                ReactiveTarget target = hitObj.GetComponent<ReactiveTarget>();
                if (target != null)
                { 
                    target.ReactToHit();
                    Messenger.Broadcast(GameEvent.ENEMY_HIT);
                }
                else StartCoroutine(SphereIndicator(hit.point));
            }
        }
    }

   
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
