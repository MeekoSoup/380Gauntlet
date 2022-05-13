using System;
using Data;
using TMPro;
using UnityEngine;

namespace Control
{
    public class TallyUpScores : MonoBehaviour
    {
        public TMP_Text merlinText;
        public TMP_Text thorText;
        public TMP_Text thyraText;
        public TMP_Text questorText;
        
        public PlayerData merlinData;
        public PlayerData thorData;
        public PlayerData thyraData;
        public PlayerData questorData;

        private void Awake()
        {
            UpdateGUI();
        }

        private void UpdateGUI()
        {
            merlinText.text = merlinData.score.ToString();
            thorText.text = thorData.score.ToString();
            thyraText.text = thyraData.score.ToString();
            questorText.text = questorData.score.ToString();
        }
    }
}