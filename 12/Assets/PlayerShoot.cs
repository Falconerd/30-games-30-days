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
    private Transform forwardGunTransform;
    [SerializeField]
    private Transform torpedoTransform1;
    [SerializeField]
    private Transform torpedoTransform2;
    private float cooldownClock = 0;

    void Update()
    {
        cooldownClock -= Time.deltaTime;

        if (Input.GetButton("Fire1") && cooldownClock <= 0)
        {
            cooldownClock = _cooldown;
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
