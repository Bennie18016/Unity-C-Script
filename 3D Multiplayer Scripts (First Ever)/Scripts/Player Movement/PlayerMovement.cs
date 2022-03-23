using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PhotonView PV;
    CharacterController myCC;
    public float moveSpeed;
    public float rotationSpeed;
    Vector3 moveDirection = Vector3.zero;
    public Camera cam;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();

        if (!PV.IsMine)
        {
            cam.enabled = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        if (PV.IsMine)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = moveSpeed * Input.GetAxis("Vertical");
            float curSpeedY = moveSpeed * Input.GetAxis("Horizontal");
            moveDirection = (-forward * curSpeedX) + (-right * curSpeedY);
            myCC.Move(moveDirection * Time.deltaTime);

            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.Rotate(new Vector3(0, mouseX, 0));
        }
    }
}
