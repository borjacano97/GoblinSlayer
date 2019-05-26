using UnityEngine;

public class SourceSelector : MonoBehaviour {

    public enum Source { INPUT, FILE };
    public Source source;

    public Automata automata;
    public InputManager inputManager;

    private void Awake()
    {
        inputManager.gameObject.SetActive(source == Source.INPUT);
            automata.gameObject.SetActive(source == Source.FILE);
    }
}
