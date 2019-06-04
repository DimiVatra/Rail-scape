using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Transform PlayerCamera;
    public Transform MirrorCamera;
    public Transform MirrorCameraOrigin;
    public Transform MirrorSurface;
    public Transform CameraSymmetric;
    public Transform CameraLookAt;
    public Transform LeftMarginOfMirror;
    public Transform RightMarginOfMirror;
    public Transform LookToLeftMargin;
    public Transform LookToRightMargin;
    public float param;
    // Update is called once per frame
    void Update()
    {
        ProjectTransformOnPlane(PlayerCamera, transform.position, new Vector3(1,0,0));
        //Vector3 dir = (PlayerCamera.position - transform.position).normalized;
        //Quaternion rot = Quaternion.LookRotation(dir);
        //rot.eulerAngles = transform.eulerAngles - rot.eulerAngles;
       // MirrorCamera.localRotation = rot;
    }

    private void ProjectTransformOnPlane(Transform objectToProject, Vector3 planeOrigin, Vector3 planeNormal)
    {
        Plane projectionPlane = new Plane(planeNormal, planeOrigin);
        float distanceToIntersection;
        Ray intersectionRay = new Ray(objectToProject.position, -planeNormal);
        if (projectionPlane.Raycast(intersectionRay, out distanceToIntersection))
        {
            CameraSymmetric.position = objectToProject.position - 2*planeNormal * (distanceToIntersection);
            MirrorCamera.gameObject.GetComponent<Camera>().nearClipPlane = Vector3.Distance(CameraLookAt.position, MirrorCamera.position);
            MirrorCamera.LookAt(CameraLookAt.position);
            MirrorCamera.gameObject.GetComponent<Camera>().fieldOfView = linear();

        }
    }
    private float linear()
    {
        LookToLeftMargin.LookAt(LeftMarginOfMirror);
        LookToRightMargin.LookAt(RightMarginOfMirror);
        param = Vector3.Angle(LookToRightMargin.forward, LookToLeftMargin.forward);
        return param+10;
    }
}
