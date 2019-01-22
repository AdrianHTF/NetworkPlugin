using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SA
{
    public class KeyboardButton : MonoBehaviour {
       
        public Text target;

        public static KeyboardButton singelton;

        private bool debug = true;

        private bool low = true;
        private bool letters = true;
        private GameObject[] sig;
        private GameObject[] gos;
        private GameObject[] letterVals;

        private void Awake()
        {
            
        }

        private void Start()
        {
            gos = GameObject.FindGameObjectsWithTag("ButtonLetter");
            sig = GameObject.FindGameObjectsWithTag("ButtonSign");
            letterVals = GameObject.FindGameObjectsWithTag("UPPERLOWER");

        }

        public void PressLetter()
        {
            Text letter = this.gameObject.GetComponentInChildren<Text>();
            target.text += letter.text;
            SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).TriggerHapticPulse(5000);
        }

        public void PressReturn()
        {
            if (target.text.Length > 0)
                target.text = target.text.Remove(target.text.Length - 1);
            SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).TriggerHapticPulse(2000);
        }

        public void PressSubmit()
        {
            Launchers.singelton.Button_Create();
            SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).TriggerHapticPulse(2000);
        }

        public void PressCancle()
        {
            target.text = "";
            Launchers.singelton.switchToLobbyUI();
            SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).TriggerHapticPulse(2000);
        }

        public void PressBack()
        {
            Launchers.singelton.switchToLobbyUI();
            SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).TriggerHapticPulse(2000);
        }

        public void OpenKeyboard()
        {
            Launchers.singelton.switchToKeyboard();
            SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).TriggerHapticPulse(2000);
        }

        public void PressToUpperLower()
        {


            if (low)
                ToUpperCase(letterVals);
            else
                ToLowerCase(letterVals);

            SetButtonUpperLower();
            SetLow();

        }

        public void SwitchSignsLetters()
        {
            if (letters)
                SwitchToSigns();
            else
                SwitchToLetters();

            SetLetters();
        }

        public void ChooseAvatar()
        {
            Launchers.singelton.switchToAvatarSelection();
            SteamVR_Controller.Input(SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost)).TriggerHapticPulse(2000);
        }

        public void setAvatar()
        {
            Launchers.singelton.setAvatar(GameObject.FindGameObjectWithTag("avatarSelection").GetComponentInChildren<Dropdown>().value);
        }

        private void SwitchToSigns()
        {
            foreach (GameObject obj in sig)
                obj.SetActive(true);
            foreach (GameObject obj in gos)
                obj.SetActive(false);
        }

        private void SwitchToLetters()
        {
            foreach (GameObject obj in sig)
                obj.SetActive(false);
            foreach (GameObject obj in gos)
                obj.SetActive(true);
        }

        private void SetLetters()
        {
            if (letters)
                letters = false;
            else
                letters = true;
        }

        private void ToUpperCase(GameObject[] gameObjects)
        {
            foreach (GameObject obj in gameObjects)
                obj.GetComponentInChildren<Text>().text = obj.GetComponentInChildren<Text>().text.ToUpper();
        }

        private void ToLowerCase(GameObject[] gameObjects)
        {
            foreach (GameObject obj in gameObjects)
                obj.GetComponentInChildren<Text>().text = obj.GetComponentInChildren<Text>().text.ToLower();
        }

        private void SetLow()
        {
            if (low)
                low = false;
            else
                low = true;
        }

        private void SetButtonUpperLower()
        {
            Text shift = GameObject.FindGameObjectWithTag("Shift").GetComponentInChildren<Text>();
            if (low)
                shift.text = "LOW";
            else
                shift.text = "UP";
        }

    }
}

