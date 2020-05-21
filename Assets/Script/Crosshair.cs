using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//레이캐스트 잔뜩
public class Crosshair : MonoBehaviour
{
    [Header("Base Setting")]
    [SerializeField] private Camera camera;
    [Range(0.1f, 25.0f)]
    public float Reach = 25.0F;          //인식 사거리

    [Header("CrossHair")]
    [SerializeField] private GameObject normalCrosshairPrefab;
    [SerializeField] private GameObject selectedCrosshairPrefab;
    [HideInInspector] private GameObject normalCrosshairPrefabInstance;         //기본 크로스헤어 복사본
    [HideInInspector] private GameObject selectedCrosshairPrefabInstance;       //선택 크로스헤어 복사본


    [Header("Debug Setting")]
    [SerializeField] private bool isOn = true;
    [SerializeField] private Color debugRayColor;   //디버그 레이 시각화 색상
    [Range(0.0f, 1.0f)]
    [SerializeField] private float opacity = 1.0f;   //투명도

    // Start is called before the first frame update
    void Start()
    {
        if (normalCrosshairPrefab != null) {
            normalCrosshairPrefabInstance = normalCrosshairPrefab;
        } else {
            Debug.LogError("NormalCrosshair Image was not found!!");
        }
        if (selectedCrosshairPrefab != null) {
            selectedCrosshairPrefabInstance = selectedCrosshairPrefab;
        }
        else {
            Debug.LogError("SelectedCrosshair Image was not found!!");
        }


        debugRayColor.a = opacity;

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));       //메인 카메라

        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, Reach)) {
            Debug.Log("Ray hit : "+hit.collider.tag);
        }

        if (isOn == true) {
            Debug.DrawRay(ray.origin, ray.direction * Reach, debugRayColor);
        }

    }
}
