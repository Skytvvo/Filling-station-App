   M S B A M L   `   `   `  ����J  FFogOilAssistant, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullm;clr-namespace:FogOilAssistant.Components.View.LoadingWindow-FogOilAssistant.Components.View.LoadingWindow  5   	   c6clr-namespace:FogOilAssistant.Components.Models.Loader(FogOilAssistant.Components.Models.Loader  6S   :    4FogOilAssistant.Components.View.LoadingWindow.Loader   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Mlocal;clr-namespace:FogOilAssistant.Components.View.LoadingWindow     5   	   Evm6clr-namespace:FogOilAssistant.Components.Models.Loader     6S   
������   � AllowsTransparency  .  � WindowStyle$
 None=�5   $    � 
ResizeMode$ NoResize=�67    � WindowStartupLocation$ CenterScreen=�6M    � Title$ Loader��5   	   $	��500��6   $	��500��6%    � 	Resources 5         
text_style �� 
TargetType �� Property �� Value	 F� Color
 F� 	Direction F� ShadowDepth F� Opacity F� Softness  	btn_style �� 
TargetType �� Triggers P� Property P� Value��%�  &	        &	 %    �� 5   
   " ��6#   .�5      ��  � ��$ 25(�6)   �� 5       � �� �#fff6+   �� 5       � ��$
 Bold"�6+   �� 5       �@J�BitmapEffect 5      F� 5      $	 Black��6-   $	
 320Q�6;   $ 10Q�6K   $ .5Q�6\   $ 9Q�6i   5      5      �� 5   
   " ��6"   .�5      ��  � �� �#D8D8D86+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� ���#6634E8$��2q�67   #��z��6K   ._�5       �� $��Center=�6/   $��Center=�6L   5!      5"      5$       5%      P� 5&       �@J�IsMouseOver . . �5'      ��  � �� �#6634E863   5(      5*      5+      .��5,      � 
������  � ColumnDefinitions 5-   
   �� 5.      $��* �6   �� 5/      $��4* �6   �� 50      $��* �6    � RowDefinitions 52   
   �� 53      $��4* �6   �� 54      $��* �6   �� 55      $��* �6   .+�57   
   �� $-��'/Components/Images/logo_transparent.png��6   $��1��6B   ��58      �� 59      " ��6   .�5:      ��  �@J�BitmapEffect 5;      F� 5<      $	 Black��65   $	
 320Q�6C   $ 10Q�6S   $ .5Q�6d   $ 9Q�6q   5>      5?      5A      �� 5B   
   $��Center=�6   $��3��62   $��1��6F    �� Orientation$ Vertical=�6S   .+�5C      �� $ 
Horizontal=�6   $��Center=�62   .+�5D      �� $��Loading ��6   #��[  6,   �� 5E      -   5         ��Name$ percent��5E      #��[  6x   ��6-   �� *	Percent+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6x   5G      �� 5H      -   5        $ status��5H      $��Center=�6q   #��[  6�   ��6(   �� *Status+$ TwoWay=�$ PropertyChanged=�6�   5J      �� 5L   
   $��Center=�6p   $��Center=�6�   $��2��6�   $��3��6�   y�6   �� *
Visibility+   BooleanToVisibilityConverter �� 	Converter# [ .+�5M      �� #��[ 62   ��6   �� *
CloseApp+.��5N      �� $
��Exit��6   #��[  6(   $��30,10q�6L   5P      5Q      5R      5S      5T      