using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] Button hostBtn;
    [SerializeField] Button clientBtn;
    //[SerializeField] string scene = "ClassicRaceway";
    private void Awake()
    {
        hostBtn.onClick.AddListener(() => { 
            NetworkManager.Singleton.StartHost();
            gameObject.SetActive(false);
        });
        clientBtn.onClick.AddListener(() => { 
            NetworkManager.Singleton.StartClient();
            gameObject.SetActive(false);
        });
    }
}
