using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCamera;
    [SerializeField] private GameObject containerToFollow;


    private float cameraDefaultLens = 14.2f;
    private float cameraBossRoomLens = 17f;

    // Start is called before the first frame update
    void Start()
    {
        //vCamera.m_Lens.OrthographicSize = 14f;
        vCamera.m_Lens.OrthographicSize = cameraDefaultLens;
        vCamera.Follow = containerToFollow.transform;
    }

    public void SetCameraTargetPosition(Vector2Int position)
    {
        containerToFollow.transform.localPosition = new Vector3(position.x + 0.5f, position.y + 0.5f, 0f);
    }

    public void SetCameraPosition(Vector2Int position)
    {
        vCamera.transform.localPosition = new Vector3(position.x + 0.5f, position.y + 0.5f, 0f);
    }

    public void SetCameraToFollow(GameObject toFollow)
    {
        vCamera.Follow = toFollow.transform;
        vCamera.m_Lens.OrthographicSize = cameraBossRoomLens;
    }

    public void ResetCameraToFollowTarget()
    {
        vCamera.Follow = containerToFollow.transform;
        vCamera.m_Lens.OrthographicSize = cameraDefaultLens;

    }

    public void TriggerCameraShakeFor(float duration, float amplitude, float frequency)
    {
        StartCoroutine(DisableCameraShakeAfter(duration, amplitude, frequency));
    }

    private IEnumerator DisableCameraShakeAfter(float duration, float amplitude, float frequency)
    {
        CinemachineBasicMultiChannelPerlin perlin = vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = amplitude;
        perlin.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        perlin.m_AmplitudeGain = 0f;
        perlin.m_FrequencyGain = 0f;

    }


}
