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
        public Action<string> ScoreChange;
        public Action<string> OxygenChange;
        public Action<string> InventoryChange;
        int playerOxygen;
        public int MaxInventory { get => maxInventory; set => maxInventory = value; }
        public int CurrentInventory { get => currentInventory; 
            private set { 

                currentInventory = value; 
                if (currentInventory > maxInventory) currentInventory = maxInventory;
                InventoryChange?.Invoke(currentInventory.ToString() + "/" + maxInventory.ToString());
            } 
        }
        public int Score { get => score; set {
                score = value;
                ScoreChange?.Invoke(score.ToString());
            } 
        }

        private void Start()
        {
            CurrentInventory = 0;
            player.GetComponent<Diver>().IsDead = false;

        }

        internal void AddOxygen(int v)
        {
            playerOxygen += v;
            if (playerOxygen > 10) playerOxygen = 10;
            OxygenChange?.Invoke(playerOxygen.ToString());
        }

        public bool AddToInventory(int value)
        {
            if (currentInventory + value > maxInventory)
            {
                return false;
            }
            currentInventory += value;
            InventoryChange?.Invoke(currentInventory.ToString() + "/" + maxInventory.ToString());
            return true;
        }

        public void EmptyInventory()
        {
            if (currentInventory > 0)
            {
                Score += currentInventory * CurrentInventory;
            }
            currentInventory = 0;
            InventoryChange?.Invoke(currentInventory.ToString() + "/" + maxInventory.ToString());
        }

        private void Update()
        {
            oxygenTimer += Time.deltaTime;
            if (oxygenTimer >= 1 )
            {
                if (player.gameObject.activeInHierarchy)
                {
                    oxygenTimer = 0;
                    playerOxygen -= 1;
                    OxygenChange?.Invoke(playerOxygen.ToString());
                    if (playerOxygen <= 0)
                    {
                        PlayerDeath();
                    }

                } else
                {
                    AddOxygen(10);
                }

            }

        }

        private void PlayerDeath()
        {
            player.GetComponent<Diver>().IsDead = true;
            print("You are dead");
        }
    }
}
