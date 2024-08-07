using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _buttonOk;
    private Button _buttonExplain;
    private Label _mainDialogue;

    private List<string> orders;

    private void initializeOrders() {
        orders = new List<string> {
            "A venti coffee frappucino with two scoops of ice, five pumps of frap roast, and double blended.",
            "A venti mango black tea lemonade with 24 pumps of mango.",
            "A venti salted caramel mocha frappucino with five pumps of frap roast, four pumps of caramel sauce, four pumps of caramel syrup, three pumps of mocha, three pumps of toffee nut syrup, double blended with extra whipped cream.",
            "A venti coffee with 10 Splenda packets and whipped cream.",
            "A doppio espresso with 20 shots of espresso and 10 pumps of white mocha."
        };
    }

    private void Awake() {
        _document = GetComponent<UIDocument>();

        RegisterEventListeners();

        initializeOrders();
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
        Debug.Log("Ok pressed");
        var random = new System.Random();
        int index = random.Next(orders.Count);
        _mainDialogue.text = orders[index];
    }

    private void OnExplainClick(ClickEvent evt) {
        Debug.Log("Explain pressed");
    }

}
