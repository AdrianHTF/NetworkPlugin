using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{


    public class UIManager : MonoBehaviour {

        public GameObject logger;
        public Text logger_text;
        public Transform roomGrid;
        public GameObject roomTamplate;
        public GameObject createRoom;
        public GameObject roomList;

        List<GameObject> roomsFound = new List<GameObject>();

        public void AddRoom(RoomInfo r)
        {
            
            GameObject go = Instantiate(roomTamplate, roomTamplate.transform.position, roomTamplate.transform.rotation) as GameObject;
            JoinRoom jr = go.GetComponent<JoinRoom>();
            jr.roomInfo = r;
            Text t = go.GetComponentInChildren<Text>();
            t.text = r.Name + "\t" + r.PlayerCount + "/" + r.MaxPlayers;
            go.transform.SetParent(roomGrid);
            roomsFound.Add(go);
            go.SetActive(true);
        }


        public void UpdateLogger(string s, bool close = false)
        {
            logger.SetActive(true);
            logger_text.text = s;

            if (close)
            {
                StartCoroutine("CloseLogger");
            }
        }

        public void CloseUI()
        {
            roomGrid.gameObject.SetActive(false);
            createRoom.gameObject.SetActive(false);
            roomList.SetActive(false);
        }

        public void OpenUI()
        {
            roomGrid.gameObject.SetActive(true);
            createRoom.gameObject.SetActive(true);
            roomList.SetActive(true);
        }

        IEnumerator CloseLogger()
        {
            yield return new WaitForSeconds(1);
            logger.SetActive(false);
        }

        public static UIManager singelton;
        private void Awake()
        {
            singelton = this;
        }
    }
}
