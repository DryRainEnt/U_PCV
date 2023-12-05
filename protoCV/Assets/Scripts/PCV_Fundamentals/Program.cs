using System;
using System.Collections;
using System.Collections.Generic;
using PCV_Interfaces;
using TMPro;
using UnityEngine;

namespace PCV_Fundamentals
{
	public class Program : Singleton<Program>
	{
		public Transform UIParent;

		public GameObject SelectMenuPrefab;
		
		public Transform InfoBoxParent;
		public GameObject InfoBoxPrefab;
		
		[SerializeField]
		private TMP_Text debugText;

		private List<InfoBox> _infoBoxes = new List<InfoBox>();
		
		private Action<GameObject> _onDeselect;
    
		// Start is called before the first frame update
		void Start()
		{
        
		}

		// Update is called once per frame
		void Update()
		{
        
		}
		
		public void GenerateInfoBox(ulong iobj, ulong iinfoNode)
		{
			GameObject infoBoxObject = Instantiate(InfoBoxPrefab, InfoBoxParent);
			InfoBox infoBox = infoBoxObject.GetComponent<InfoBox>();
			infoBox.LoadInfo(iobj, iinfoNode);
			_infoBoxes.Add(infoBox);
		}

		public void ListenForDeselect(Action<GameObject> action)
		{
			_onDeselect += action;
		}
		
		public void StopListeningForDeselect(Action<GameObject> action)
		{
			_onDeselect -= action;
		}
		
		public void OnDeselect(GameObject obj)
		{
			_onDeselect?.Invoke(obj);
		}
		
		public SelectMenu CreateSelectMenu(Vector3 position, params SelectMenuCall[] options)
		{
			GameObject selectMenuObject = Instantiate(SelectMenuPrefab, UIParent);
			SelectMenu selectMenu = selectMenuObject.GetComponent<SelectMenu>();
			selectMenu.transform.position = position;
			selectMenu.Initiate(options);
			return selectMenu;
		}

		public void OnDatabaseModified()
		{
			int subCount = DatabaseManager.GetSubjectQuery().Length;
			int evCount = DatabaseManager.GetEventQuery().Length;
			int infoCount = DatabaseManager.GetInfoNodeQuery().Length;
			
			debugText.text = $"Subjects: {subCount}\n" +
			                 $"Events: {evCount}\n" +
			                 $"InfoNodes: {infoCount}";
		}
	}

}