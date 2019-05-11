using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon = null;
    [SerializeField]
    private float _cooldown = 0;
    internal float Cooldown { get { return _cooldown; } set { _cooldown = value; } }

    [SerializeField]
    Transform forwardGunTransform;
    [SerializeField]
    Transform torpedoTransform1;
    [SerializeField]
    Transform torpedoTransform2;

    void Update()
    {
        if (Input.GetButton("Fire1") && _cooldown <= 0)
        {
            if (weapon != null)
            {
                var go = Instantiate(weapon, transform.position, transform.rotation);
                if (go.GetComponent<ForwardGunSlot>())
                    go.transform.position = forwardGunTransform.position;
                else if (go.GetComponent<TorpedoSlot>())
                    go.transform.position = torpedoTransform1.position;
            }
        }
    }
}
