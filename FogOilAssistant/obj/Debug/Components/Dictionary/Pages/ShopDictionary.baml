   M S B A M L   `   `   `  ����J  FFogOilAssistant, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:FogOilAssistant.Components.Dictionary.Pages+FogOilAssistant.Components.Dictionary.Pages  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   product_panel_item  �� 
TargetType �� Property �� Value �� Triggers �� Binding �� ElementName �� Path �� Value-   'System.Windows.Media.Effects.BlurEffect    Radius	    
KernelType
 P� Property P� Value  product_img  product_price  product_name �� BasedOn  product_description  product_hover_button �� 
TargetType ]� 
Conditions �� Property �� Value  products_filter  product_filter_btn  btn_text 	 filter_panel�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      Clocal9clr-namespace:FogOilAssistant.Components.Dictionary.Pages  5      %  &	        &	 U    &	 �    &	 �    7  &	 4    7  &	 8    &	 n    &	 �    &	 �
    &		 +    �� 5      "  �6'   .�5   
   ��  � ��$	 200��6#   �� 5   
    � ��$ Stretch=�60   �� 5	   
    � ��$ Center=�6.   �� 5
   
    � ��$ 10,10,10,10q�6#   �� 5   
    � �� �	#FF2221216'    5   
   �� 5       6   �� $ product_buy��$ IsMouseOver� True6X   .a�5      ��  �@J�Effect 5         5      $ 20Q�6%   $	 Gaussian=�61   5      5      P� 5      
 �@J�IsMouseOver . . �5      ��  �@J�Effect 5         5      $ 20Q�6%   $	 Gaussian=�61   5      5      5      �� 5      "  ��6    .�5    
   ��  � ��$ Center=�60   �� 5!   
    � ��$ Center=�6.   5"      �� 5#      "  ��6"   .�5%   
   ��  � ��$ Center=�6.   �� 5&   
    � ��$ 30(�6%   �� 5'   
    � ��$
 Bold"�6'   �� 5(   
    � �� �#FFFFFF6'   �� 5)   
    �@��TextAlignment$ Center=�6*   �� 5*   
    � ��$ Center=�60   5+      �� 5-      "  ��6!   8   .�5.   
   ��  � ��$ 20(�6%   5/      �� 51      "  ��6(   8   .�52   
   ��  � ��$ 14(�6%   �� 53   
    �@��TextAlignment$ Justify=�6*   �� 54   
    � ��$
 Wrap=�6)   �� 55   
    � ��$ WordEllipsis=�6)   �� 56   
    �@��Padding$ 10q�6$   57      �� 59      "  ��6)   .�5:   
   ��  � ��$ 50��6&   �� 5;   
    � y�$ Hidden=�6'   �� 5<   
    � ��$	 100��6%   �� 5=   
    � ��
 ���� 6'   �� 5>   
    � �� �#ffffff6'   �� 5?   
    � ��$
 Bold"�6'   �� 5@   
    �@�Cursor$
 Hand��6#   �� 5A   
    � �� 5B      �� 5C      " ��6"   .R�5E      �� ���#FF0000$��2q�63   #��z��6G   ._�5F      �� $��Center=�6+   $��Center=�6H   5G      5H      5J       5K   
   �� 5L       6   �� $ product_panel��$ IsMouseOver� True6Z   .a�5M      ��  � y�$ Visible=�6/   5N      ]� 5O       5P      �� 5Q       �@J�IsMouseOver . 67   ./�5S      ��  � y�$ Visible=�6/   �� 5T       � �� �#FF00006/   5U      5Y      �� 5[      "  �6$   .�5]   
   ��  � ��$ Center=�6.   5^      �� 5`      "  ��6'   .�5a   
   ��  � ��
 ���� 6'   �� 5b   
    � ��$ 5q�6#   �� 5c   
    �@�Cursor$
 Hand��6#   �� 5d   
    � �� 5e      �� 5f      " ��6"   .R�5h      �� ���#FF0000$��2q�63   #��z��6G   ._�5i      �� $��Center=�6+   $��Center=�6H   5j      5k      5m       5n   
   P� 5o      
 �@J�IsMouseOver . . �5p      ��  � �� �#FF00006/   5q      5s      �� 5u      "  ��6   .�5v   
   ��  � �� �#FFFFFF6'   �� 5w   
    � ��$
 Bold"�6'   �� 5x   
    �@��Padding$ 	0,10,0,10q�6$   5y      �� 5|      "  ��6!   .�5}   
   ��  � ��$ Center=�60   �� 5~   
    � ��$ Center=�6.   5�      5�      