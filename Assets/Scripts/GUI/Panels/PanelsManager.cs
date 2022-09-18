using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCExample.GUI {

    public class PanelsManager : MonoBehaviour {

        [Header("Transforms")] 
        [SerializeField] private Transform _panelsT;

        private List<Panel> _panels = new List<Panel>();

        private void Awake() {
            foreach (Transform panelT in _panelsT) {
                var panel = panelT.gameObject.GetComponent<Panel>();

                if (panel == null) {
                    Debug.Log("Panel missed!");
                    continue;
                }
                
                _panels.Add(panel);
            }
        }
    }
}
