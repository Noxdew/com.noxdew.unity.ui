using UnityEngine;

namespace Noxdew.UI.Editor
{
  public class ObjectCreationUtilities
  {
    public static void SetAndStretchToParentSize(RectTransform _mRect, RectTransform _parent) {
      _mRect.anchoredPosition = _parent.position;
      _mRect.anchorMin = new Vector2(0, 0);
      _mRect.anchorMax = new Vector2(1, 1);
      _mRect.pivot = new Vector2(0.5f, 0.5f);
      _mRect.sizeDelta = _parent.rect.size;
      _mRect.transform.SetParent(_parent);
    }
  }
}
