using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPulse : MonoBehaviour
{
    [SerializeField] private LayerMask Astroids;
    [SerializeField] private GameObject BlastEffect;
    [SerializeField] private Vector3 BubbleChange, Default;
    [SerializeField] private bool Blastable = true;
    [SerializeField] private int BlastRange = 1000;
    [SerializeField] private int BubbleSize = 50;
    [SerializeField] private int CooldownTime = 10;
    void Start()
    {
        Default = new Vector3(1, 1, 1);

        BubbleChange = Default;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Upgrade.Instance.Pulse == true && Blastable == true)
        {
            // Find asteroids
            Collider[] astroids = Physics.OverlapSphere(transform.position, BlastRange, Astroids);
            for (int i = 0; i < astroids.Length; i++)
            {
                // Push the asteroid
                astroids[i].GetComponent<Asteroid>().Push(transform.position);
            }

            // Play effects
            BlastEffect.SetActive(true);
            SoundEffect.Instance.playSound(SoundEffect.Instance.explosion);
            StartCoroutine(GrowBubble());
        }
    }
    // Blast effect
    private IEnumerator GrowBubble()
    {
        
        Blastable = false;
        for (int i = 0; i < BubbleSize; i++)
        {
            BubbleChange = new Vector3(i, i, i);

            // Change bubble size
            BlastEffect.transform.localScale = BubbleChange; 
            yield return new WaitForSeconds(0.01f);

        }
        // return bubble to noraml state
        BubbleChange = Default;
        BlastEffect.transform.localScale = Default;
        BlastEffect.SetActive(false);

        yield return new WaitForSeconds(CooldownTime);
        // Cooldown over
        Blastable = true;
    }
}
