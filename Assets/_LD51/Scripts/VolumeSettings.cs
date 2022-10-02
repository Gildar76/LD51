using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace GildarGaming.LD51
{
    public class VolumeSettings : MonoBehaviour
    {
        public float Volume { get; set; } = 0.5f;
        public void Reset()
        {
        }
        
        public void Awake()
        {
        }
        
        public void Start()
        {
            VolumeSettings[] otherSettings = FindObjectsOfType<VolumeSettings>();
            if (otherSettings.Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
        }
        
        public void OnEnable()
        {

        }
        
        public void OnDisable()
        {
        }
        
        public void Update()
        {
        }
        
        public void OnDestroy()
        {
        }
    }
}
