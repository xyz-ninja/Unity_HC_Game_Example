using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCExample.GUI {
    public class Panel : MonoBehaviour {
        public virtual void Open() {
            this.gameObject.SetActive(true);
        }

        public virtual void Close() {
            this.gameObject.SetActive(false);
        }
    }
}