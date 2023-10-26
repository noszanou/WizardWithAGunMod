using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BepInEx;
using BullshitForm;
using BullshitLib;
using GGDebug;
using GGUtil;
using Inventory;
using Inventory.Unity;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mod
{
    [BepInPlugin(ConstMODInfo.MAIN_PLUGIN_GUID, ConstMODInfo.MAIN_PLUGIN_NAME, ConstMODInfo.MAIN_PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic

            // Winform
            Wtf.Open();
            Logger.LogInfo($"Plugin {ConstMODInfo.MAIN_PLUGIN_GUID} is loaded!");

            UIDialogueStack.OnFocusChanged += new FocusChangedHandler(OnFocusChanged);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != "title-screen")
            {
                return;
            }
            GameObject val = new GameObject("InGameHotkey");
            val.AddComponent<InGameHotKey>();
            DontDestroyOnLoad(val);
        }

        private void OnFocusChanged(IUIDialogue from, IUIDialogue to)
        {
            // add button nieunieu 
            // new form when click nieunieu
            // oue non flemme jtrouve mm pas l'UI du options menu è_é
            //UIDialogueStack.OnFocusChanged -= new FocusChangedHandler(OnFocusChanged);
            UIWindow val = to as UIWindow;
            if (val == null)
            {
                return;
            }
            Debug.Log($"focus {val} {val.name} {val.tag} {val.gameObject.name} {val.GetScriptClassName()}");
            if (val.ToString().Contains("options-menu")) 
            {
            }
        }
    }

    public class InGameHotKey : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                ItemInteraction.CreateItem(amount: 50);
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                Debug.Log(ItemInteraction.GetItemsByName());
            }
            //Debug.Log($"update");
        }
    }
}
