using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SpellCaster : MonoBehaviour
{  
    [Header("Cast Spell System")]
    [SerializeField] private Transform spellSpawnPoint;
    [SerializeField] private CharacterManaSO playerMana;
    
    
   
    [Header("Time")]
    [SerializeField] private float timeCastSpell;
    [SerializeField] private float resetSlotTime;
    
    
    [Header("Slot Manager")]
    [SerializeField] private SpellSlotManager spellSlotManager;
    [SerializeField] private ResultCombo resultCombo;
    
    [Header("StateMove")]
    [SerializeField] private float timeStopMove;


   
    private SpellItem _currentSpellConfig;
    private StateMove _stateMove;
    private bool _isCastingSpell = false;

    private void Awake()
    {
        _stateMove = StateMove.Move;
        spellSlotManager.Initialize(resetSlotTime);
    }

    private void Start()
    {
        ControllerPlayer.Instance.OnCastBaseSpellButtonPressed += SpellCast;
    }

    private void Update()
    {
       spellSlotManager.UpdateSlotReset();
    }



    private void SpellCast()
    {
        // Проверка на готовность каста третьего (комбо) заклинания
        InventoryItem resultItem = resultCombo.GetChildrenItem();
        if (resultItem != null && spellSlotManager.IsComboReady())
        {
            Debug.Log("Nice");
            CastResultSpell(resultItem); // Вызов третьего заклинания
            return;
        }

        // Если комбинация не завершена, продолжаем обычный каст
        SpellSlot spellSlot = spellSlotManager.GetCurrentSpellSlot();
        InventoryItem inventoryItem = spellSlot.GetComponentInChildren<InventoryItem>();

        if (inventoryItem != null && inventoryItem.Config != null)
        {
            if (playerMana.manaCharacterValue > inventoryItem.Config.spellConfig.GetManaCost())
            {
                if (!_isCastingSpell && spellSlotManager.GetCurrentStepCombo() == spellSlotManager.GetCurrentSpellSlotIndex())
                {
                    _currentSpellConfig = inventoryItem.Config;
                    StartCoroutine(DelaySpell());
                    Debug.Log(spellSlotManager.GetCurrentStepCombo());
                    
                    spellSlotManager.AdvanceSequence(); // Продвижение по комбо
                }
            }
        }
    }



    private IEnumerator BlockMovement()
    {
        _stateMove = StateMove.NoMove;
        yield return new WaitForSeconds(timeStopMove);
        _stateMove = StateMove.Move;
    }

    private IEnumerator DelaySpell()
    {
        _isCastingSpell = true;
        yield return new WaitForSeconds(_currentSpellConfig.spellConfig.GetDelayForSpell());

        var spellObject = Instantiate(_currentSpellConfig.spellConfig.GetSpellPrefab(), spellSpawnPoint.position, spellSpawnPoint.rotation);
        spellObject.GetComponent<Rigidbody>().linearVelocity = spellSpawnPoint.forward * _currentSpellConfig.spellConfig.GetSpeed();
        
        playerMana.manaCharacterValue -= _currentSpellConfig.spellConfig.GetManaCost();
        StartCoroutine(BlockMovement());

        _isCastingSpell = false;
    }
    
    private void CastResultSpell(InventoryItem resultItem)
    {
        if (spellSlotManager.IsComboReady())
        {
            if (playerMana.manaCharacterValue > resultItem.Config.spellConfig.GetManaCost())
            {
                _currentSpellConfig = resultItem.Config;
                StartCoroutine(DelaySpell());
                spellSlotManager.ResetSequence(); // Reset after casting the combo spell
            }
        }
        else
        {
            Debug.Log("You must cast the first two spells before using the combo spell.");
        }
    }



    public StateMove GetStateMove() => _stateMove;
}
