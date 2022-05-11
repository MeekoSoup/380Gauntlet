using Control;
using UnityEngine;

namespace Data
{
    /**
     * PlayerData is designed to identify different players internally, and hold persistent stats that will be dumbed
     * from playthrough to playthrough.
     *
     * Currently monitors controllerID, health (current and max), score, and whether this player is considered the host.
     * PlayerData is used a lot with the EventNetwork class when a specific player has raised a certain event.
     */
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData", order = 51)]
    public class PlayerData : ScriptableObject
    {
        [Header("Role Stats")]
        public PlayerRole role;
        public string heroName;
        public GameObject heroGraphics;
        public GameObject heroWeapon;
        public float healthStarting;
        [Header("Current Stats")]
        public float health;
        public int score;
        public int keys;
        public int potions;

        public void Reset()
        {
            health = healthStarting;
            score = 0;
            keys = 0;
            potions = 0;
        }
    }
}