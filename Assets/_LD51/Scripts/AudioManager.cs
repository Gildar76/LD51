using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GildarGaming.LD51
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource waterSplash;
        [SerializeField] AudioSource pickup;
        [SerializeField] AudioSource powerUp;
        [SerializeField] AudioSource getScore;
        [SerializeField] AudioSource death;
        [SerializeField] AudioSource oxygenAlarm;
        Diver diver;

        public static AudioManager Instance;
        private void Start()
        {
            diver = FindObjectOfType<Diver>();
        }
        AudioManager()
        {
            Instance = this;
        }

        public void PlayAudioSource(AudioSource source)
        {
            if (source.isPlaying)
            {
                source.Stop();
                
            }
            source.Play();
        }

        public void PlayWaterSplash()
        {
            if (diver.IsDead) return;
            PlayAudioSource(waterSplash);
        }

        public void PlayDeath()
        {
            //if (diver.IsDead) return;
            PlayAudioSource(death);
        }
        public void PlayGetScore()
        {
            if (diver.IsDead) return;
            PlayAudioSource(getScore);

        }
        public void PlayPickup()
        {
            if (diver.IsDead) return;
            PlayAudioSource(pickup);
        }
        public void PlayPowerUp()
        {
            if (diver.IsDead) return;
            PlayAudioSource(powerUp);
        }

        internal void PlayOxyGenAlarm()
        {
            if (diver.IsDead) return;
            PlayAudioSource(oxygenAlarm);
        }
    }
}
