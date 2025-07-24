using BepInEx;
using BepInEx.Configuration;
using Photon.Pun;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SimpleBoards
{
    [BepInPlugin("Shadow.SimpleBoards", "SimpleBoards", "1.0.0")]
    internal class Plugin : BaseUnityPlugin
    {
        private TextMeshPro? tmp;
        private GameObject? textObj;
        private bool shouldUpdate;

        ConfigEntry<string>? stumptext;
        ConfigEntry<string>? motdtext;
        ConfigEntry<string>? motdheading;
        ConfigEntry<string>? coctext;
        ConfigEntry<string>? cocheading;

        ConfigEntry<bool>? enableFeatureA;
        ConfigEntry<bool>? enableFeatureB;
        ConfigEntry<bool>? enableFeatureC;
        
        ConfigEntry<float>? StumpFloat;

        private void Start()
        {
            //stumptext = Config.Bind<string>("Text", "Stump Text", "Hi there Shadow", "The text in the middle of stump");
            motdtext = Config.Bind<string>("Text", "motd text", "Fun fact, you can change what this says, look the the code of conduct/guide board for the steps.", "The text on the message of the day board");
            motdheading = Config.Bind<string>("Text", "motd heading", "Shadow's message of the day", "The heading on the message of the day board");
            coctext = Config.Bind<string>("Text", "coc text", "Go to your Gorilla Tag folder\nThen BepInEx folder\nThen Config folder\nThen double click \"Shadow.SimpleBoards.cfg\"", "The text on the code of conduct board");
            cocheading = Config.Bind<string>("Text", "coc heading", "Shadow's Guide", "The heading on the code of conduct board");

            //enableFeatureA = Config.Bind<bool>("Bool", "Stump Text enabled?", true, "Toggles the Stump Text on or off");
            enableFeatureB = Config.Bind<bool>("Bool", "Motd enabled?", true, "Toggles the custom Motd board on or off");
            enableFeatureC = Config.Bind<bool>("Bool", "Coc enabled?", true, "Toggles the custom coc board on or off");

            //StumpFloat = Config.Bind<float>("Float", "Stump Text height", 12.05f, "Changes how high or low the Stump Text is");

            GorillaTagger.OnPlayerSpawned(OnGameInit);
        }

        void OnEnable()
        {
            shouldUpdate = true;
        }

        void OnDisable()
        {
            shouldUpdate = false;
        }

        void OnGameInit()
        {
            _ = RunInitSequence();
            if (enableFeatureA.Value)
            {
               // CreateTMPWorldText();
            }
        }

        void Update()
        {
            Vector3 forward = Camera.main.transform.forward;

            if (forward != Vector3.zero)
            {
                textObj.transform.rotation = Quaternion.LookRotation(forward);
            }
        }

        async Task RunInitSequence()
        {
            await Task.Delay(10000);
            if (enableFeatureB.Value)
            {
                UpdateMOTD();
            }
            if (enableFeatureC.Value)
            {
                UpdateCoc();
            }
        }

        private void UpdateMOTD()
        {
                GameObject gameObject = GameObject.Find("motdBodyText");
                bool flag1 = gameObject != null;
                if (flag1)
                {
                    TextMeshPro component = gameObject.GetComponent<TextMeshPro>();
                    bool flag2 = component != null;
                    if (flag2)
                    {
                        component.text = motdtext.Value;
                    }
                }

                GameObject gameObjectS = GameObject.Find("motdHeadingText");
                bool flagS1 = gameObjectS != null;
                if (flagS1)
                {
                    TextMeshPro componentB = gameObjectS.GetComponent<TextMeshPro>();
                    bool flag3 = componentB != null;
                    if (flag3)
                    {
                        componentB.text = motdheading.Value;
                    }
                }

                GameObject.Find("TMP SubMesh [LiberationSans SDF Material]")?.SetActive(false);
        }

        private void UpdateCoc()
        {
                GameObject gameObject = GameObject.Find("COCBodyText_TitleData");
                bool flag1 = gameObject != null;
                if (flag1)
                {
                    TextMeshPro component = gameObject.GetComponent<TextMeshPro>();
                    bool flag2 = component != null;
                    if (flag2)
                    {
                        component.text = coctext.Value;
                    }
                }

                GameObject gameObjectA = GameObject.Find("CodeOfConductHeadingText");
                bool flag1A = gameObjectA != null;
                if (flag1A)
                {
                    TextMeshPro componentA = gameObjectA.GetComponent<TextMeshPro>();
                    bool flag2 = componentA != null;
                    if (flag2)
                    {
                        componentA.text = cocheading.Value;
                    }
                }
        }

        private void CreateTMPWorldText()
        {
            textObj = new GameObject("Stump Text");
            textObj.transform.position = new Vector3(-66.9232f, StumpFloat.Value, -82.5794f);
            textObj.transform.localScale = Vector3.one * 0.1f;

            tmp = textObj.AddComponent<TextMeshPro>();
            tmp.text = stumptext.Value;
            tmp.fontSize = 10f;
            tmp.alignment = TextAlignmentOptions.Center;


            shouldUpdate = true;
        }
    }
}