using System.Threading.Tasks;
using JetBrains.Annotations;
using PCV_Fundamentals;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace PCV_Interfaces
{
    
    public enum PCV_ObjectTypeEnum
    {
        Subject,
        Event,
        InfoNode,
        Object,
    }
    
    public enum GenerateState
    {
        Idle,
        Generating,
        Done,
        Error,
        Cancelled
    }
    
    public class ObjectGenerator : Singleton<ObjectGenerator>
    {
        [SerializeField]
        private GameObject uiField;
        
        [SerializeField]
        private TMP_InputField inputField;
        
        [SerializeField]
        private GenerateState state;
        
        public async Task<PCV_Fundamentals.PCV_Object> GenerateObject(PCV_ObjectTypeEnum typeEnum)
        {
            uiField.SetActive(true);
            
            state = GenerateState.Generating;

            await WaitForGeneration();
            
            string input = inputField.text;
            PCV_Fundamentals.PCV_Object output = null;
            
            if (state == GenerateState.Done)
            {
                switch (typeEnum)
                {
                    case PCV_ObjectTypeEnum.Subject:
                        output = new PCV_Subject(input);
                        break;
                    case PCV_ObjectTypeEnum.Event:
                        output = new PCV_Event(input);
                        break;
                    case PCV_ObjectTypeEnum.InfoNode:
                        output = new PCV_InfoNode(input);
                        break;
                }
            }
            
            state = GenerateState.Idle;
            inputField.text = "";
            uiField.SetActive(false);

            return output;
        }
            
        private async Task WaitForGeneration()
        {
            var escape = false;
            
            while (state == GenerateState.Generating || !escape)
            {
                await Task.Yield();
                
                if (state == GenerateState.Done)
                {
                    escape = true;
                
                    if (string.IsNullOrEmpty(inputField.text))
                    {
                        Debug.LogWarning("Input field is empty!");
                        escape = false;
                    }
                }
                else if (state == GenerateState.Error)
                {
                    escape = false;
                }
                else if (state == GenerateState.Cancelled)
                {
                    escape = true;
                }
            }
        }

        public void OnGenerateButton()
        {
            state = GenerateState.Done;
        }

        public void OnCancelButton()
        {
            state = GenerateState.Cancelled;
        }
    }
}
