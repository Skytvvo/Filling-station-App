   M S B A M L   `   `   `  ����J  FFogOilAssistant, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Kclr-namespace:FogOilAssistant.Components.View.Pages.EmployeeControls.Frames=FogOilAssistant.Components.View.Pages.EmployeeControls.Frames  5      }Cclr-namespace:FogOilAssistant.Components.Models.Pages.Signed.Frames5FogOilAssistant.Components.Models.Pages.Signed.Frames  5      Dclr-namespace:FogOilAssistant.Components.View.Pages.EmployeeControls6FogOilAssistant.Components.View.Pages.EmployeeControls  5      T    NFogOilAssistant.Components.View.Pages.EmployeeControls.Frames.ProductManagment   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ]localKclr-namespace:FogOilAssistant.Components.View.Pages.EmployeeControls.Frames     5      RvmCclr-namespace:FogOilAssistant.Components.Models.Pages.Signed.Frames     5      b ^SharpVectors.Converters.Wpf, Version=1.7.1.0, Culture=neutral, PublicKeyToken=b532964b8548be771svgc&http://sharpvectors.codeplex.com/svgc/  6V   U
modtextboxDclr-namespace:FogOilAssistant.Components.View.Pages.EmployeeControls  5      -   5          N�Name$  product_m_page��5       N� Title$ ProductManagment��5
       � DataContext 5      U   OFogOilAssistant.Components.Models.Pages.Signed.Frames.ViewModelProductManagment  5   
    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $@ :/Components/Dictionary/Pages/Signed/ProfileDictionary.xaml?�6%   �� 5      $F @/Components/Dictionary/Pages/Signed/RefualingPageDictionary.xaml?�6%   �� 5      $5 //Components/Dictionary/Pages/OilDictionary.xaml?�6%   %    5      .��5      � .+�5   
   �� $��Hidden=�6   $��Hidden=�6>   .��5      �� ��6   �� *
Products+��5      �� 5      .R�5      ��  �� Orientation$ Vertical=�6%   5      ��5       �� 5!      .R�5"      � .+�5#      �  �Name$ behind_block��6#      product_behind#��[  69    � ColumnDefinitions 5$   "   �� 5%   &   $��2* �67   �� 5&   &   $��2* �67   �� 5'   &   $��1* �67   �� 5(   &   $	��500 �67   .+�5+   "   �  5,   &   �� 5-   *   $
��0.1* �6;   �� 5.   *   $
��0.8* �6;   �� 5/   *   $
��0.1* �6;   	 � RowDefinitions	 51   &   �� 52   *   $
��0.1* �68   �� 53   *   $
��0.8* �68   �� 54   *   $
��0.1* �68   .+�56   &   �� $��1��6,   $��1��6<   ��6I   �� *	ImgCode+  ImageSourceConvertor
 �� 	Converter#
 [ 57   #   �� 58   "     description_text#��[ 6B   $��1��6l   ��6,   �� *Name+6l   �� 59   "   $��2��6,   #��[ 6�   ��6<   �� *Price+ �� ValidatesOnExceptions .  �� StringFormat$ {0} Oil��6�   �� 5:   "   $��3��6,   $
��Wrap=�6<   $
��Auto��6P   #��[ 6{   ��6^   �� *Description+6{   5;      �� 5<      $��Center=�6)   $��Center=�6F   .+�5=   "   ��  ��Name$ over_btn��5?   %     about_btn_style#��[ 5@   %   ��5>   %   ��  �� ElementName$ product_m_page�� �� Path$ DataContext.EditProduct���5A   %   �� .��5C   &   �� $
��Edit��60   #��[ 6<   $��20,10q�6f   5D   #   5E      5F      5G      5I      5J      � 5L   
   y�6   �� *EditVisibility+   BooleanToVisibilityConverter#
 [  5M      �� 5N      $
��0.1* �6#   �� 5O      $
��0.8* �6#   �� 5P      $
��0.1* �6#   	 5R      �� 5S      $��0.95* �6    �� 5T      $��0.05* �6    .+�5W      � $��1��6   .+�5X      �� $��2q�6   ���#ff0000���	#FF222121._�5Y      �  5Z      �� 5[      $��* �6/   �� 5\      $��* �6/   .+�5`      �  5a      �� 5b   "   $
��0.1* �63   �� 5c   "   $
��0.8* �63   �� 5d   "   $
��0.1* �63   	 5f      �� 5g   "   $
��0.4* �60   �� 5h   "   $
��0.6* �60   .+�5j      � $��1��6#   .+�5l   "   �� $��10q�6)   $��2q�65   ���#FF0000._�5m   &   � .+�5n   *   �� -   5         ��Name$ changing_picture��5n   0   $��10q�6J   ��6W   �� *ProductImage+��5o   .   �� 5p   2    �� 
TargetType" ��68    �� Triggers 5q   6   �� 5r   :    �� Binding 6F   �� $ changing_picture_btn��$ IsMouseOver� �� Value True6�   .a�5s   >   ��  �� Property �@J�Effect �� Value 5t   B   -  'System.Windows.Media.Effects.BlurEffect  5u   F      Radius$ 20Q�6Q      
KernelType$ Gaussian=�6]   5w   ?   5x   ;   5z   3   5|   +   �� 5}   *   -   5        $ changing_picture_btn��5}   ~     change_picture#��[ 6�   ��61   �� $ product_m_page��$ DataContext.UploadImage�.��5~   .   � {�.   5   2   �� 5�   6   $
��0.3* �6G   �� 5�   6   $
��0.4* �6G   �� 5�   6   $
��0.3* �6G   	 5�   2   �� 5�   6   $
��0.3* �6D   �� 5�   6   $
��0.4* �6D   �� 5�   6   $
��0.3* �6D   .+�5�   2   (  "SharpVectors.Converters.SvgViewbox  $��1��6B   $��1��6R      Source$# /Components/Images/upload.svg?�6_   5�   /   5�   +   5�   '   5�   #   5�      �� 5�      $ Vertical=�6)   $��1��6@   $��1��6P   .+�5�   "   �� $��Name:��6,   #��[ 69   $
��0,10q�6c   �� 5�   "     
user_input#��[ 6x   $
��0,10q�6�   ��6*   �� *ProductName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5�   "   $��Price:��6,   #��[ 6:   $
��0,10q�6d   �� 5�   "    �� 	MaxLength$ 5��6*   #��[ 6�   $
��0,10q�6�   ��69   �� $ product_m_page��$ DataContext.ProductPrice�$ TwoWay=�$ PropertyChanged=�6�   � 5�   "   $��Stretch=�6,   .+�5�   &   ��   profile_btn_style#��[ 6M   ��6-   �� *CloseEditor+.��5�   *   �� $��Cancel��64   #��[ 6B   5�   '   �� 5�   &   #��[ 6N   ��6-   �� *SaveSelected+.��5�   *   �� $
��Save��64   #��[ 6@   5�   '   �� 5�   &   #��[ 6P   ��6-   �� *DeleteSelected+.��5�   *   �� $��Delete��64   #��[ 6B   5�   '   5�   #   5�      5�      �� 5�      $��Disabled=�6'   $��Hidden=�6P   $��1��6u   .��5�      �� $ Vertical=�6)   .+�5�   "   �� $��Description:��6,   #��[ 6@   M   GFogOilAssistant.Components.View.Pages.EmployeeControls.ModifinedTextBox  5�   "      Description 6>   �� *Description+5�      5�      5�      5�      5�      5�      �� 5�   
   $
��Left=�6   $��Bottom=�60   .+�5�      �� #��[ 64   ��6   �� *
AddProduct+.��5�      �� $	��Add��6   #��[ 6'   5�      5�      5�      5�      