using System;
using System.Collections.Generic;
using PCV_Fundamentals;
using UnityEngine;

namespace PCV_Interfaces
{
    public class SelectMenuCall
    {
        public string name;
        public Action onSelect;
        public Action onDeselect;
        public Action onUse;
        
        public SelectMenuCall(string name, Action onSelect, Action onDeselect, Action onUse)
        {
            this.name = name;
            this.onSelect = onSelect;
            this.onDeselect = onDeselect;
            this.onUse = onUse;
        }
    }
    
    public class SelectMenu : MonoBehaviour
    {
        List<SelectMenuItem> menuItems = new List<SelectMenuItem>();
        public GameObject menuItemPrefab;
        
        public bool isExclusive = true;
        
        public int MenuItemCount => menuItems.Count;
        public int SelectedIndex { get; set; } = 0;
        public SelectMenuItem SelectedItem => menuItems[SelectedIndex];
        
        RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            Program.Instance.OnDeselect(gameObject);
        }

        public void Initiate(params SelectMenuCall[] options)
        {
            foreach (var option in options)
            {
                AddItem(option.name, option.onSelect, option.onDeselect, option.onUse);
            }
            rectTransform.sizeDelta = new Vector2(256f, MenuItemCount * 30);
        }
        
        public void AddItem(string n, Action onSelect, Action onDeselect, Action onUse)
        {
            var item = Instantiate(menuItemPrefab, transform).GetComponent<SelectMenuItem>();
            
            item.Label = n;
            item.onSelect = onSelect;
            item.onDeselect = onDeselect;
            item.onUse = onUse;
            menuItems.Add(item);
            item.parentMenu = this;
            item.index = MenuItemCount - 1;
        }
        
        public void RemoveItem(SelectMenuItem item)
        {
            menuItems.Remove(item);
            item.Destroy();
        }
        
        public void SelectItem(int index)
        {
            if (index < 0 || index >= MenuItemCount)
            {
                Debug.LogError($"Index {index} is out of range for menu {gameObject.name}");
                return;
            }
            
            if (SelectedIndex >= 0)
            {
                SelectedItem.Deselect();
            }
            
            SelectedIndex = index;
            SelectedItem.Select();
        }
    }
}
