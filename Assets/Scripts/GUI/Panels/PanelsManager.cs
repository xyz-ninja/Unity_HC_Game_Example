using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCExample.GUI {

    public class PanelsManager : MonoBehaviour {

        [Header("Transforms")] 
        [SerializeField] private Transform _panelsT;

        [Header("Panels")]
        [SerializeField] private GameOverPanel _gameOverPanel;
        
        private List<Panel> _panels = new List<Panel>();

        #region getters

        public GameOverPanel GameOverPanel => _gameOverPanel;

        #endregion

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

        public void CloseAllPanels(Panel exceptPanel) {
            
            foreach (var panel in _panels) {
                if (panel == exceptPanel) {
                    continue;
                }
                
                panel.Close();
            }
        }
    }
}
