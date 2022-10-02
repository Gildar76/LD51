using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

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
        public Action<string> DiveStopStart;
        public Action<string> OxygenChange;
        public Action<string> InventoryChange;
        Diver diver;
        int playerOxygen;
        bool gameOver = false;
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
            diver = player.GetComponent<Diver>();
            diver.IsDead = false;

        }

        public void GameOver()
        {
            gameOver = true;
            DiveStopStart("GAME OVER! Press ENTER to go back to menu");

        }

        internal void AddOxygen(int v)
        {
            if (playerOxygen <= 8) AudioManager.Instance.PlayPowerUp();
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
            AudioManager.Instance.PlayPickup();
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
            AudioManager.Instance.PlayGetScore();
        }
        public void CanEnterBoat()
        {
            DiveStopStart("Press space to end your dive");
        }

        public void CanNoLongerEnterBoat()
        {
            DiveStopStart("");
        }

        public void CanExitBoat()
        {
            DiveStopStart("Press space to start your next dive");
        }


        private void Update()
        {
            if (gameOver)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                {
                    
                    SceneManager.UnloadSceneAsync(1);
                    SceneManager.LoadScene(0);
                    

                    return;
                }
            }
            if (diver.IsDead) return;
            oxygenTimer += Time.deltaTime;
            if (oxygenTimer >= 1 )
            {
                if (player.gameObject.activeInHierarchy)
                {
                    oxygenTimer = 0;
                    playerOxygen -= 1;
                    if (playerOxygen < 4)
                    {
                        AudioManager.Instance.PlayOxyGenAlarm();
                    }
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

        public void PlayerDeath()
        {
            Diver diver = player.GetComponent<Diver>();
            if (!diver.IsDead)
            {
                diver.IsDead = true;
                AudioManager.Instance.PlayDeath();
            }
            GameOver();
            print("You are dead");
        }
    }
}
