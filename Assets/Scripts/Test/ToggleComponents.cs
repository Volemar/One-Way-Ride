using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ToggleComponents : MonoBehaviour, IInteractable
{
    [SerializeField]
    public GameObject targetObject;
    
    [SerializeField]
    private List<MonoScript> scriptsToToggle = new List<MonoScript>();

    public bool isExplorable => false;

    public bool hasCloseInteraction => false;

    public bool hasToggleCloseInteraction => false;

    public void Start()
    {
        
    }

    public void CloseInteraction()
    {
        
    }

    public void Interact()
    {
        List<string> scriptsToToggleNames = new List<string>(new string[scriptsToToggle.Count()]);
        bool state = false;
        for (int i = 0; i < scriptsToToggle.Count(); i++)
        {
            scriptsToToggleNames[i] = scriptsToToggle[i].name;
        }
        for (int i = 0; i < scriptsToToggle.Count; i++)
        {
            List<MonoBehaviour> scripts = targetObject.GetComponents<MonoBehaviour>().ToList();
            foreach (MonoBehaviour item in scripts)
            {
                if(item.GetType().Name == scriptsToToggleNames[i])
                {
                    item.enabled = !item.enabled;
                    state = item.enabled;
                }
            }
            if(state)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                return;
            }
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void StopCloseInteraction()
    {
        
    }

    public void StopInteracting()
    {
        
    }
}
