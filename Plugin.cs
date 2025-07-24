using BepInEx;
using Cinemachine;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace MotdChanger
{
    [BepInPlugin("com.maroon.shadow.SB", "SimpleBoards", "1.0.0")] // change this
    internal class Plugin : BaseUnityPlugin
    {

        private string path = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Gorilla Tag\\Boards\\motdText.txt";
        private string pathA = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Gorilla Tag\\Boards\\cocText.txt";
        private string pathAB = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Gorilla Tag\\Boards\\motdHeading.txt";
        private string pathABC = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Gorilla Tag\\Boards\\cocHeading.txt";
        private string pathABCD = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Gorilla Tag\\Boards";
        private string lastTxt = "";
        private float checking = 1f;
        private float t = 0f;
        private void Start() // does this right before spawn
        {
            bool Files = !File.Exists(this.pathABCD);
            if (Files)
            {
                string folderName = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Gorilla Tag\\Boards";
                Directory.CreateDirectory(folderName);
            }
            bool flag = !File.Exists(this.path);
            if (flag)
            {
                File.WriteAllText(this.path, "Hi Player! Fun fact, you can change what this says in your Gorilla tag folder, there should be another folder called \"Boards\" in there you can change the name and text for both boards.");
            }
            bool flag1 = !File.Exists(this.pathA);
            if (flag1)
            {
                File.WriteAllText(this.pathA, "Here are the steps:\nGo to your Gorilla Tag folder\nOpen \"Boards\"\nThen change the text in the .txt files");
            }
            bool flag2 = !File.Exists(this.pathAB);
            if (flag2)
            {
                File.WriteAllText(this.pathAB, @"Shadow's Message of the day");
            }
            bool flag3 = !File.Exists(this.pathABC);
            if (flag3)
            {
                File.WriteAllText(this.pathABC, @"Shadow's Guide");
            }
            GorillaTagger.OnPlayerSpawned(OnGameInit); // keep this
        }

        private void OnGameInit()
        {
            _ = RunInitSequence();
        }

        private async Task RunInitSequence()
        {
            try
            {
                await Task.Delay(12000);
                UpdateMOTD();
            }
            catch (Exception ex)
            {

            }

            try
            {
                await Task.Delay(1000);
                UpdateCoc();
            }
            catch (Exception ex)
            {

            }
        }



        private void Update() // does this every frame
        {

        }

        private void UpdateMOTD()
        {
            string Heading = File.ReadAllText(this.pathAB);
            string text = File.ReadAllText(this.path);
            bool flag = File.Exists(this.path);
            if (flag)
            {
                GameObject gameObject = GameObject.Find("motdBodyText");
                bool flag1 = gameObject != null;
                if (flag1)
                {
                    TextMeshPro component = gameObject.GetComponent<TextMeshPro>();
                    bool flag2 = component != null;
                    if (flag2)
                    {
                        component.text = text;
                        this.lastTxt = text;
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
                        componentB.text = Heading;
                        this.lastTxt = text;
                    }
                }

                GameObject gameObjectD = GameObject.Find("TMP SubMesh [LiberationSans SDF Material]");
                gameObjectD.SetActive(false);
            }
        }

        public void UpdateCoc()
        {
            string Heading = File.ReadAllText(this.pathABC);
            string textA = File.ReadAllText(this.pathA);
            bool flag = File.Exists(this.pathA);
            if (flag)
            {
                GameObject gameObject = GameObject.Find("COCBodyText_TitleData");
                bool flag1 = gameObject != null;
                if (flag1)
                {
                    TextMeshPro component = gameObject.GetComponent<TextMeshPro>();
                    bool flag2 = component != null;
                    if (flag2)
                    {
                        component.text = textA;
                        this.lastTxt = textA;
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
                        componentA.text = Heading;
                        this.lastTxt = textA;
                    }
                }
            }
        }
    }
}
