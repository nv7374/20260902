using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 15f;

    // 줌인 속도
    public float zoomSpeed = 500f;

    // 최대 줌인 제한
    public float minZoom = 5f;

    // 최대 줌아웃 제한
    public float maxZoom = 20f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v) * (panSpeed * Time.deltaTime);

        transform.Translate(move, Space.World);

        // 마우스 휠 스크롤을 입력받음
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // 마우스 휠을 굴렸을 때 실행
        if (scroll != 0f)
        {
            Vector3 pos = transform.position;

            pos.y = Mathf.Clamp(pos.y - scroll * zoomSpeed * Time.deltaTime, minZoom, maxZoom);

            transform.position = pos;
        }
    }
}