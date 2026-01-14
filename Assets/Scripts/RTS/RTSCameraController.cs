using UnityEngine;

public class RTSCameraController : MonoBehaviour
{
    [Header("이동")]
    public float moveSpeed = 12f;
    public float edgeSize = 20f;

    [Header("회전 / 줌")]
    public float rotateSpeed = 90f;
    public float zoomSpeed = 300f;

    void Update()
    {
        Vector3 move = Vector3.zero;

        // WASD 이동
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        move += new Vector3(h, 0, v);

        // Edge Scrolling
        Vector3 mouse = Input.mousePosition;

        if (mouse.x < edgeSize) move += Vector3.left;
        else if (mouse.x > Screen.width - edgeSize) move += Vector3.right;

        if (mouse.y < edgeSize) move += Vector3.back;
        else if (mouse.y > Screen.height - edgeSize) move += Vector3.forward;

        // 실제 이동
        if (move != Vector3.zero)
            transform.Translate(move.normalized * moveSpeed * Time.deltaTime, Space.Self);

        // 회전(Q/E)
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);

        // 줌
        float scroll = Input.mouseScrollDelta.y;
        transform.Translate(Vector3.forward * scroll * zoomSpeed * Time.deltaTime, Space.Self);
    }
}
