   M S B A M L   `   `   `  ����J  FFogOilAssistant, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Kclr-namespace:FogOilAssistant.Components.View.Pages.EmployeeControls.Frames=FogOilAssistant.Components.View.Pages.EmployeeControls.Frames  5      }Cclr-namespace:FogOilAssistant.Components.Models.Pages.Signed.Frames5FogOilAssistant.Components.Models.Pages.Signed.Frames  6a   R    LFogOilAssistant.Components.View.Pages.EmployeeControls.Frames.OrderManagment   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ]localKclr-namespace:FogOilAssistant.Components.View.Pages.EmployeeControls.Frames     5      Y	viewmodelCclr-namespace:FogOilAssistant.Components.Models.Pages.Signed.Frames     6a   -   5          N�Name$  user_page_context��5       N� Title$ OrderManagment��5       � 	Resources 5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $@ :/Components/Dictionary/Pages/Signed/ProfileDictionary.xaml?�6%   �� 5      $F @/Components/Dictionary/Pages/Signed/RefualingPageDictionary.xaml?�6%   �� 5      $5 //Components/Dictionary/Pages/OilDictionary.xaml?�6%   %    5       � DataContext 5      S   MFogOilAssistant.Components.Models.Pages.Signed.Frames.OrderManagmentViewModel  5   
   .��5      � .+�5   
   �� $��Disabled=�6   $��Hidden=�6@   .��5      �� ��6   �� *
Products+��5      �� 5      .R�5      � 5      ��5      �� 5      .R�5      �� $��2q�6?   $	��300��6S   $��10q�6_   $	��400��6k   ��6!   �� *Color+._�5       � .+�5!   "   �  �Name$ behind_block��6M   $
��Auto��6'   $
��Auto��65   $��0q�6B      product_behind#��[  6c    � RowDefinitions 5"   &   �� 5#   *   $��3* �68   �� 5$   *   $��* �68   �� 5%   *   $��* �68   .+�5'   &   � .+�5(   *   �� $��15q�6|   ��60   �� *	ImgCode+  ImageSourceConvertor �� 	Converter# [ 6|   5)   '   � 5*   &   $��2��6+   .+�5+   *   �� $��Center=�6n     description_text#��[ 6�   $��WrapWithOverflow=�6�   ��64   �� 	 �� Path$	 Status���6Q   �� *Color+6�   5,   '   � 5-   &   $��1��6+   .+�5.   *   �� $��Center=�6i   #��[ 6�   $��WrapWithOverflow=�6�   ��64   �� $	 ID�
 �� StringFormat$
 Order id: {0}��6�   5/   '   51   #   � 54   "    � ColumnDefinitions 55   &   �� 56   *   $
��0.2* �6;   �� 57   *   $
��0.6* �6;   �� 58   *   $
��0.2* �6;    5:   &   �� 5;   *   $
��0.4* �68   �� 5<   *   $
��0.2* �68   �� 5=   *   $
��0.4* �68   .+�5?   &   ��  ��Name$ over_btn��6-   $��1��5B   -   $��1��6:     about_btn_style#��[ 6J   ��5@   -   ��  �� ElementName$ user_page_context��$	 DataContext.ProcessOrder���5A   -   �� .��5C   *   �� $��Process��64   #��[ 6C   5D   '   5E   #   5F      5H      5I      5K      5L      � 5M   
    5N      �� 5O      $��0.15* �6#   �� 5P      $
��0.7* �6#   �� 5Q      $��0.15* �6#    5S      �� 5T      $��0.95* �6    �� 5U      $��0.05* �6    .+�5W      �� ���#ff0000$��2q�6�   ���#212122$��1��6�   y�6,   �� *AboutVisibility+   BooleanToVisibilityConverter# [ ._�5Y      �  6   �� *SelectedOrder+ 5Z      �� 5[      $
��0.4* �6+   �� 5\      $
��0.7* �6+    5^      �� 5_      $
��0.7* �6(   �� 5`      $
��0.3* �6(   .+�5b      � $��Center=�6   $	��Top=�68   .+�5c      �� $��20q�6l   ��6    �� *	ImgCode+# [ 6l   5d      �� 5f      $��20,0,0,0q�6'   $��2��6:   $��1��6L   $��Hidden=�6]   $��Hidden=�6�   .��5g      �� $
��Left=�6%   $��2��6@   $��1��6R    �� Orientation$ Vertical=�6b   .+�5h      �� $��
20,20,20,0q�6(   $
��Left=�6<     profile_btn_blocktext_style#��[ 6�   ��6W   �� *Name+$
 Product: {0}��6�   �� 5i      $��
20,0,20,20q�6(   $
��Left=�6<   #��[ 6�   ��6W   �� *tempID+$
 Product id: {0}��6�   �� 5j      $��20q�6(   $
��Left=�64   #��[ 6|   ��6P   �� *Price+$
 Price:��6|   �� 5k      $��
20,20,20,0q�6(   $
��Left=�6<   #��[ 6�   ��6W   �� *Color+��6u   �� *Status+6�   �� 5l      $��
20,0,20,20q�6(   $
��Left=�6<   #��[ 6�   ��6W   �� *Color+��6v   �� *Addres+$
 Adress: {0}��6�   �� 5n      $��
20,20,20,0q�6E   $
��Left=�6Y   #��[ 6�   ��6(   �� *Color+��6u   �� $	 userNick�$
 Client: {0}��6�   �� 5o      $��	20,0,20,0q�6E   $
��Left=�6X   #��[ 6�   ��6(   �� *Color+��6t   �� $	 userID�$
 Client id: {0}��6�   �� 5p      $��
20,0,20,20q�6E   $
��Left=�6Y   #��[ 6�   ��6(   �� *Color+��6u   �� $	 ID�$
 Order id: {0}��6�   �� 5r      $��
20,20,20,0q�6(   $
��Left=�6<   #��[ 6X   $��Descripion:��6�   �� 5s      $��
20,0,20,20q�6(   $	��450��6<   #��[ 6i   ��6L   �� *Description+6i   5v      5w      � 5x      $��1��6   .+�5{      � $��Center=�6$   $��Center=�6A   $��1��6^   .+�5|      �� $��5q�6w     profile_btn_style#��[ 6�   ��6&   �� $ user_page_context��$	 DataContext.CloseEditor�.��5}   "   �� $
��15,5q�6,   $��Close��6:   #��[ 6G   5~      �� 5      $��5q�6E   #��[ 5�   %   ��5   '   �� *Color+��5�   %   �� $ user_page_context��$!	 DataContext.ImplementAction���5�   %   �� .��5�   "   �� $
��15,5q�6,   #��[ 6�   ��6:   �� $ user_page_context��$	 DataContext.BtnText�6�   5�      �� 5�      $��5q�6E   #��[ 5�   %   ��5�   '   �� *Color+��5�   %   �� $ user_page_context��$ 	 DataContext.RejectSelected���5�   %   �� .��5�   "   �� $
��15,5q�6,   $��Reject��6:   #��[ 6H   5�      5�      5�      5�      5�      5�      5�      5�      