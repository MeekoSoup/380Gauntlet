using UnityEngine;

namespace Data
{
    public class EventNetworkInvoker : MonoBehaviour
    {
        public EventNetwork eventNetwork;

        public void PlayerJoined()
        {
            eventNetwork.OnPlayerJoined?.Invoke();
        }
    }
}