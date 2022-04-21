using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData", order = 51)]
    public class PlayerData : ScriptableObject
    {
        public int controllerID;
        public int health;
        public int score;
        public int keys;
        public bool isHost;
    }
}