using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{

    public class JoinRoom : MonoBehaviour
    {
        public RoomInfo roomInfo;

        public void Press()
        {
            Debug.Log("");
            Launchers.singelton.Button_JoinRoom(roomInfo);
        }
    }
}
