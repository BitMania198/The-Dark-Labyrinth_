using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkSync : Photon.MonoBehaviour
{
    public MonoBehaviour[] localScripts;

    public GameObject[] localObjects;
    Vector3 latestPos;
    Quaternion latestRot;

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.isMine)
        {
            //player is local
        }
        else
        {
            for(int i = 0; i < localScripts.Length; i++)
            {
                localScripts[i].enabled = false;
            }
            for(int i = 0;i < localObjects.Length; i++)
            {
                localObjects[i].SetActive(false);
            }
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //Network player, recieve data
            latestPos = (Vector3)stream.ReceiveNext();
            latestRot = (Quaternion)stream.ReceiveNext();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime * 5);
        }
    }
}
