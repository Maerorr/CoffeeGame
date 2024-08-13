using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using System.Threading;
using UnityEngine.InputSystem;

public class CompendiumListManager : MonoBehaviour
{
    private const int itemsPerPage = 12;
    private int currentPage = 0;

    private UIDocument _document;
    private VisualElement _root;
    private Button _recipeButton;

    private Button _toppingButton;

    private Button _bunCakeButton;

    private Button _nextPageButton;

    private Button _previousPageButton;

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

        _root = GetComponent<UIDocument>().rootVisualElement;

        _itemDetails = _document.rootVisualElement.Q("ItemDetails") as ScrollView;
        _recipeButton = _document.rootVisualElement.Q("RecipeButton") as Button;
        _toppingButton = _document.rootVisualElement.Q("ToppingButton") as Button;
        _bunCakeButton = _document.rootVisualElement.Q("BunCakeButton") as Button;
        _closeButton = _document.rootVisualElement.Q("CloseButton") as Button;
        _nextPageButton = _document.rootVisualElement.Q("NextPageButton") as Button;
        _previousPageButton = _document.rootVisualElement.Q("PreviousPageButton") as Button;

        if (_recipeButton != null)
            _recipeButton.RegisterCallback<ClickEvent>(ToggleRecipeList);

        if (_closeButton != null)
        {
            _closeButton.RegisterCallback<ClickEvent>(CloseCompendium);
        }

        if (recipeItems.Length > itemsPerPage)
        {
            _nextPageButton.visible = true;
            _previousPageButton.visible = true;
            _nextPageButton.RegisterCallback<ClickEvent>(HandleNextPage);
            _previousPageButton.RegisterCallback<ClickEvent>(HandlePreviousPage);
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

    private void HandleNextPage(ClickEvent evt)
    {
        if (currentPage < recipeItems.Length / itemsPerPage)
        {
            BuildRecipeList(++currentPage);
        }
    }

    private void HandlePreviousPage(ClickEvent evt)
    {
        if (currentPage > 0)
        {
            BuildRecipeList(--currentPage);
        }
    }

    private void CloseCompendium(ClickEvent evt)
    {
        _document.gameObject.SetActive(false);
    }

    void BuildRecipeList(int page)
    {

        _scrollView = _root.Q<ScrollView>("RecipeItemList");
        _scrollView.Clear();
        _scrollView.AddToClassList("scroll-view");
        int itemsPerRow = 3;

        for (int i = 0; i < itemsPerPage; i += itemsPerRow)
        {
            var rowContainer = new VisualElement();
            rowContainer.style.flexDirection = FlexDirection.Row;
            rowContainer.style.justifyContent = Justify.SpaceBetween;
            rowContainer.style.marginBottom = 9;

            for (int j = 0; j < itemsPerRow; j++)
            {
                int itemIndex = page * itemsPerPage + i + j;
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

    public void Toggle(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            _document.gameObject.SetActive(!_document.gameObject.activeSelf);
            if (_document.gameObject.activeSelf == true)
            {
                RegisterEventListeners();
                BuildRecipeList(currentPage);
            }
        }
    }

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        RegisterEventListeners();
    }

    void Start()
    {
        BuildRecipeList(currentPage);
    }

}