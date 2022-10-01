using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD51
{
    public class Pickup : MonoBehaviour
    {
        public Spawner spawner;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player") 
            {
                spawner.Despawn(this.gameObject);
                
            }
        }
    }
}
