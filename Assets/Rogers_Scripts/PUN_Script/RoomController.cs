using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        if(!PhotonNetwork.connected)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            return;
        }

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity, 0);
    }
    private void OnGUI()
    {
        if (PhotonNetwork.room == null) return;
        if (GUI.Button(new Rect(5, 5, 125, 25), "Leave Room"))
        {
            PhotonNetwork.LeaveRoom();
        }
        GUI.Label(new Rect(135, 5, 200, 25), PhotonNetwork.room.Name);

        for(int i = 0;i< PhotonNetwork.playerList.Length;i++)
        {
            string isMasterClient = (PhotonNetwork.playerList[i].IsMasterClient ? ": MasterClient" : "");
            GUI.Label(new Rect(5, 35 + 30 * i, 200, 25), PhotonNetwork.playerList[i].NickName + isMasterClient);
        }
    }
    void OnLeftRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
