using System.Collections.Generic;
using UnityEngine;

public interface IProducible
{
    public List<(string id, string name, Sprite icon)> GetProductList();
    public void Produce(string id);
    public bool IsAvailable(IGridContent gridContent);
}
