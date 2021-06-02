using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    // Tutorial text is only active within the trigger.
    // Is sometimes permanent, hence the bool.
    // Maybe add animation for fade in/ out?
    public Text tutorialText;
    [SerializeField] private string input;

    private static readonly string box = "Box";
    private static readonly string boxTutorial = "Press 'Right Click' to pick up boxes";
    private static readonly string key = "Key";
    private static readonly string keyTutorial = "You can also use 'Right Click' to pick up keys";
    private static readonly string zap1 = "Zap1";
    private static readonly string zap1Tutorial = "Use 'Left Click' to perform a zap";
    private static readonly string zap2 = "Zap2";
    private static readonly string zap2Tutorial = "The zap ability can damage spiders";
    private static readonly string mechanism = "Mechanism";
    private static readonly string mechanismTutorial = "Use 'Left Click' to interact with the Mechanism";
    private static readonly string empty = "Empty";
    private static readonly string emptyTutorial = "";

    private readonly Dictionary<string, string> tutorialDictionary = new Dictionary<string, string>();

    public float activeTime;
    public bool permanent;

    public Animator anim; // Tutorialtexts animator.

    private void Start()
    {
        tutorialDictionary.Add(box, boxTutorial);
        tutorialDictionary.Add(key, keyTutorial);
        tutorialDictionary.Add(zap1, zap1Tutorial);
        tutorialDictionary.Add(zap2, zap2Tutorial);
        tutorialDictionary.Add(mechanism, mechanismTutorial);
        tutorialDictionary.Add(empty, emptyTutorial);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //tutorialText.text = "";
            // SFX here?
            tutorialText.text = tutorialDictionary[input];
            anim.SetBool("isActive", true); // Fade in text
            
            if (!permanent)
            {
                StartCoroutine(WaitAndDeactivate());
            }
        }
    }

    // If the tutorial text isn't permanent it gets changed to "" after specified time.
    // The trigger also gets deactivated.
    private IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(activeTime);
        if (tutorialText.text == tutorialDictionary[input])
        {
            anim.SetBool("isActive", false); // Fade out text
            //tutorialText.text = "";
        }
        gameObject.SetActive(false);
    }

    // Used to null out the text when player leaves trigger.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && tutorialText.text == tutorialDictionary[input])
        {
            anim.SetBool("isActive", false); // Fade out text
            //tutorialText.text = "";
        }
    }
}
