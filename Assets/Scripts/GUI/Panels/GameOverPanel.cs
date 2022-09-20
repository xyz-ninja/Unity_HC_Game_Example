using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HCExample.GUI {
   public class GameOverPanel : Panel {

      [SerializeField] private PanelsManager _panelsManager;
      [SerializeField] private List<PunchScaler> _punchScalers;
      [SerializeField] private Button _restartButton;

      private void Awake() {
         _restartButton.onClick.AddListener(() => {
            
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
         });
      }

      public override void Open() {
         
         _panelsManager.CloseAllPanels(this);
         
         base.Open();
      }
   }
}
