   M S B A M L   `   `   `  ����S  OWPF_MD_Personnel_Records, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:WPF_MD_Personnel_RecordsWPF_MD_Personnel_Records  5      *    $WPF_MD_Personnel_Records.Pages.Page1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:WPF_MD_Personnel_Records     5      V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
        N� Title$  Page1��5	         MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper N� 
Background# �  5   	     MaterialDesignFont N� 
FontFamily# �  5   	    � 	Resources 5       	 Butto �� 
TargetType " MaterialDesignFlatDarkBgButton �� BasedOn �� Triggers	 �� Binding
 �� ElementName �� Path �� Value �� Property �� Value��%  &	       7  '�� �     7���� 5   
   " ��6   8    5      �� 5      	 6   �� $
 	btnCancel��$ Content�
 ahahah6V   5      5      �� 5   
   " ��6   8   .�5      ��  � ��$ 0q�6'   �� 5       � ��
 �����6+   5      5      .��5"      �  � RowDefinitions 5#   
   �� 5$      $��* �6   �� 5%      $��40 �6   .+�5'   
   �� 
����  �$��2q�6(   ._�5(   
   �� $��0,6,0,0q�6   $��0��6"   ._�5)      �� $��Hidden=�6   .��5*      �  5+      �� 5,      $
��auto �6(   �� 5-      $
��auto �6(   .+�50      �� .+�51      �� $	��150��6!   $	��150��6-   ._�52      �� -   5         ��Name$ picBox��52   $   $��Stretch=�64   ��53   $   �� *DisplayedImageInCreatorPage+ �� Mode$ TwoWay=�54      �� 55      -   5         ��Name$ btn_ImgDwnld��55   J   $	��150��6!   $��Stretch=�6-   $��CreatorPage��56   ?   $��Завантажити��57      $��14(�6/   ��56   !   �� *	LoadPhoto+57   /   58      �� 5;      $��1��6"   $��Center=�6/   $	��250��6L   .+�5=      �� -   5         ��Name$ TB_Name��5=   "    % !MaterialDesignFloatingHintTextBox#��[ 5>   %   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Ім'я��5?   %   $��Bottom=�5C   %   ��5@   %   �� *CreatedMember.name+$ TwoWay=� �� ValidatesOnDataErrors$
 True�� �� UpdateSourceTrigger$ PropertyChanged=�5C   %   �� 5D      -   5        $ 
TB_Surname��5D   "   #��[ 5E   %   $ Прізвище��5F   %   $��Bottom=�5I   %   ��5G   %   �� *CreatedMember.surname+$ TwoWay=�$
 True��$ PropertyChanged=�5I   %   �� 5J      -   5        $ TB_Patromymic��5J   "   #��[ 5K   %   $ По-батькові��5L   %   $��Bottom=�5O   %   ��5M   %   �� *CreatedMember.patronymic+$ TwoWay=�$
 True��$ PropertyChanged=�5O   %   �� 5P      -   5        $ TB_Phone��5P   "   #��[ 5Q   %   $! Номер телефону��5R   %   $��Bottom=�5T   %   ��5S   %   �� *CreatedMember.phoneNum+$ TwoWay=�5T   %   �� 5V      -   5        $��positionBox��5W   +    & "MaterialDesignFloatingHintComboBox#��[ 5V   '   $��2��5W   >   $��Bottom=�6K   $ Група/Посада��5X   +   $��0,2,0,0q�5Y   '   ��5Z   +   �� *	positions+ �� SelectedItem 5[   +   �� *CreatedMember.position+$ TwoWay=�$
 True��$ PropertyChanged=���5]   "   �� 5^   &   .R�5_   *   �� ��65   �� *
position+6Q   5`   '   5b      �� 5d       �� Orientation$ 
Horizontal=�6)   .+�5e   "   	� -   5         	�Name$ RadioBtnMan��5e   .   $��	0,30,10,0q�6C   $��Чол��5f   .    d� 	IsChecked$
 TrueY�6<   $��чол��5h   L   ��6.   �� *	SetGender+6L   	� 5i   "   -	   5        $ RadioBtnWoman��5i   .   $��	0,30,10,0q�6E   $��Жін��5j   .   $��жін��5k   L   ��6.   �� *	SetGender+6L   5l      �� 5m      $��0,20,0,0q�6$   $��1��66   $��3��6F   $	��Top=�6S   .��6k   Нотатки:5o      �� 5p      ��6&   �� *CreatedMember.Errors+$ TwoWay=���5q   "   �� 5r   &   .R�5s   *   �� ��64   �� 6G   5t   '   5v      �� 5w      -
   5        $ TB_Notes��5w   &   
����  �$��1q�5x       �� TextWrapping$
 Wrap=�62    �� AcceptsReturn$
 true��6F   $	��100��5y      $��0,0,0,2q�5z      ��5{   "   �� *CreatedMember.note+$ TwoWay=�$ 	LostFocus=�.��5|   #   5}      5      5�      5�      5�      5�      � 5�   
   $��2��6   .+�5�      �  � ColumnDefinitions 5�      �� 5�      $��* �6'   �� 5�      $��* �6'   .+�5�      �� -   5        $ btnAdd��5�      $��3q�6*   $��35��65   ���#009933$��Додати��5�   .   ��5�      �� $ AddMemberToDB�5�      �� 5�      -   5        $ 	btnCancel��5�      $��3q�6,   $��35��68   $��1��5�      
�������$��Відмінити��6;   ��5�      �� *HideMemberBuilder+5�      5�      5�      5�      5�      