using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Game/GameData", order = 51)]
    public class GameData : ScriptableObject
    {
        public int stageIndex;
    }
}