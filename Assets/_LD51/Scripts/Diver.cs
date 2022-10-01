using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GildarGaming.LD51
{
    public class Diver : MonoBehaviour
    {

        [SerializeField] float waterLevel = 6.5f;
        Rigidbody rb;
        Vector3 input;
        Vector3 force = Vector3.zero;
        [SerializeField] float playerForce = 10f;
        public void Reset()
        {
        }
        
        public void Awake()
        {
        }
        
        public void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        

        private void FixedUpdate()
        {
            
            ApplyWaterForce();
            ApplyInputForce();
            if (transform.position.y >= waterLevel)
            {
                force.y = 0f;
            }
            rb.AddForce(force);
        }

        private void ApplyInputForce()
        {
            force.x += input.x;
            force.y += input.y;
        }

        private void ApplyWaterForce()
        {
            Vector3 waterForce = new Vector3(0, 8, 0);
            if (transform.position.y < waterLevel)
            {
                
                waterForce = rb.velocity * 0.1f;
                waterForce.y = 9.5f;
                force = waterForce;
                rb.drag = 2f;

            }
            else
            {
                rb.drag = 25f;
                waterForce.y = -9.80f;
                
            }
        }

        public void Update()
        {
            input.x = Input.GetAxis("Horizontal") * playerForce;
            input.y = Input.GetAxis("Vertical") * playerForce;


        }
        

    }
}
