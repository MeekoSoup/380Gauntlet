using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData", order = 51)]
    public class PlayerData : ScriptableObject
    {
        public int controllerID;
        public int healthCurrent;
        public int healthMax;
        public int score;
        public bool isHost;
    }
}