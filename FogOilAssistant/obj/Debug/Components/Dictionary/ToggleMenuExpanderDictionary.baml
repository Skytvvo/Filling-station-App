   M S B A M L   `   `   `  ����J  FFogOilAssistant, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:FogOilAssistant.Components.ViewFogOilAssistant.Components.View  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089b ^SharpVectors.Converters.Wpf, Version=1.7.1.0, Culture=neutral, PublicKeyToken=b532964b8548be77   expander_block  �� 
TargetType �� Property �� Value  expander_block_button �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value  expander_block_content  expander_block_style �� Orientation	 d�Name(   "SharpVectors.Converters.SvgViewbox
   Name J� RenderTransformOrigin (� Stretch    Source �� Angle0   *FogOilAssistant.Components.View.ToggleMenu  Name �� Triggers �� 
TargetName �� Value �� Binding �� ElementName �� Path�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      8local1-clr-namespace:FogOilAssistant.Components.View  5      1svgc&http://sharpvectors.codeplex.com/svgc/  6Q   %h  &	        &	 t     &	 �    &	     7  �� 5      "  ��6#   .�5	   
   ��  � ��$ Bottom=�6.   �� 5
   
    � ��$ Center=�60   5      �� 5      "  d�6*   .�5   
   ��  � �� �	#FF2221216'   �� 5   
    �@�Cursor$
 Hand��6#   �� 5   
    �@�ForceCursor . 6(   �� 5   
    � ��$ 5,5,5,5q�6#   �� 5   
    � ��$ 30��6#   �� 5   
    � ��$ 30��6"   �� 5   
    � ��$ 5q�6$   �� 5   
    � �� 5      �� 5      " d�6"   .R�5      �� #��z��6   ���#FF0000$��2q�6]   $ 5��6q   ._�5      �� $��Center=�6+   $��Center=�6H   5      5      5       5   
   P� 5       �@J�IsMouseOver . . �5       ��  � �� �#FF00006/   5!      5%      �� 5&      "  ��6+   5(      �� 5*      toggle_expander��"  9�6B   .�5+   
   ��  � ��$ Center=�60   �� 5,   
    � ��$ Bottom=�6.   �� 5-   
    �@9�
IsExpanded .  6'   �� 5.   
    � �� 5/      �� 50      ct_toggle_expander��" 9�6>   .R�51      �� $ Vertical=�6!   .+�52      d� $	 toggle_button��6W   8��  .��53         $
 toggle_button_svg��6.   $ 0.5,0.5&�6J   $��20��6j   $��20��6v   $ Uniform=�6�   $$ /Components/Images/exp_btn.svg?�6�   z�54   "   �� 55   &   toggle_button_angle��$ 0Q�6T   57      58        59      $ toggle_menu��6,   $
��1280��6A   $	��250��6N   $y�	Collapsed=�6[   5:       5;      P� 5<       �@9�
IsExpanded . . �5=      �� $ toggle_menu��6%    � y�$ Visible=�6T   �� 5>      $ toggle_button_svg��6@    � z� 5?   "   �� 5@   &   $	 180Q�66   5B      5C      �� 5D      $
 True��6d    6&   �� $ toggle_button��$ 	IsChecked�.a�5E      ��  �@9�
IsExpanded . 6;   5F      5H      5K      5L      5N      