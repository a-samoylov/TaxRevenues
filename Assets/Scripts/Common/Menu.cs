﻿using TMPro;
using UnityEngine;

namespace Common
{
    public class Menu : MonoBehaviour
    {
        public GameObject info;
        public ManufacturesManager manufacturesManager;
        
        private Canvas _canvas;

        private void Start()
        {
            _canvas = gameObject.GetComponentInChildren<Canvas>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                _canvas.enabled = !_canvas.enabled;
            }
        }

        public void ShowInfoPanelToggleChanged(bool value)
        {
            info.SetActive(value);
        }
        
        public void ShowManufactureInfoToggleChanged(bool value)
        {
            //todo
        }
        
        public void SaveManufactureLogsToggleChanged(bool value)
        {
            //todo
        }
    }
}