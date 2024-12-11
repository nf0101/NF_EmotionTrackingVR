/*
 * @author nf0101
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Button = UnityEngine.UI.Button;

public class SceneController : MonoBehaviour
{
    public Button HappyButton, AngerButton, SadButton, NeutralButton, ScareButton;
    public GameObject FiltersFather;
    public List<GameObject> FiltersList;
    public Material[] Skyboxes, Rivermaterial;
    public GameObject River;
    private Renderer RiverRenderer;
    public GameObject HappySounds;
    public GameObject SpawnPoint;
    public GameObject Player;
    private GameObject lightBox;
    private bool inPosition = false;
    private Volume volume;

   // Start is called before the first frame update
        void Start()
    {
        NeutralButton.onClick.AddListener(delegate { TaskOnClick(0); });
        HappyButton.onClick.AddListener(delegate { TaskOnClick(1); });
        AngerButton.onClick.AddListener(delegate { TaskOnClick(2); });
        SadButton.onClick.AddListener(delegate { TaskOnClick(3); });
        ScareButton.onClick.AddListener(delegate { TaskOnClick(4); });
        lightBox = FiltersList.Find(x => x.gameObject.name.Equals("LightBox"));
        volume = lightBox.GetComponent<Volume>();

        RiverRenderer = River.gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inPosition) {      
            if (volume.weight > 0)
            {
                volume.weight -= 0.08f * Time.deltaTime;
            }
        }
        
    }

    public void TaskOnClick(int SceneVal)
    {
        RenderSettings.skybox = Skyboxes[SceneVal];
        RiverRenderer.material = Rivermaterial[SceneVal];
        HappySounds.gameObject.SetActive(false);
        RenderSettings.fogStartDistance = 10.0f;
        RenderSettings.fogEndDistance = 150.0f;
        RenderSettings.fogColor = new Color32(129, 220, 255, 255);

        foreach (AudioSource e in FindObjectsOfType<AudioSource>()) {
            e.Stop();
        }

        GameObject lightBox = FiltersList.Find(x => x.gameObject.name.Equals("LightBox"));
        foreach (GameObject go in FiltersList)
        {
            if (go != lightBox)
            {
                go.SetActive(false);
            } 
        }

        Player.transform.position = SpawnPoint.transform.position;
        Player.transform.rotation = Quaternion.Euler(0, 90f, 0);
        inPosition = true;
        switch (SceneVal)
        {
            case 0:
                
                
                break;
            case 1:
                foreach (GameObject go in FiltersList)
                {
                    if (go != lightBox)
                    {
                        go.SetActive(false);
                    }
                }
                HappySounds.gameObject.SetActive(true);
                break;

            case 2:
                foreach (GameObject go in FiltersList)
                {
                    if (go != lightBox)
                    {
                        go.SetActive(false);
                    }
                }

                Debug.Log(FiltersList.Find(x => x.gameObject.name.Equals("AngerBox")));
                FiltersList.Find(x => x.gameObject.name.Equals("AngerBox")).SetActive(true);
                break;

            case 3:
                foreach (GameObject go in FiltersList)
                {
                    if (go != lightBox)
                    {
                        go.SetActive(false);
                    }
                }

                Debug.Log(FiltersList.Find(x => x.gameObject.name.Equals("SadBox")));
                FiltersList.Find(x => x.gameObject.name.Equals("SadBox")).SetActive(true);
                break;

            case 4:
                RenderSettings.fog = true;
                RenderSettings.fogStartDistance = -80.0f;
                RenderSettings.fogEndDistance = 55.0f;
                RenderSettings.fogColor = Color.gray;

                FiltersList.Find(x => x.gameObject.name.Equals("ScaryBox")).SetActive(true);
                break;
        }
    }
}
