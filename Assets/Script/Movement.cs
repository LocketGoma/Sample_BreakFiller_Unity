using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform cameraTransform;
    private Vector3 Gravity = Vector3.down * 9.81f;

    private Rigidbody rbody;
    // Start is called before the first frame update

    private void Start() {
        cameraTransform = transform.GetChild(0);
        rbody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().freezeRotation = true;            //빙글빙글 도는것 방지.
        GetComponent<Rigidbody>().useGravity = false;

    }


    //뭔가 많이 많이 석연찮은데
    void Update()
    {
        Vector3 gravityForward = Vector3.Cross(Gravity, transform.right);
        Quaternion targetRotation = Quaternion.LookRotation(gravityForward, -Gravity);
        GetComponent<Rigidbody>().rotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, 0.1f);


        Vector3 forward = Vector3.Cross(transform.up, -cameraTransform.right).normalized;         //상하 힘 *            // transform.up = Y축
        Vector3 right = Vector3.Cross(transform.up, cameraTransform.forward).normalized;          //좌우 힘 *

        Vector3 BaseVelocity = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")) * speed;
        Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
        Vector3 ChangeVelocity = transform.InverseTransformDirection(BaseVelocity) - localVelocity;




        GetComponent<Rigidbody>().AddForce(ChangeVelocity, ForceMode.VelocityChange);
        GetComponent<Rigidbody>().AddForce(Gravity * GetComponent<Rigidbody>().mass);

        //transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime,0f, Input.GetAxis("Vertical") * speed * Time.deltaTime);

        //transform.localRotation = Quaternion.Euler(new Vector2(0,transform.GetChild(0).localRotation.eulerAngles.y));

    }
}
