using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.MyUIFramework
{
    internal static class MultipleDisplayUtilities
    {
        public static bool GetRelativeMousePositionForDrag(PointerEventData eventData, ref Vector2 position)
        {
#if UNITY_EDITOR
            position = eventData.position;
#else
            int pressDisplayIndex = eventData.pointerPressRaycast.displayIndex;
            var relativePosition = RelativeMouseAtScaled(eventData.position);
            int currentDisplayIndex = (int)relativePosition.z;

            if (currentDisplayIndex != pressDisplayIndex) return false;

            position = pressDisplayIndex != 0 ? (Vector2)relativePosition : eventData.position;

#endif
            return true;
        }

        public static object RelativeMouseAtScaled(Vector2 position)
        {
#if !UNITY_EDITOR && !UNITY_WSA
            if (Display.main.renderingWidth != Display.main.systemWidth ||
                Display.main.renderingHeight != Display.main.systemHeight)
            {
                var systemAspectRatio = Display.main.systemWidth / (float)Display.main.systemHeight;

                var sizePlusPadding = new Vector2(Display.main.renderingWidth, Display.main.renderingHeight);
                var padding = Vector2.zero;

                if (Screen.fullScreen)
                {
                    var aspectRatio = Screen.width / (float)Screen.height;
                    if (Display.main.systemHeight * aspectRatio < Display.main.systemWidth)
                    {
                        sizePlusPadding.x = Display.main.renderingHeight * systemAspectRatio;
                        padding.x = (sizePlusPadding.x - Display.main.renderingWidth) * 0.5f;
                    }
                    else
                    {
                        sizePlusPadding.y = Display.main.renderingWidth / systemAspectRatio;
                        padding.y = (sizePlusPadding.y - Display.main.renderingHeight) * 0.5f;
                    }
                }


                var sizePlusPositivePadding = sizePlusPadding - padding;

                if (position.y < -padding.y || position.y > sizePlusPadding.y ||
                    position.x < -padding.x || position.x > sizePlusPadding.x)
                {
                    var adjustedPosition = position;

                    if (!Screen.fullScreen)
                    {
                        adjustedPosition.x -= (Display.main.renderingWidth - Display.main.systemWidth) * 0.5f;
                        adjustedPosition.y -= (Display.main.renderingHeight - Display.main.systemHeight) * 0.5f;
                    }
                    else
                    {
                        adjustedPosition += padding;
                        adjustedPosition.x *= Display.main.systemWidth / sizePlusPadding.x;
                        adjustedPosition.y *= Display.main.systemHeight / adjustedPosition.y;
                    }

                    var relativePos = Display.RelativeMouseAt(adjustedPosition);
                    if (relativePos.z != 0) return relativePos;
                }

                return new Vector3(position.x, position.y, 0);
            }

#endif
            return Display.RelativeMouseAt(position);
        }
    }
}