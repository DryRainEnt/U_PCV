using System;
using TMPro;
using UnityEngine;

namespace PCV_Interfaces
{
    public class SelectMenuItem : MonoBehaviour
    {
        public int index;
        public SelectMenu parentMenu;

        [SerializeField] private TMP_Text label;
        
        public string Label
        {
            get => label.text;
            set => label.text = value;
        }
        public Action onSelect;
        public Action onDeselect;
        public Action onUse;
        
        public void Select()
        {
            parentMenu.SelectedIndex = index;
            onSelect?.Invoke();
        }
        
        public void Deselect()
        {
            parentMenu.SelectedIndex = -1;
            onDeselect?.Invoke();
        }

        public void Use()
        {
            onUse?.Invoke();
        }

        public void Destroy()
        {
            
        }
    }
}
