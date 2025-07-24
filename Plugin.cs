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
        private bool shouldUpdate = false;

        private ConfigEntry<string>? stumptext;
        private ConfigEntry<string>? motdtext;
        private ConfigEntry<string>? motdheading;
        private ConfigEntry<string>? coctext;
        private ConfigEntry<string>? cocheading;

        private ConfigEntry<bool>? enableFeatureA;
        private ConfigEntry<bool>? enableFeatureB;
        private ConfigEntry<bool>? enableFeatureC;

        private ConfigEntry<float>? StumpFloat;


        void Awake()
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
        }
        private void Start()
        {
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

        private void OnGameInit()
        {
            _ = RunInitSequence();
            if (enableFeatureA.Value)
            {
                //CreateTMPWorldText();
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

        private async Task RunInitSequence()
        {
            await Task.Delay(10000);
            if (enableFeatureB.Value)
            {
                try
                {
                    UpdateMOTD();
                }
                catch (Exception)
                {
                }
            }
            await Task.Delay(1000);
            if (enableFeatureC.Value)
            {
                try
                {
                    UpdateCoc();
                }
                catch (Exception)
                {
                }
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
                UpdateCoc();

                GameObject gameObjectD = GameObject.Find("TMP SubMesh [LiberationSans SDF Material]");
                gameObjectD.SetActive(false);
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
                if (flag1)
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