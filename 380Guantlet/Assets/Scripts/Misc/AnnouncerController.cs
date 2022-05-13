using System;
using System.Collections;
using Character;
using Data;
using General;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Misc
{
    public class AnnouncerController : MonoBehaviour
    {
        public EventNetwork eventNetwork;
        public TMP_Text announcerText;

        private void OnEnable()
        {
            eventNetwork.OnThiefStealsPotion += ThiefStealsPotion;
            eventNetwork.OnPlayerUseNuke += UsePotion;
            StartCoroutine(DisplayText(""));
        }

        private void UsePotion(PlayerInput playerInput = null)
        {
            if (playerInput)
                StartCoroutine(DisplayText($"{playerInput.gameObject.name} used a Potion and nuked some enemies!"));
            else
                StartCoroutine(DisplayText($"Someone used a Potion and nuked some enemies!"));
        }

        private void OnDisable()
        {
            eventNetwork.OnThiefStealsPotion -= ThiefStealsPotion;
            eventNetwork.OnPlayerUseNuke -= UsePotion;
        }

        private void ThiefStealsPotion(PlayerInput playerInput = null)
        {
            if (playerInput)
            {
                string text = $"Thief steals potion from {playerInput.GetComponent<PlayerOverseer>().playerData.name}!";
                StartCoroutine(DisplayText(text));
            }
        }

        private IEnumerator DisplayText(string text, float duration = 1.5f, float fadeDuration = 1.5f)
        {
            Color color = announcerText.color;
            color.a = 1f;
            announcerText.color = color;
            announcerText.text = text;
            yield return new WaitForSeconds(duration);
            StartCoroutine(FadeText(fadeDuration));
        }

        private IEnumerator FadeText(float duration)
        {
            Color color = announcerText.color;
            float originalTime = Time.time;
            float diff = 0f;
            while (diff < duration)
            {
                color.a = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(0f, duration, Time.time - originalTime));
                announcerText.color = color;
                yield return new WaitForSeconds(0.01f);
                diff = Time.time - originalTime;
            }

            color.a = 0f;
            announcerText.color = color;
        }
    }
}