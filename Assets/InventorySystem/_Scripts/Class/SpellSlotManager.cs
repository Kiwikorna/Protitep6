using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SpellSlotManager : MonoBehaviour
{ 
    [SerializeField] private SpellSlot[] spellSlots; // Первые два слота для рецепта
    [SerializeField] private ResultCombo resultSlot;  // Третий слот для результата
    [SerializeField] private CombinationManager combinationManager;
    
    
    private int _currentSpellSlotIndex = 0;
    private float _lastSlotUseTime = 0f;
    private bool _isWaitingForReset = false;
    private float _resetSlotTime;
    private int _spellSequenceIndex = 0; // Tracks current sequence step (0 -> 1 -> 2)
    private bool _isComboComplete = false;

    private void Update()
    {
        CheckAndCombineSpells();
        CheckAndClearResultSlot();
    }

    public void Initialize(float resetSlotTime)
    {
        _resetSlotTime = resetSlotTime;
    }

    
    public void UpdateSlotReset()
    {
        if (_isWaitingForReset && Time.time - _lastSlotUseTime > _resetSlotTime)
        {
            ResetSlotTime();
        }
    }
    
    private void ResetSlotTime()
    {
        _currentSpellSlotIndex = 0;
        _isWaitingForReset = false;
    }

    public SpellSlot GetCurrentSpellSlot()
    {
        var currentSlot = spellSlots[_currentSpellSlotIndex];
        _currentSpellSlotIndex = (_currentSpellSlotIndex + 1) % spellSlots.Length;
        _lastSlotUseTime = Time.time;
        _isWaitingForReset = true;

        return currentSlot;
    }

    
    public void CheckAndCombineSpells()
    {
        if (spellSlots.Length < 2 || resultSlot == null) return;

        var item1 = spellSlots[0].GetComponentInChildren<InventoryItem>();
        var item2 = spellSlots[1].GetComponentInChildren<InventoryItem>();

        // Проверяем, что в обоих слотах есть предметы
        if (item1 != null && item2 != null)
        {
            var resultCombo = combinationManager.GetCombinationResult(item1.Config, item2.Config);
            if (resultCombo != null)
            {
                resultSlot.AddItemToSlot(resultCombo);
            }
        }
    }
    public void AdvanceSequence()
    {
        _spellSequenceIndex++;
        if (_spellSequenceIndex >= spellSlots.Length) // After the second spell
        {
            _isComboComplete = true;
        }
    }

    public void ResetSequence()
    {
        _spellSequenceIndex = 0;
        _isComboComplete = false;
    }

    public int GetCurrentStepCombo() => _spellSequenceIndex;
    public int GetCurrentSpellSlotIndex() => _currentSpellSlotIndex;
    public bool IsComboReady() => _isComboComplete;

    public void CheckAndClearResultSlot()
    {
        // Очищаем результат, если один из слотов рецепта пуст
        var item1 = spellSlots[0].GetComponentInChildren<InventoryItem>();
        var item2 = spellSlots[1].GetComponentInChildren<InventoryItem>();

        if (item1 == null || item2 == null)
        {
            resultSlot.ClearSlot();
        }
    }
}
