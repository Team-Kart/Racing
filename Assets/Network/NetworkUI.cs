using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] Button serverBtn;
    [SerializeField] Button hostBtn;
    [SerializeField] Button clientBtn;
    private void Awake()
    {
        serverBtn.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); });
        hostBtn.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); });
        clientBtn.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });
    }
}
