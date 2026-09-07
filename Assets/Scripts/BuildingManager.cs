using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    // 소환될 건물 프리펩 변수
    public GameObject buildingPrefab;

    private Camera mainCam;

    private GameObject selectedBuilding;

    private Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        // 마우스 클릭할 때 딱 한번만 실행
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치에 레이저를 쏨
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 만약 맞힌 물체가 "Building" 태그를 가지고 있다면
                if (hit.collider.CompareTag("Building"))
                {
                    // 태그를 가진 물체를 '선택된 건물'로 저장
                    selectedBuilding = hit.collider.gameObject;
                }
                // 건물이 아닌곳을 클릭하면
                else
                {
                    // 소수를 반올림해서 정수로 만듦
                    int x = Mathf.RoundToInt(hit.point.x);
                    int z = Mathf.RoundToInt(hit.point.z);

                    // 계산된 좌표가 맵 안일때
                    if (x >= 0 && x <= 10 && z >= 0 && z <= 10)
                    {
                        // 대충 0.5로 안하면 건물이 바닥에 파묻히니깐
                        Instantiate(buildingPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);
                    }
                }
            }
        }

        // 마우스를 계속 누르고 있을때
        // 선택된 건물이 없을때
        if (Input.GetMouseButton(0) && selectedBuilding != null)
        {
            // 마우스 커서 위치에 레이저를 쏨
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);

                int x = Mathf.Clamp(Mathf.RoundToInt(hitPoint.x), 0, 10);
                int z = Mathf.Clamp(Mathf.RoundToInt(hitPoint.z), 0, 10);

                selectedBuilding.transform.position = new Vector3(x, selectedBuilding.transform.position.y, z);
            }
        }

        // 마우스 좌클릭 후 손을 떗을때
        if (Input.GetMouseButtonUp(0))
        {
            selectedBuilding = null;
        }
    }
}