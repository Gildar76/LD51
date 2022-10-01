using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD51
{
    public class Pickup : MonoBehaviour
    {
        GameManager manager;
        public Spawner spawner;

        private void Start()
        {
            manager = FindObjectOfType<GameManager>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                
                if (manager.AddToInventory(1))
                {
                    Debug.Log("Adding to inventory");
                    spawner.Despawn(this.gameObject);
                }

            }
        }

    }
}
