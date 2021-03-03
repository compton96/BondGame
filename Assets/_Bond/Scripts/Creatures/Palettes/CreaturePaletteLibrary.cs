using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "PaletteLibrary", menuName = "ScriptableObjects/CreaturePaletteLibrary")]
public class CreaturePaletteLibrary : ScriptableObject
{
    public List<CreaturePalette> creaturePalettes;

    private Queue<CreaturePalette> _randomOrder;

    public CreaturePalette GetRandomPalette()
    {
        if (_randomOrder == null) _randomOrder = new Queue<CreaturePalette>();
        if(_randomOrder.Count == 0)
        {
            creaturePalettes.ForEach(palette => _randomOrder.Enqueue(palette));
            _randomOrder.OrderBy(x => Random.value);
        }

        return _randomOrder.Dequeue();
    }
}
