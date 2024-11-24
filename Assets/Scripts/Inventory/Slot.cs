using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    private DragDropHandler dragDropHandler;
    private InventoryManager inventory;
    public ItemsSO data;
    public int stackSize;
    [Space]
    public Image icon;
    public Text stackText;
    

    private bool isEmpty;
    public bool IsEmpty => isEmpty;

    private void Start()
    {
        dragDropHandler = GetComponentInParent<Player>().GetComponentInChildren<DragDropHandler>();
        inventory = GetComponentInParent<Player>().GetComponentInChildren<InventoryManager>();
        UpdateSlot();

    }

    public void UpdateSlot()
    {
        if (data != null)
        {
            if(data.itemType != ItemsSO.ItemType.Weapon)
            {
                if (stackSize <= 0)
                {
                    data = null;
                }
            }
        }   


        if (data == null)
        {
            isEmpty = true;

            icon.gameObject.SetActive(false);
            stackText.gameObject.SetActive(false);
        }
        else
        {
            isEmpty = false;

            icon.sprite = data.icon;
            stackText.text = $"x{stackSize}";


            icon.gameObject.SetActive(true);
            stackText.gameObject.SetActive(true);
        }
    }
    public void AddItemToSlot(ItemsSO data_, int stackSize_)
    {
        data = data_;
        stackSize = stackSize_;
    }

    public void AddStackAmount(int stackSize_)
    {
        stackSize += stackSize_;
    }

    public void Drop()
    {
        GetComponentInParent<InventoryManager>().DropItem(this);
    }

    public void Clean()
    {
        data = null;
        stackSize = 0;
        UpdateSlot();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dragDropHandler.isDragging)
        {
            if (eventData.button == PointerEventData.InputButton.Left && !isEmpty)
            {
               dragDropHandler.slotDraggedFrom = this;
               dragDropHandler.isDragging = true; 
            }
        }
    }

    public void Try_Use()
    {
        if(data == null)
        return;

        if(data.itemType == ItemsSO.ItemType.Consumable)
        Consume();
    }

    public void Consume()
    {
        PlayerStats stats = GetComponentInParent<PlayerStats>();
        stats.health += data.healthChange;
        stats.hunger += data.hungerChange;
        stats.thirst += data.thirstChange;

        stackSize--;

        UpdateSlot();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (dragDropHandler.isDragging)
        {
            dragDropHandler.slotDraggedTo = this;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (dragDropHandler.isDragging)
        {
            //drop
            if (dragDropHandler.slotDraggedTo == null)
            {
                dragDropHandler.slotDraggedFrom.Drop();
                dragDropHandler.isDragging = false;
            }
            //drag and drop
            else if (dragDropHandler.slotDraggedTo != null)
            {
                inventory.DragDrop(dragDropHandler.slotDraggedFrom, dragDropHandler.slotDraggedTo);
                dragDropHandler.isDragging = false;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (dragDropHandler.isDragging)
        {
            dragDropHandler.slotDraggedTo = null;
        }

    }
}


    