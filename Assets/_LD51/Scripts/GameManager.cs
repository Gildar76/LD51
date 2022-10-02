using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GildarGaming.LD51
{
    public class GameManager : MonoBehaviour
    {
        public GameObject player;
        [SerializeField] int maxInventory;
        int currentInventory = 0;
        int score = 0;
        float oxygenTimer = 0;

        float playerOxygen;
        public int MaxInventory { get => maxInventory; set => maxInventory = value; }
        public int CurrentInventory { get => currentInventory; 
            private set { 

                currentInventory = value; 
                if (currentInventory > maxInventory) currentInventory = maxInventory;
            } 
        }
        public int Score { get => score; set => score = value; }

        private void Start()
        {
            CurrentInventory = 0;

        }

        internal void AddOxygen(int v)
        {
            playerOxygen += v;
            if (playerOxygen > 10) playerOxygen = 10;
        }

        public bool AddToInventory(int value)
        {
            if (currentInventory + value > maxInventory)
            {
                return false;
            }
            currentInventory += value;
            return true;
        }

        public void EmptyInventory()
        {
            if (currentInventory > 0)
            {
                Score += currentInventory;
            }
            currentInventory = 0;
        }

        private void Update()
        {
            oxygenTimer += Time.deltaTime;
            if (oxygenTimer >= 1)
            {
                playerOxygen -= 1f;
                if (playerOxygen <= 0)
                {
                    PlayerDeath();
                }
            }

        }

        private void PlayerDeath()
        {
            print("You are dead");
        }
    }
}
