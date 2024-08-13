using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _buttonOk;
    private Button _buttonExplain;
    public Label _mainDialogue;

    private string order;

    private string explanation;

    public ClientHandler clientHandler;


    private void Awake() {
        _document = GetComponent<UIDocument>();

        RegisterEventListeners();

    }

    private void OnEnable() {
        RegisterEventListeners();
    }

    private void OnDisable() {
        UnregisterEventListeners();
    }

    private void RegisterEventListeners() {
        if (_document == null) return;

        _buttonOk = _document.rootVisualElement.Q("OkButton") as Button;
        _buttonExplain = _document.rootVisualElement.Q("ExplainButton") as Button;
        _mainDialogue = _document.rootVisualElement.Q("MainDialogue") as Label;
        
        if (_buttonOk != null) 
            _buttonOk.RegisterCallback<ClickEvent>(OnOkClick);
        if (_buttonExplain != null)
            _buttonExplain.RegisterCallback<ClickEvent>(OnExplainClick);
    }

    private void UnregisterEventListeners() {
        if (_buttonOk != null)
            _buttonOk.UnregisterCallback<ClickEvent>(OnOkClick);
        if (_buttonExplain != null)
            _buttonExplain.UnregisterCallback<ClickEvent>(OnExplainClick);
    }

    private void OnOkClick(ClickEvent evt) {
        clientHandler.nextClient();
        Debug.Log("Ok pressed");
    }

    private void OnExplainClick(ClickEvent evt) {
        Debug.Log("Explain pressed");
        if (_buttonExplain.text == "Go back") {
            setClientDialogue(order);
            _buttonExplain.text = "Explain";
        } else {
            setClientDialogue(explanation);
            _buttonExplain.text = "Go back";
        }
    }

    public void setClientDialogue(string dialogue)
    {
        if (_mainDialogue != null)
        {
            _mainDialogue.text = dialogue;
        }
    }

    public void setClientOrder(string order)
    {
        this.order = order;
        setClientDialogue(order);
    }

    public void setClientExplanation(string explanation)
    {
        this.explanation = explanation;
    }

}
