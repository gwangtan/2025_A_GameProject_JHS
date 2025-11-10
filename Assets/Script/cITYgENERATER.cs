using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class cITYgENERATER : EditorWindow
{
    // Start is called before the first frame update
    private int gridSizeX = 10;
    private int gridSizeY = 10;
    private int gridSizeZ = 10;
    private float buildingSpacing = 15;
    private float roadWidth = 5f;
    private bool makeStatic = true;

    [MenuItem("Tools/City Generator")]
    public static void ShowWindow()
    {
        GetWindow<cITYgENERATER>("CityGenerator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Simple City Generator",EditorStyles.boldLabel);
         
        gridSizeX = EditorGUILayout.IntField("Grid SizeX" , gridSizeX);
        gridSizeZ = EditorGUILayout.IntField("Grid SizeZ", gridSizeZ);
        buildingSpacing =EditorGUILayout.FloatField("Building Spacing", buildingSpacing);
        roadWidth = EditorGUILayout.FloatField("Road Width", roadWidth);
        makeStatic = EditorGUILayout.Toggle("Make Static", makeStatic);

        GUILayout.Space(10);

        if (GUILayout.Button("Generate City"))
        {
            GenerateCity();
        }

        if (GUILayout.Button("Clear city"))
        {
            ClearCity();
        }
        
    }




    private void CreateBuilding(Vector3 position , Transform parent)
    {
        GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
        building.name = "Building";

        float height = Random.Range(5.0f, 20.0f);
        building.transform.position = position + Vector3.up * height / 2.0f;
        building.transform.localScale = new Vector3(buildingSpacing  - roadWidth -1f , height, buildingSpacing - roadWidth -1f);
        building.transform .SetParent(parent, false);

        Renderer renderer = building.GetComponent<Renderer>();
        renderer.material.color = new Color(Random.Range(0.5f, 0.8f), Random.Range(0.5f, 0.8f), Random.Range(0.5f, 0.8f));

        if (makeStatic)
        {
            building. isStatic = true;
        }

    }



    private void CreateRoad(Vector3 position , Transform parent)
    {
        GameObject road = GameObject.CreatePrimitive(PrimitiveType.Cube);

        road.transform.position = position + Vector3.up * 0.1f;
        road.transform.localScale = new Vector3(buildingSpacing, 0.2f, buildingSpacing);
        road .transform .SetParent(parent);

        Renderer renderer = road.GetComponent<Renderer>();
        renderer.material.color = new Color(0.3f, 0.3f, 0.3f);

        if (makeStatic)
            {
            road.isStatic = true;
        }
    }


    private void ClearCity()
    {
        GameObject city = GameObject.Find("City");
        if(city != null) 
            {
            DestroyImmediate(city);
            Debug.Log("city Cleard");
            }
        else
        {
            Debug.Log("도시가 없습니다.");
        }
    }

    private void GenerateCity()
    { //도시 생성 함수
        GameObject cityParent = new GameObject("City"); //전체 도시를 담을 부모 오브젝트

        GameObject buildingsParent = new GameObject("Buildings"); //건물 묶음 부모
        buildingsParent.transform.SetParent(cityParent.transform, false);

        GameObject roadsParent = new GameObject("Roads"); //도로 묶음 부모
        roadsParent.transform.SetParent(cityParent.transform, false);

        for (int x = 0; x < gridSizeX; x++) //x 방향 반복
        {
            for (int z = 0; z < gridSizeZ; z++) //z 방향 반복
            {
                Vector3 position = new Vector3(x * buildingSpacing, 0, z * buildingSpacing); //각 위치 계산

                if (x % 2 == 0 || z % 2 == 0) //짝수 줄에는 도로 생성
                {
                    CreateRoad(position, roadsParent.transform);
                }
                else
                {
                    CreateBuilding(position, buildingsParent.transform);
                }
            }
        }
    }
}
