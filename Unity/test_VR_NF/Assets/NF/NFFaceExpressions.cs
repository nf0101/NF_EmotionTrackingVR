/*
 * @author nf0101
 */

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static OVRFaceExpressions.FaceExpression;

public class NFFaceExpressions : MonoBehaviour
{
    [SerializeField]
    private OVRFaceExpressions faceExpressions;

    [SerializeField]
    private Transform faceWeightLayout;
    
    // Start is called before the first frame update
    private static Dictionary<OVRFaceExpressions.FaceExpression, bool> expressionsList = new Dictionary<OVRFaceExpressions.FaceExpression, bool>();
    float weight;
    static OVRFaceExpressions.FaceExpression[] ExprArray = (OVRFaceExpressions.FaceExpression[])Enum.GetValues(typeof(OVRFaceExpressions.FaceExpression));
    public GameObject cube;
    Renderer r;
    public TMP_Text text2;
    public Text text3;
    void Start()
    {
        r = cube.gameObject.GetComponent<Renderer>();
        foreach (OVRFaceExpressions.FaceExpression e in Enum.GetValues(typeof(OVRFaceExpressions.FaceExpression)))
        {
            expressionsList.Add(e, false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DetectExpression(ref string result)
    {
        r.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);

        //for (int i = 0; i < 65; i++)
        //{
        //text2.SetText(i.ToString());
        foreach (OVRFaceExpressions.FaceExpression key in expressionsList.Keys) {
            text2.SetText(key.ToString());
            if (faceExpressions.GetWeight(key) > 0.25)
            {
                //expressionsList[key] = true;
                //text2.SetText(key + expressionsList[key].ToString());
            }
            else
            {
                //expressionsList[key] = false;
                //text2.SetText(key + expressionsList[key].ToString());
            }
        }
        if (expressionsList[CheekRaiserL] && expressionsList[CheekRaiserL] && expressionsList[LipCornerPullerL] && expressionsList[LipCornerPullerR])
        {
            result = "Felice/Gioia";
        }
        else if (expressionsList[InnerBrowRaiserL] && expressionsList[InnerBrowRaiserR] && expressionsList[OuterBrowRaiserL] && expressionsList[OuterBrowRaiserR] && expressionsList[UpperLidRaiserL] &&
            expressionsList[UpperLidRaiserR] && expressionsList[JawDrop])
        {
            result = "Sorpresa";
        }

        else if (expressionsList[InnerBrowRaiserL] && expressionsList[InnerBrowRaiserR] && expressionsList[BrowLowererL] && expressionsList[BrowLowererR] && expressionsList[LipCornerDepressorL] &&
            expressionsList[LipCornerDepressorR])
        {
            result = "Triste/Tristezza";
        }

        else if (expressionsList[InnerBrowRaiserL] && expressionsList[InnerBrowRaiserR] && expressionsList[OuterBrowRaiserL] && expressionsList[OuterBrowRaiserR] && expressionsList[BrowLowererL]
            && expressionsList[BrowLowererR] && expressionsList[BrowLowererR] && expressionsList[UpperLidRaiserL] && expressionsList[UpperLidRaiserR] && expressionsList[LidTightenerL] &&
            expressionsList[LidTightenerR] && expressionsList[LipStretcherL] && expressionsList[LipStretcherR])
        {
            result = "Paura";
        }

        else if (expressionsList[BrowLowererL] && expressionsList[BrowLowererR] && expressionsList[BrowLowererR] && expressionsList[UpperLidRaiserL] &&
            expressionsList[UpperLidRaiserR] && expressionsList[LidTightenerL] && expressionsList[LidTightenerR] && expressionsList[LipTightenerL] && expressionsList[LipTightenerR])
        {
            result = "Rabbia";
        }

        else if (expressionsList[NoseWrinklerL] && expressionsList[NoseWrinklerR] && expressionsList[LipCornerDepressorL] && expressionsList[LipCornerDepressorR] &&
            expressionsList[LowerLipDepressorL] && expressionsList[LowerLipDepressorR])
        {
            result = "Disgusto";
        }

        else if ((expressionsList[LipCornerPullerL] || expressionsList[LipCornerPullerR]) && (expressionsList[OuterBrowRaiserL] || expressionsList[OuterBrowRaiserR]))
        {
            result = "Disprezzo";
        }
        else
        {
            result = "Neutro";
        }
    }
}
