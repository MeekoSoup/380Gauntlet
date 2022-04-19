﻿using UnityEngine;

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
        public int controllerID;
        public int healthCurrent;
        public int healthMax;
        public int score;
        public bool isHost;
    }
}