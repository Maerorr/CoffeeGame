using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class CompendiumListManager : MonoBehaviour
{

    private UIDocument _document;
    private Button _recipeButton;

    private Button _toppingButton;

    private Button _bunCakeButton;

    private Button _closeButton;
    private ScrollView _scrollView;
    public VisualElement _leftPane;
    public VisualElement _rightPane;
    public VisualElement _itemDetails;
    public StyleSheet _styleSheet;


    public RecipeItemSO[] recipeItems;

    private void RegisterEventListeners()
    {
        if (_document == null) return;

        _leftPane = _document.rootVisualElement.Q("LeftPane") as VisualElement;
        _rightPane = _document.rootVisualElement.Q("RightPane") as VisualElement;
        _itemDetails = _document.rootVisualElement.Q("ItemDetails") as ScrollView;
        _recipeButton = _document.rootVisualElement.Q("RecipeButton") as Button;
        _toppingButton = _document.rootVisualElement.Q("ToppingButton") as Button;
        _bunCakeButton = _document.rootVisualElement.Q("BunCakeButton") as Button;
        _closeButton = _document.rootVisualElement.Q("CloseButton") as Button;

        if (_recipeButton != null)
            _recipeButton.RegisterCallback<ClickEvent>(ToggleRecipeList);

        if (_closeButton != null)
        {
            _closeButton.RegisterCallback<ClickEvent>(CloseCompendium);
        }
    }

    private void ToggleRecipeList(ClickEvent evt)
    {
        if (_scrollView.visible == true)
        {
            _scrollView.visible = false;
        }
        else
        {
            _scrollView.visible = true;
        }

    }

    private void CloseCompendium(ClickEvent evt)
    {
        _document.gameObject.SetActive(false);
    }

    void BuildRecipeList()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _scrollView = root.Q<ScrollView>("RecipeItemList");
        _scrollView.Clear();
        _scrollView.AddToClassList("scroll-view");
        int itemsPerPage = 12;
        int itemsPerRow = 3;

        for (int i = 0; i < itemsPerPage; i += itemsPerRow)
        {
            var rowContainer = new VisualElement();
            rowContainer.style.flexDirection = FlexDirection.Row;
            rowContainer.style.justifyContent = Justify.SpaceBetween;
            rowContainer.style.marginBottom = 9;

            for (int j = 0; j < itemsPerRow; j++)
            {
                int itemIndex = i + j;
                var itemContainer = new VisualElement();

                itemContainer.AddToClassList("item-container");

                if (itemIndex < recipeItems.Length)
                {
                    var itemImage = new Image();
                    itemImage.AddToClassList("item-image");
                    Sprite sprite = recipeItems[itemIndex].sprite;
                    if (sprite != null)
                    {
                        itemImage.image = sprite.texture;
                    }
                    itemContainer.Add(itemImage);

                    int currentItemIndex = itemIndex;
                    itemContainer.RegisterCallback<ClickEvent>(evt => ShowItemDetails(currentItemIndex));
                }

                rowContainer.Add(itemContainer);
            }

            _scrollView.Add(rowContainer);
        }
    }

    private void ShowItemDetails(int itemIndex)
    {
        _itemDetails.Clear();

        var selectedItem = recipeItems[itemIndex];

        var largeImage = new Image();
        largeImage.AddToClassList("large-item-image");
        if (selectedItem.sprite != null)
        {
            largeImage.image = selectedItem.sprite.texture;
        }

        var itemNameLabel = new Label(selectedItem.name);
        itemNameLabel.AddToClassList("item-name");

        var itemDescriptionLabel = new Label(selectedItem.description);
        itemDescriptionLabel.AddToClassList("item-description");

        _itemDetails.Add(largeImage);
        _itemDetails.Add(itemNameLabel);
        _itemDetails.Add(itemDescriptionLabel);
    }

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        RegisterEventListeners();
    }

    void Start()
    {
        BuildRecipeList();
    }

}