using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteController : MonoBehaviour
{
    private GameObject player;
    private Vignette vignette;
    private PostProcessVolume volume;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        vignette = ScriptableObject.CreateInstance<Vignette>();
        // Color color = new Color(31, 25, 41);
        vignette.enabled.Override(true);
        // vignette.color.Override(color);
        vignette.intensity.Override(1f);

        volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            vignette.intensity.Override(-player.transform.position.y / 80);
    }
}
