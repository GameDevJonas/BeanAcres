using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using RDG;

public class HarvestingBehaviour : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Transform harvestSliderTransform;
    public Image slider, arrow;

    public GameObject harvestingParticles;
    public Material carrotMat, strawberryMat, aubergineMat;

    public Vector2 lastPosition;

    public Vector2 direction;

    public bool inDrag;

    public float harvestingTime, timer;

    private SoilBehaviour mySoil;

    private Transform objectToTest;

    // Start is called before the first frame update
    void Start()
    {
        mySoil = GetComponent<SoilBehaviour>();
        objectToTest = new GameObject("PlaceHolder").transform;
    }

    private void OnEnable()
    {
        harvestSliderTransform.gameObject.SetActive(true);
        arrow.gameObject.SetActive(false);
        direction = Vector2.zero;
        timer = 0;
    }

    private void OnDisable()
    {
        harvestSliderTransform.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= harvestingTime)
        {
            arrow.gameObject.SetActive(true);
            if (direction.y > 100)
            {
                ParticleSystemRenderer particles = Instantiate(harvestingParticles.gameObject, transform.position, Quaternion.identity).GetComponent<ParticleSystemRenderer>();
                switch (mySoil.myPlant)
                {
                    case SwapTools.Plants.carrot:
                        particles.material = carrotMat;
                        break;
                    case SwapTools.Plants.strawberry:
                        particles.material = strawberryMat;
                        break;
                    case SwapTools.Plants.aubergine:
                        particles.material = aubergineMat;
                        break;
                }
                particles.GetComponent<ParticleSystem>().Play();
                Destroy(particles.gameObject, 2f);
                GetComponent<SoilBehaviour>().HarvestNow();
                this.enabled = false;
            }
        }
        else
        {
            timer += Time.deltaTime;
            harvestSliderTransform.position = Input.mousePosition;
            slider.fillAmount = timer / harvestingTime;
            //Vibration.Vibrate(40, 10, true);
            //VibrationMethods.ShortLowVibration();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        VibrationMethods.ShortLowVibration();
        inDrag = true;
        lastPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (timer >= harvestingTime)
            direction = (eventData.position - lastPosition);
        lastPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inDrag = false;
        //if (direction.x == 0) direction = new Vector2(direction.x, direction.y / direction.y);
        //if (direction.y == 0) direction = new Vector2(direction.x / direction.x, direction.y);
        //if (direction.y == 0 && direction.x == 0) direction = Vector2.zero;
        harvestSliderTransform.gameObject.SetActive(false);
        this.enabled = false;
    }
}
