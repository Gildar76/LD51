using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GildarGaming.LD51
{
    public class Diver : MonoBehaviour
    {

        [SerializeField] float waterLevel = 6.5f;
        Rigidbody2D rb;
        Vector3 input;
        Vector3 force = Vector3.zero;
        [SerializeField] float playerForce = 10f;
        [SerializeField] GameManager gameManager;
        bool isDead = false;

        public bool IsDead { get => isDead; set => isDead = value; }

        public void Reset()
        {
        }
        
        public void Awake()
        {
        }
        
        public void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            gameManager = FindObjectOfType<GameManager>();
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

            force += input.normalized * playerForce;
            if (input.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            } else if (input.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }

        private void ApplyWaterForce()
        {
            Vector3 waterForce = new Vector3(0, 8, 0);
            if (transform.position.y < waterLevel)
            {
                
                waterForce = rb.velocity * 0.75f;
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
            if (isDead)
            {
                this.enabled = false;
                return;
            }
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            if (transform.position.y >= waterLevel)
            {
                gameManager.AddOxygen(10);
            }


        }
        

    }
}
