using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Noxdew.UI.Editor
{
  public class DelayedHealthBarEditor : UnityEditor.Editor
  {
    [MenuItem("GameObject/UI/Noxdew/Delayed Health Bar")]
    static void CreateDelayedHealthBar(MenuCommand menuCommand) {
      // Create a custom game object
      var go = new GameObject("DelayedHealthBar", typeof(RectTransform), typeof(Slider), typeof(Image), typeof(DelayedHealthBar));

      var dhb = go.GetComponent<DelayedHealthBar>();

      // Set the default transform size
      var transform = go.GetComponent<RectTransform>();
      transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 160);
      transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20);

      // Set the default background colour
      var background = go.GetComponent<Image>();
      background.color = new Color(0.8f, 0.8f, 0.8f);

      // Disable raycasting to improve performance
      background.raycastTarget = false;

      // Disable the transition by default
      var slider = go.GetComponent<Slider>();
      slider.transition = Selectable.Transition.None;
      slider.interactable = false;

      // Set an initial value to ensure the health can be seen
      slider.value = 0.5f;

      // Disable the navigation by default
      var nav = new Navigation();
      nav.mode = Navigation.Mode.None;
      slider.navigation = nav;

      // Create the health layer
      var healthArea = new GameObject("Health", typeof(RectTransform), typeof(Image));
      
      // Link the health layer to the slider
      var healthTransform = healthArea.GetComponent<RectTransform>();
      slider.fillRect = healthTransform;

      // Set the default colour
      var healthImage = healthArea.GetComponent<Image>();
      healthImage.color = new Color(1, 0.32f, 0.32f);

      // Disable raycasting to improve performance
      healthImage.raycastTarget = false;

      // Create the delayed bar slider
      var delayedBar = new GameObject("DelayedSlider", typeof(RectTransform), typeof(Slider));

      // Setup delayed slider props
      var delayedSlider = delayedBar.GetComponent<Slider>();
      delayedSlider.interactable = false;

      // Setup the logic script
      dhb.healthSlider = slider;
      dhb.delayedHealthSlider = delayedSlider;

      delayedSlider.transition = Selectable.Transition.None;

      // Set an initial value to ensure the health can be seen
      delayedSlider.value = 0.75f;

      // Disable the navigation by default
      delayedSlider.navigation = nav;

      // Create the health layer
      var delayedArea = new GameObject("DelayedHealth", typeof(RectTransform), typeof(Image));

      // Link the health layer to the slider
      var delayedAreaTransform = delayedArea.GetComponent<RectTransform>();
      delayedSlider.fillRect = delayedAreaTransform;

      // Set the default colour
      var delayedImage = delayedArea.GetComponent<Image>();
      delayedImage.color = new Color(1, 0.70f, 0);

      // Disable raycasting to improve performance
      delayedImage.raycastTarget = false;

      // Ensure it gets reparented if this was a context click (otherwise does nothing)
      GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);

      var delayedSliderTransform = delayedBar.GetComponent<RectTransform>();
      ObjectCreationUtilities.SetAndStretchToParentSize(delayedSliderTransform, transform);
      ObjectCreationUtilities.SetAndStretchToParentSize(delayedAreaTransform, delayedSliderTransform);
      ObjectCreationUtilities.SetAndStretchToParentSize(healthTransform, transform);

      // Register the creation in the undo system
      Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
      Selection.activeObject = go;
    }
  }
}
