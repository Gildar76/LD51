using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD51
{
    public class Boat : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] float smoothing = 5f;
        [SerializeField] Camera mainCamera;
        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            Vector3 playerPos = player.transform.position;
            Vector3 cameraPos = mainCamera.transform.position;
            Vector3 translation = (playerPos - transform.position);// * smoothing * Time.deltaTime; 
            translation.z = 0;
            translation.y = 0;
            transform.position += translation / (smoothing / Time.deltaTime);

        }
    }
}
