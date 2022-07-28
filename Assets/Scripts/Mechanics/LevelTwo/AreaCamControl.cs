using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCamControl : MonoBehaviour
{
    public enum AreaType { entrance, exit };
    public AreaType areaType;

    public float areaFOV;

    [SerializeField] Cinemachine.CinemachineVirtualCamera CM1;
    [SerializeField] Mechanics.TieCameraControl tieCamCtrl;
    [SerializeField] GameObject camBlocks;

    int numberOfCharacters = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BasicControl>())
        {
            numberOfCharacters++;
            if (areaType == AreaType.entrance && numberOfCharacters == 2)
            {
                CM1.Follow = transform.parent.transform;
                CM1.LookAt = transform.parent.transform;
                camBlocks.SetActive(true);
                tieCamCtrl.targetCamFOV = areaFOV;

            }
            else if (areaType == AreaType.exit && numberOfCharacters == 2)
            {
                CM1.Follow = tieCamCtrl.transform;
                CM1.LookAt = tieCamCtrl.transform;
                camBlocks.SetActive(false);
                tieCamCtrl.targetCamFOV = areaFOV;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BasicControl>())
        {
            numberOfCharacters--;
        }
    }
}
