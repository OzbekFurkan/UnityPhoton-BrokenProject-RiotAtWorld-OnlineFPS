using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausesc : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene(0);
    }


}
