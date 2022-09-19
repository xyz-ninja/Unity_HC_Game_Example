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
         
         base.Open();

         _panelsManager.CloseAllPanels(this);

         StartCoroutine(CoroOpen());
      }
      
      IEnumerator CoroOpen() {
         foreach (var punchScaler in _punchScalers) {
            punchScaler.Scale();
            
            yield return new WaitForSeconds(0.06f);
         }
      }
   }
}
