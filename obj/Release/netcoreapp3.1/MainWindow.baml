   M S B A M L   `   `   `  ���� S  OWPF_MD_Personnel_Records, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:WPF_MD_Personnel_RecordsWPF_MD_Personnel_Records  )    #WPF_MD_Personnel_Records.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     8local&clr-namespace:WPF_MD_Personnel_Records     V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     � Title$  
MainWindow��$	��450��$	��800��   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � 	Resources   TextBoxTemplate �� 
TargetType ��Name �� CornerRadius ��Name  RoundCorner �� 
TargetType	 �� Property
 �� Value �Name J� RenderTransformOrigin T� X �� ImageSource �� Triggers P� Property P� Value �� 
TargetName��%<  &	       &	 c     �� " ��.R��� $ Bd��#��z��#��z��$ 	10,0,0,10��
����  �._��� $ PART_ContentHost���� " ��.��� 	 � ��$
 1q��� 	 � ��
 �� " ��.R�� $
 grid��.+��� $ border��$ 	0,10,10,0��
����  �#��z��$ 0.5,0.5&�z�V� .~��� �� �� T� $ 0Q����� $ /View/search-icon.png�� P�  �@J�IsMouseOver$
 true��. ��� $ border��	 � ��
 �	#FF33962BP�  � ��$
 true��. ��� $ border��	 � ��
 �	#FF0000FFP�  � {�$ False��. ��� $
 grid��	 �@J�Opacity$

 0.25Q�.���� $ 2��
�������$��2,0,2,2q�._�� -   $ mainGridPanel�� � IsSharedSizeScope$
 True�� � RowDefinitions  � ColumnDefinitions �� $
��200* ��� $
��120* �.+��  �� $��* ��� $��40 �.+�� $��2,0,2,2q�$��1�� �� $��* ��� $��* �.+��� -    ��Name$ btn_Upd�����#6666ff$��3,0,2,2q�$��Stretch=�$��1��
�������$��Оновити��${�False���� -   $ btn_Del��${�False�����#ff5050$��2,0,3,2q�$��Stretch=�$��0��
�������$��Видалити���  �� $
��auto � z� SharedSizeGroup$ 	shareGrid���� $
��auto ��� $��* �$ 
MemberInfo���� $��* � �� $��10 ��� $
��auto ��� $��10 ��� $��* ��� $��10 �.+��� $	��150��$��1��$��1��._�� $	��Top=�$��Stretch=� �� $	��150 ��� $
��auto � �� $	��150 �.+��� -    ��Name$ picBox��$��Stretch=��� -   $ btn_ImgDwnld��$��Stretch=�$��1��$��Завантажити��$��10(�� $��1��$��3�� �� $��* ��� $��* �.+��� $��0��$��2,2,10,2q�._��  �� $��* ��� $��* ��� $��* �.+��� -    ��Name$ TB_Name��$��0�� % !MaterialDesignFloatingHintTextBox#��[ )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Ім'я��$��Bottom=� �� 
IsReadOnly ��  �� Path$ canEdit����� *Name+�� -   $ 
TB_Surname��$��1��#��[ $ Прізвище��$��Bottom=� �� $ canEdit����� *Name+�� -	   $ TB_Patromymic��$��2��#��[ $ По-батькові��$��Bottom=� �� $ canEdit����� *Name+�� $��1��$��2q�._��  �� $��* ��� $��* ��� $��* �.+��� -
   $ TB_Phone��$��0��#��[ $! Номер телефону��$��Bottom=� �� $ canEdit����� *Name+�� -   $ TB_Sex��$��1��#��[ $ 
Стать��$
 true��$��Bottom=����� *Name+�� -   $��positionBox�� & "MaterialDesignFloatingHintComboBox#��[ $��2��$��Bottom=�$ Група/Посада���� -   $ TB_Position��$��2��#��[ $ Група/Посада��$��Bottom=� �� $ canEdit����� *Name+�� -   $ TB_Notes��
����  �$��1q� �� TextWrapping$
 Wrap=� �� AcceptsReturn$
 true��$��1��$��3��$��3��$��0,50,0,2q� �� $ canEdit�.���� $��0,30,0,0q�$��1��$��3��$	��Top=�.��Нотатки:�� -    ��Name$ label_id��$y�Hidden=�$��Label��$��3��$
��Left=�$��
102,21,0,0q�$��3��$	��Top=��� $��0,2,0,0q�$��1��$ 	11,11,0,0��
�������._�� $��1�� �� $��30 �$ 	shareGrid���� $��* ��� $��40 �.+��� $ 3��
�������  �� Padding$  5,0,10,0q�$��5,1,5,1q�._�� $��Stretch=� �� $
��auto ��� $��* �.+�'  !MaterialDesignThemes.Wpf.PackIcon  $��0��$��Stretch=�$��Stretch=�!   Kind$! Magnify=�" J� Opacity$	" .56Q��� -   $ 	TB_Search��$��1��$��8 0 0 0q�$ 
Пошук��$	��200��$��Center=�� $��Stretch=� �� $	��90* ��� $��33 ��� $ 1��$��0,1,2,1q�$��1��
����  �$��1q�._��� -   # ��Name$# listBox���� -   $ btn_Add�����#009933$��2,0,2,2q�$��Stretch=�$��2��
�������$��Додати��