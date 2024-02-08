/*
 * @author nf0101
 */


using Meta.WitAi.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static OVRFaceExpressions.FaceExpression;

public class TestExpr : MonoBehaviour
{
    [SerializeField]
    private OVRFaceExpressions faceExpressions;

    [SerializeField]
    private Transform faceWeightLayout;
    public int expressionChoosed;
    public TMP_Text espressione;
    public string expressionChoosedString;
    public string espressioneString;
    public GameObject player;
    public GameObject eventSys;
    SceneController controller;
    public List<TMP_Text> listText;
    private List<OVRFaceExpressions.FaceExpression> rageExprList;
    bool trackingExpression = true;
    private Soggetto subject;
    private SoggettoList subjectList;
    private string filePath;
    Dictionary<string, float> jsonMap = new();
    public bool timer = false;
    public float timeElapsed = 0.0f;
    public RawImage expressionimage;


    private Dictionary<OVRFaceExpressions.FaceExpression, bool> expressionsList = new Dictionary<OVRFaceExpressions.FaceExpression, bool>();
    //OVRFaceExpressions.FaceExpression[] ExprArray = (OVRFaceExpressions.FaceExpression[])Enum.GetValues(typeof(OVRFaceExpressions.FaceExpression));

    [System.Serializable]
    public class Soggetto
    {
        public string id, expression;
        public Dictionary<string, float> expressionMap = new();

        public Soggetto() { }
        public Soggetto(string id, string expression, Dictionary<string, float> expressionMap)
        {
            this.id = id;
            this.expression = expression;
            this.expressionMap = expressionMap;
        }


    }

    [System.Serializable]
    public class SoggettoList
    {
        public List<Soggetto> ListaSoggetti = new List<Soggetto>();

        public SoggettoList() { }
    }

    IEnumerator WaitLoading(int val)
    {
        yield return new WaitForSeconds(3);
        controller.TaskOnClick(val);

    }

    IEnumerator WaitImage(string expr)
    {
        yield return new WaitForSeconds(3);
        expressionimage.enabled = true;
        Texture2D tex = Resources.Load<Texture2D>(expr);
        expressionimage.texture = tex;

    }

    // Start is called before the first frame update
    void Start()
    {       
        GameObject[] texts = GameObject.FindGameObjectsWithTag("Texts");
        foreach (GameObject text in texts)
        {
            listText.Add(text.GetComponent<TMP_Text>());
        }

        expressionChoosedString = "Choose an expression by pressing one of the buttons";
        espressione.text = expressionChoosedString;
        filePath = Application.persistentDataPath + "data.json";
        subjectList = new SoggettoList();
        controller = eventSys.GetComponent<SceneController>();

        if (File.Exists(filePath))
        {
            string str = File.ReadAllText(filePath);
            //print(str);
            subjectList = JsonConvert.DeserializeObject<SoggettoList>(str);
        }

        foreach (OVRFaceExpressions.FaceExpression e in Enum.GetValues(typeof(OVRFaceExpressions.FaceExpression)))
        {
            expressionsList.Add(e, false);
            jsonMap.Add(e.ToString(), 0);
        }

        //Debug purpose
        //rageExprList = new List<OVRFaceExpressions.FaceExpression>() {BrowLowererL, BrowLowererR, LidTightenerL, LidTightenerR, UpperLidRaiserL,
        //        UpperLidRaiserR, LipCornerPullerR, LipCornerPullerL, DimplerL, DimplerR};
       // rageExprList = Enum.GetValues(typeof(OVRFaceExpressions.FaceExpression)).OfType<OVRFaceExpressions.FaceExpression>().ToList();
    }

