using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GildarGaming.LD51
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;
        public float Volume { get;set; }
        public AudioSource testAudio;
        bool started = false;
        private void Start()
        {
            Volume = 0.5f;
            VolumeSettings settings = FindObjectOfType<VolumeSettings>();
            Volume = settings.Volume;
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
            Slider slider = FindObjectOfType<Slider>();
            if (slider != null)
            {
                slider.value = Volume;
            }
            started = true;
            //DontDestroyOnLoad(gameObject);
        }

        public void OnsStartButtonClick()
        {
            VolumeSettings settings = FindObjectOfType<VolumeSettings>();
            settings.Volume = Volume;
            StartGame();
        }

        private void StartGame()
        {
            SceneManager.LoadScene(1);

        }

        public void OnQuitCLick()
        {
            Application.Quit();
        }
        public void OnVolumeChange(System.Single v)
        {
            Volume = v;
            Debug.Log(Volume);
            testAudio.volume = Volume;
            if (!testAudio.isPlaying && started)
            {
                testAudio.Play();
            }
            
            
        }
    }
}
