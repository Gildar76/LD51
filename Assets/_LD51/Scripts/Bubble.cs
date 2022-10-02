using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD51
{
    public class Bubble : MonoBehaviour, ISpawnable
    {
        private Spawner spawner;
        [SerializeField] float speed;
        GameManager manager;

        public Spawner Spawner { get => spawner; set => spawner = value; }

        private void Start()
        {
            manager = FindObjectOfType<GameManager>();
        }
        private void Update()
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            if (transform.position.y > 4)
            {
                Spawner.Despawn(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Spawner.Despawn(this.gameObject);
            manager.AddOxygen(10);

        }


    }
}
