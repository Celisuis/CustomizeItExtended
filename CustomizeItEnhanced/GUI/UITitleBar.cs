using ColossalFramework.UI;
using CustomizeItEnhanced.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CustomizeItEnhanced
{
    public class UITitleBar : UIPanel
    {
        public static UITitleBar Instance;
        private UILabel titleLabel;
        private UIButton closeButton;
        public UIDragHandle dragHandle;

        public override void Start()
        {
            base.Start();
            Instance = this;
            SetupControls();
        }

        private void SetupControls()
        {
            name = "CustomizeItEnhancedTitleBar";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = Vector3.zero;
            width = parent.width;
            height = 40f;

            dragHandle = AddUIComponent<UIDragHandle>();
            dragHandle.height = height;
            dragHandle.relativePosition = Vector3.zero;
            dragHandle.target = parent;
            dragHandle.eventMouseUp += (c, e) =>
            {
                CustomizeItEnhancedMod.Settings.PanelX = parent.relativePosition.x;
                CustomizeItEnhancedMod.Settings.PanelY = parent.relativePosition.y;
                CustomizeItEnhancedMod.Settings.Save();
            };

            titleLabel = AddUIComponent<UILabel>();
            titleLabel.text = CustomizeItEnhancedTool.instance.CurrentSelectedBuilding.GetUncheckedLocalizedTitle();
            titleLabel.textScale = 0.9f;
            titleLabel.isInteractive = false;

            closeButton = AddUIComponent<UIButton>();
            closeButton.size = new Vector2(20, 20);
            closeButton.relativePosition = new Vector3(width - closeButton.width - 10f, 10f);
            closeButton.normalBgSprite = "DeleteLineButton";
            closeButton.hoveredBgSprite = "DeleteLineButtonHovered";
            closeButton.pressedBgSprite = "DeleteLineButtonPressed";
            closeButton.eventClick += (component, param) =>
            {
                CustomizeItEnhancedTool.instance.CustomizeItEnhancedPanel.isVisible = false;
                UIUtils.DeepDestroy(CustomizeItEnhancedTool.instance.CustomizeItEnhancedPanel);
            };
        }

        public void RecenterElements()
        {
            closeButton.relativePosition = new Vector3(width - closeButton.width - 10f, 10f);
            titleLabel.relativePosition = new Vector3((width - titleLabel.width) / 2f, (height - titleLabel.height) / 2);
        }
    }
}
