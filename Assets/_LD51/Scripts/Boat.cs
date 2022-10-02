using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD51
{
    public class Boat : MonoBehaviour
    {
        [SerializeField] GameObject player;
        public bool playerInBoat = false;
        [SerializeField] float smoothing = 5f;
        [SerializeField] Camera mainCamera;
        GameManager gameManager;
        public GameObject spawnPoint;
        private void Start()
        {
            mainCamera = Camera.main;
            gameManager = FindObjectOfType<GameManager>();
            player = gameManager.player;
            playerInBoat = true;
            player.SetActive(false);
            gameManager.CanExitBoat();
            gameManager.EmptyInventory();

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

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                playerInBoat = false;
                gameManager.CanNoLongerEnterBoat();
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                playerInBoat = true;
                gameManager.AddOxygen(10);
                //Diver diver = player.GetComponent<Diver>();
                
                gameManager.CanEnterBoat();


            }
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && playerInBoat && player.activeInHierarchy)
            {
                player.SetActive(false);
                gameManager.CanExitBoat();
                gameManager.EmptyInventory();
            } else if (Input.GetKeyDown(KeyCode.Space) && !player.activeInHierarchy == true )
            {
                player.SetActive(true);
                player.transform.position = spawnPoint.transform.position;
                gameManager.CanNoLongerEnterBoat();
            }
        }
    }
}
