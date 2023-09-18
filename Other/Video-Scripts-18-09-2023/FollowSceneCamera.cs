using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR

/// <summary>
///     Simple script to follow the scene camera, and allows for rendering only view frustum objects
/// </summary>
public class FollowSceneCamera : MonoBehaviour
{
    private Camera _gameCamera;
    private bool _shouldUpdate = false;
    private bool _viewFrustumVisible = true;
    private List<MeshCollider> _renderers = new List<MeshCollider>();

    private void Start()
    {
        _gameCamera = GetComponent<Camera>();
        _renderers.AddRange(FindObjectsOfType<MeshCollider>());
    }

    private void Update()
    {
        FollowSceneCameraImpl();
        UpdateViewFrustumRendering();
        
        // Move the camera using WSAD and mouse when right mouse button is pressed
        float speed = 10;
        if(Input.GetKey(KeyCode.LeftShift)) speed *= 10;
        
        float mouseSpeed = 100;
        float dt = Time.deltaTime;
        if (Input.GetKey(KeyCode.W)) transform.position += transform.forward * speed * dt;
        if (Input.GetKey(KeyCode.S)) transform.position -= transform.forward * speed * dt;
        if (Input.GetKey(KeyCode.A)) transform.position -= transform.right * speed * dt;
        if (Input.GetKey(KeyCode.D)) transform.position += transform.right * speed * dt;
        
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * mouseSpeed * dt, Space.World);
            transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * mouseSpeed * dt, Space.Self);
        }
    }

    /// <summary>
    ///     Update the rendering of the view frustum
    /// </summary>
    private void UpdateViewFrustumRendering()
    {
        if(_viewFrustumVisible)
        {
            foreach (var renderer in _renderers)
            {
                renderer.GetComponent<Renderer>().enabled = true;
            }

            return;
        }

        // Read light direction so that we can calculate the shadow length, and use that (it's only an approximation)
        var sun = FindObjectOfType<Light>();
        var sunDirection = sun.transform.forward;
        var sunAngle = Vector3.Angle(sunDirection, transform.forward);
        
        
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_gameCamera);
        // if renderer is not visible, disable it
        foreach (var renderer in _renderers)
        {
            // using sun data, calculate new bounds of the renderer, to include it's shadow
            // Calculate new bounds of the renderer, to include its shadow
            var boundsWithShadow = renderer.bounds;

            // Calculate the shadow length based on the sun angle
            float shadowLength = boundsWithShadow.extents.y / Mathf.Tan(sunAngle * Mathf.Deg2Rad);

            // Create a vector that represents the shadow
            Vector3 shadowVector = sunDirection * shadowLength;

            // Add the shadow vector to the bounds
            boundsWithShadow.Encapsulate(new Bounds(boundsWithShadow.center + shadowVector, boundsWithShadow.size));

            renderer.GetComponent<Renderer>().enabled = GeometryUtility.TestPlanesAABB(planes, boundsWithShadow);
        }
    }

    /// <summary>
    ///     Follow the scene camera
    /// </summary>
    private void FollowSceneCameraImpl()
    {
        if (!_shouldUpdate) return;

        var sceneCameraTransform = SceneView.lastActiveSceneView.camera.transform;
        transform.position = sceneCameraTransform.position;
        transform.rotation = sceneCameraTransform.rotation;
    }

    /// <summary>
    ///     Show a GUI to toggle the script on/off
    /// </summary>
    private void OnGUI()
    {
        // Show a button to toggle the script on/off
        if (GUI.Button(new Rect(10, 10, 100, 20), _shouldUpdate
                ? "Disable"
                : "Enable")) _shouldUpdate = !_shouldUpdate;
        // Show a button to toggle the view frustum on/off
        if (GUI.Button(new Rect(10, 40, 100, 20), _viewFrustumVisible
                ? "Hide Frustum"
                : "Show Frustum")) _viewFrustumVisible = !_viewFrustumVisible;
        
        // Show number of enabled renderers in the scene
        GUI.Label(new Rect(10, 70, 200, 20), "Enabled renderers: " + _renderers.Select(x => x.GetComponent<Renderer>()).Count(r => r.enabled));
    }

    /// <summary>
    ///     Draw the view frustum using Gizmos
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!_viewFrustumVisible) return;

        // Draw the view frustum using Gizmos
        DrawViewFrustum();
    }

    /// <summary>
    ///     Draw the view frustum using Handles and Gizmos
    /// </summary>
    private void DrawViewFrustum()
    {
        var cam = _gameCamera;
        if (cam == null) return;

        Gizmos.color = Color.red;

        // Calculate the corners of the near clip plane
        Vector3[] nearCorners = new Vector3[4];
        nearCorners[0] = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        nearCorners[1] = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        nearCorners[2] = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        nearCorners[3] = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));

        // Draw rays from the camera's position to each corner of the near clip plane
        foreach (Vector3 corner in nearCorners)
        {
            Ray ray = new Ray(cam.transform.position, corner - cam.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // If the ray hit something, draw it to the point of contact
                Gizmos.DrawLine(ray.origin, hit.point);
            }
            else
            {
                // Otherwise, draw it to the near clip plane
                Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * cam.farClipPlane);
            }
        }
    }
}
#endif