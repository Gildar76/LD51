using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

namespace GildarGaming.LD51
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;
        public float Volume { get;set; }
        private void Start()
        {
            
            MenuManager[] managers = FindObjectsOfType<MenuManager>();
            foreach (MenuManager manager in managers)
            {
                if (manager != this)
                {
                    Destroy(this);
                    break;
                }
            }
            instance = this;
        }

        public void OnsStartButtonClick()
        {
            StartGame();
        }

        private void StartGame()
        {
            SceneManager.LoadScene(1);

        }
        public void OnVolumeChange(System.Single v)
        {
            Volume = v;
            Debug.Log(Volume);
        }
    }
}