    //Update is called once per frame
    void Update()
    {
        foreach (OVRFaceExpressions.FaceExpression e in Enum.GetValues(typeof(OVRFaceExpressions.FaceExpression)))
        {
            //Debug purpose
            //for (int j = 0; j < rageExprList.Count; j++)
            //{
            //    float temp;
            //    faceExpressions.TryGetFaceExpressionWeight(rageExprList[j], out temp);
            //    listText[j].SetText($"{rageExprList[j]}: {temp} - {expressionsList[rageExprList[j]]}");
            //}

            if (trackingExpression)
            {

                    espressione.text = expressionChoosedString;
                
                float weight;
                faceExpressions.TryGetFaceExpressionWeight(e, out weight);
                if (weight > 0.30)
                {
                    expressionsList[e] = true;
                }
                else
                {
                    expressionsList[e] = false;
                }
                switch (expressionChoosed)
                {
                    case 1:
                        if (expressionsList[CheekRaiserL] && expressionsList[CheekRaiserL] && expressionsList[LipCornerPullerL] && expressionsList[LipCornerPullerR])
                        {

                            List<string> jsonKeys = jsonMap.Keys.ToList();
                            foreach (string key in jsonKeys)
                            {
                                float w;
                                faceExpressions.TryGetFaceExpressionWeight(Enum.Parse<OVRFaceExpressions.FaceExpression>(key), out w);
                                jsonMap[key] = w;
                            }

                            subject = new(DateTime.Now.Ticks.ToString(), "Felicità", jsonMap);
                            subjectList.ListaSoggetti.Add(subject);
                            saveData();
                            StartCoroutine(WaitLoading(1));
                            trackingExpression = false;

                        }
                        break;
                    case 2:
                        float temp;
                        faceExpressions.TryGetFaceExpressionWeight(JawDrop, out temp);
                        if (expressionsList[InnerBrowRaiserL] && expressionsList[InnerBrowRaiserR] && expressionsList[OuterBrowRaiserL] && expressionsList[OuterBrowRaiserR] && expressionsList[UpperLidRaiserL] &&
                        expressionsList[UpperLidRaiserR] && temp > 0.1)
                        {


                            List<string> jsonKeys = jsonMap.Keys.ToList();
                            foreach (string key in jsonKeys)
                            {
                                float w;
                                faceExpressions.TryGetFaceExpressionWeight(Enum.Parse<OVRFaceExpressions.FaceExpression>(key), out w);
                                jsonMap[key] = w;
                            }

                            subject = new(DateTime.Now.Ticks.ToString(), "Sorpresa", jsonMap);
                            subjectList.ListaSoggetti.Add(subject);
                            saveData();
                            StartCoroutine(WaitLoading(1));
                            trackingExpression = false;
                        }
                        break;
                    case 3:
                        if (((expressionsList[InnerBrowRaiserL] && expressionsList[InnerBrowRaiserR]) || (expressionsList[BrowLowererL] && expressionsList[BrowLowererR])) && expressionsList[LipCornerDepressorL] &&
                        expressionsList[LipCornerDepressorR])
                        {
                            List<string> jsonKeys = jsonMap.Keys.ToList();
                            foreach (string key in jsonKeys)
                            {
                                float w;
                                faceExpressions.TryGetFaceExpressionWeight(Enum.Parse<OVRFaceExpressions.FaceExpression>(key), out w);
                                jsonMap[key] = w;
                            }

                            subject = new(DateTime.Now.Ticks.ToString(), "Tristezza", jsonMap);
                            subjectList.ListaSoggetti.Add(subject);
                            saveData();
                            StartCoroutine(WaitLoading(3));
                            trackingExpression = false;
                        }
                        break;
                    case 4:
                        if (((expressionsList[InnerBrowRaiserL] && expressionsList[InnerBrowRaiserR] && expressionsList[OuterBrowRaiserL] && expressionsList[OuterBrowRaiserR] &&
                        expressionsList[UpperLidRaiserL] && expressionsList[UpperLidRaiserR]) || (expressionsList[BrowLowererL] && expressionsList[BrowLowererR] && expressionsList[LidTightenerL] &&
                        expressionsList[LidTightenerR])) && ((expressionsList[LipStretcherL] && (expressionsList[LipStretcherR]) || expressionsList[JawDrop])))
                        {

                            List<string> jsonKeys = jsonMap.Keys.ToList();
                            foreach (string key in jsonKeys)
                            {
                                float w;
                                faceExpressions.TryGetFaceExpressionWeight(Enum.Parse<OVRFaceExpressions.FaceExpression>(key), out w);
                                jsonMap[key] = w;
                            }

                            subject = new(DateTime.Now.Ticks.ToString(), "Paura", jsonMap);
                            subjectList.ListaSoggetti.Add(subject);
                            StartCoroutine(WaitLoading(4));
                            saveData();
                            trackingExpression = false;

                        }
                        break;

                    case 5:
                        float tempLipTL, tempLipTR, chinRT, chinRB;
                        faceExpressions.TryGetFaceExpressionWeight(LipTightenerL, out tempLipTL);
                        faceExpressions.TryGetFaceExpressionWeight(LipTightenerR, out tempLipTR);
                        faceExpressions.TryGetFaceExpressionWeight(ChinRaiserT, out chinRT);
                        faceExpressions.TryGetFaceExpressionWeight(ChinRaiserB, out chinRB);

                        if (expressionsList[BrowLowererL] && expressionsList[BrowLowererR] && ((expressionsList[LidTightenerL] && expressionsList[LidTightenerR]) || (expressionsList[UpperLidRaiserL] &&
                        expressionsList[UpperLidRaiserR])) && ((tempLipTL > 0.1f || tempLipTR > 0.1) || (chinRT > 0.1 || chinRB > 0.1)))
                        {
                            List<string> jsonKeys = jsonMap.Keys.ToList();
                            foreach (string key in jsonKeys)
                            {
                                float w;
                                faceExpressions.TryGetFaceExpressionWeight(Enum.Parse<OVRFaceExpressions.FaceExpression>(key), out w);
                                jsonMap[key] = w;
                            }

                            subject = new(DateTime.Now.Ticks.ToString(), "Rabbia", jsonMap);
                            subjectList.ListaSoggetti.Add(subject);

                            saveData();
                            StartCoroutine(WaitLoading(2));
                            trackingExpression = false;
                        }
                        break;
                    case 6:
                        if (expressionsList[NoseWrinklerL] && expressionsList[NoseWrinklerR] && ((expressionsList[LipCornerDepressorL] && expressionsList[LipCornerDepressorR]) ||
                        (expressionsList[LowerLipDepressorL] && expressionsList[LowerLipDepressorR])))
                        {
                            List<string> jsonKeys = jsonMap.Keys.ToList();
                            foreach (string key in jsonKeys)
                            {
                                float w;
                                faceExpressions.TryGetFaceExpressionWeight(Enum.Parse<OVRFaceExpressions.FaceExpression>(key), out w);
                                jsonMap[key] = w;
                            }

                            subject = new(DateTime.Now.Ticks.ToString(), "Disgusto", jsonMap);
                            subjectList.ListaSoggetti.Add(subject);
                            saveData();
                            StartCoroutine(WaitLoading(2));
                            trackingExpression = false;
                        }

                        break;

                    case 7:
                        float tempLipCL, tempLipCR, dimplerL, dimplerR, uppLL, uppLR;
                        faceExpressions.TryGetFaceExpressionWeight(LipCornerPullerL, out tempLipCL);
                        faceExpressions.TryGetFaceExpressionWeight(LipCornerPullerR, out tempLipCR);
                        faceExpressions.TryGetFaceExpressionWeight(DimplerL, out dimplerL);
                        faceExpressions.TryGetFaceExpressionWeight(DimplerR, out dimplerR);
                        faceExpressions.TryGetFaceExpressionWeight(UpperLipRaiserL, out uppLL);
                        faceExpressions.TryGetFaceExpressionWeight(UpperLipRaiserR, out uppLR);

                        if ((tempLipCL > 0.02 ^ tempLipCR > 0.02) && (dimplerL > 0.01 ^ dimplerR > 0.01) &&
                            (uppLL > 0.2 ^ uppLR > 0.2))
                        {
                            List<string> jsonKeys = jsonMap.Keys.ToList();
                            foreach (string key in jsonKeys)
                            {
                                float w;
                                faceExpressions.TryGetFaceExpressionWeight(Enum.Parse<OVRFaceExpressions.FaceExpression>(key), out w);
                                jsonMap[key] = w;
                            }

                            subject = new(DateTime.Now.Ticks.ToString(), "Disprezzo", jsonMap);
                            subjectList.ListaSoggetti.Add(subject);
                            saveData();
                            StartCoroutine(WaitLoading(2));
                            trackingExpression = false;
                        }
                        break;
                }

                if (timer)
                {
                    timeElapsed += Time.deltaTime;
                }
                if (((int)(timeElapsed / 60)) > 14)
                {
                    timer = false;
                    timeElapsed = 0;
                    expressionChoosedString = "Try like this:";
                    StartCoroutine(WaitImage(espressioneString));
                }
                
               
                //else
                //{
                //    espressione.text = "Neutro";
                //}
            }
            else
            {
                expressionimage.enabled = false;
                espressione.text = "Correct expression! Good job!";
                timer = false;
            }

        }
    }
    void saveData()
    {
        string str = JsonConvert.SerializeObject(subjectList);
        File.WriteAllText(filePath, str);
        Debug.Log($"Data exported with success to {filePath}!");
    }
}
