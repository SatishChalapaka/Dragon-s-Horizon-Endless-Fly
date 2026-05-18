using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Shop Item")]

public class ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public RenderTexture rawImage;
    public int cost;
    public int number;
}
