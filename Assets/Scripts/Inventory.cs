using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Image> inventoryImages = new List<Image>();

    private List<Weapon> _weapons;

    public Color activeColour = Color.white;
    public Color inactiveColour = Color.grey; 

    public Weapon[] weapons {
        get {
            // return weapons except null values
            return _weapons.FindAll(w => w != null).ToArray();
        }
    } 

    private int _currentSlot = 0;
    public int currentSlot {
        get { return _currentSlot; }
        set {
            inventoryImages[_currentSlot].color = activeColour;
            _currentSlot = value;
            inventoryImages[_currentSlot].color = inactiveColour;
        }
    }

    public Weapon currentWeapon {
        get { return _weapons[currentSlot]; }
    }

    public int nextEmptySlot {
        get {
            for (int i = 0; i < this._weapons.Count; i++)
            {
                if (_weapons[i] == null) return i;
            }
            return -1;
        }
    }

    void Start()
    {
        for (int i = 0; i < inventoryImages.Count; i++)
        {
            inventoryImages[i].enabled = inventoryImages[i].sprite != null;
        }
        // new list of length weapons.Count
        _weapons = new List<Weapon>(inventoryImages.Count);
        for (int i = 0; i < inventoryImages.Count; i++)
        {
            _weapons.Add(null);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentSlot = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            currentSlot = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            currentSlot = 2;
    }

    public void SetSlot(int slot, Weapon weapon)
    {
        _weapons[slot] = weapon;
        inventoryImages[slot].sprite = weapon.icon;
        inventoryImages[slot].enabled = true;
    }

    public void ClearSlot(int slot)
    {
        _weapons[slot] = null;
        inventoryImages[slot].sprite = null;
        inventoryImages[slot].enabled = false;
    }

    public Weapon GetSlot(int slot)
    {
        return _weapons[slot];
    }
}
