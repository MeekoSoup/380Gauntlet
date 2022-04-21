using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EventNetwork", menuName = "Game/EventNetwork", order = 51)]
    public class EventNetwork : ScriptableObject
    {
        // We can have an empty overloaded method if the specific player isn't needed
        public delegate void PlayerEvent(PlayerData playerData = null);

        public PlayerEvent OnPlayerDamaged;
        public PlayerEvent OnPlayerKilled;
        public PlayerEvent OnPlayerJoined;
        public PlayerEvent OnPlayerRevived;
        public PlayerEvent OnPlayerScored;
        public PlayerEvent OnPlayerPickup;
        public PlayerEvent OnPlayerPickupNuke;
        public PlayerEvent OnPlayerExitStage;
        public PlayerEvent OnPlayerDisconnect;
        public PlayerEvent OnPlayerReconnect;
        public PlayerEvent OnPlayerLeavesCameraZone;

        // should probably be overloaded with a parameter for the enemy controller
        public delegate void EnemyEvent();

        public EnemyEvent OnEnemyDamaged;
        public EnemyEvent OnEnemyKilled;
        public EnemyEvent OnEnemySpawned;

        public delegate void AudioEvent();

        public AudioEvent OnVolumeChanged;
    }
}