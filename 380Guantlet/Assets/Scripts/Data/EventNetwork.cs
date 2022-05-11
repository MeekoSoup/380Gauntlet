using UnityEngine;
using UnityEngine.InputSystem;

namespace Data
{
    /**
     * EventNetwork is used to communicate common events across the whole game. These usually fall under player events,
     * enemy events, and audio events (specifically considering the game's announcer).
     *
     * As a ScriptableObject, it is a plug-'n-play style object, so use it whenever a script needs to invoke one of
     * these events, or listen for one of them to be invoked.
     *
     * When a player event is invoked with no parameters, it signifies that the specific player is not important, so
     * make sure to accomodate this if you are invoking player events. 
     */
    
    [CreateAssetMenu(fileName = "EventNetwork", menuName = "Game/EventNetwork", order = 51)]
    public class EventNetwork : ScriptableObject
    {
        // We can have an empty overloaded method if the specific player isn't needed
        public delegate void PlayerEvent(PlayerInput playerInput = null);

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
        public PlayerEvent OnPlayerShootFood;
        public PlayerEvent OnPlayerDestroyFood;
        public PlayerEvent OnPlayerShootPlayer;
        public PlayerEvent OnPlayerUseNuke;

        // should probably be overloaded with a parameter for the enemy controller
        public delegate void EnemyEvent();

        public EnemyEvent OnEnemyDamaged;
        public EnemyEvent OnEnemyKilled;
        public EnemyEvent OnEnemySpawned;

        public delegate void AudioEvent();

        public AudioEvent OnVolumeChanged;
    }
}