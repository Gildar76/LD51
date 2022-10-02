using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GildarGaming.LD51
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text scoreText;
        [SerializeField]  TMPro.TMP_Text inventoryText;
        [SerializeField] TMPro.TMP_Text airText;
        [SerializeField] GameManager gameManager;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.ScoreChange += ScoreChanged;
            gameManager.InventoryChange += InventoryChanged;
            gameManager.OxygenChange += OxygenChanged;

        }

        private void OxygenChanged(string value)
        {
            
            airText.text = value;
        }

        private void InventoryChanged(string value)
        {
            inventoryText.text = value;
            
        }

        private void ScoreChanged(string value)
        {
            scoreText.text = value;
        }

    }
}
