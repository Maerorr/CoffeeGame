using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _okButton;
    private Button _explainButton;
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

        _okButton = _document.rootVisualElement.Q("OkButton") as Button;
        _explainButton = _document.rootVisualElement.Q("ExplainButton") as Button;
        _mainDialogue = _document.rootVisualElement.Q("MainDialogue") as Label;
        
        if (_okButton != null) 
            _okButton.RegisterCallback<ClickEvent>(OnOkClick);
        if (_explainButton != null)
            _explainButton.RegisterCallback<ClickEvent>(OnExplainClick);
    }

    private void UnregisterEventListeners() {
        if (_okButton != null)
            _okButton.UnregisterCallback<ClickEvent>(OnOkClick);
        if (_explainButton != null)
            _explainButton.UnregisterCallback<ClickEvent>(OnExplainClick);
    }

    private void OnOkClick(ClickEvent evt) {
        clientHandler.nextClient();
        Debug.Log("Ok pressed");
    }

    private void OnExplainClick(ClickEvent evt) {
        Debug.Log("Explain pressed");
        if (_explainButton.text == "Go back") {
            SetClientDialogue(order);
            _explainButton.text = "Explain";
        } else {
            SetClientDialogue(explanation);
            _explainButton.text = "Go back";
        }
    }

    public void SetClientDialogue(string dialogue)
    {
        if (_mainDialogue != null)
        {
            _mainDialogue.text = dialogue;
        }
    }

    public void SetClientOrder(string order)
    {
        this.order = order;
        SetClientDialogue(order);
    }

    public void setClientExplanation(string explanation)
    {
        this.explanation = explanation;
    }

}
