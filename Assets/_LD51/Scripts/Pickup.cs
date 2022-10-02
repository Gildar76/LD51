using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD51
{
    public class Pickup : MonoBehaviour,ISpawnable
    {
        GameManager manager;
        private Spawner spawner;

        public Spawner Spawner { get => spawner; set => spawner = value; }

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
                    
                    Spawner.Despawn(this.gameObject);
                }

            }
        }

    }
}
