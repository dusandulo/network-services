   M S B A M L   `   `   `  ����I  ENetworkService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:NetworkService.ViewsNetworkService.Views  5	      C&clr-namespace:NetworkService.ViewModelNetworkService.ViewModel  5
      .    (NetworkService.Views.NetworkEntitiesView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      ` \MindFusion.Keyboard.Wpf, Version=5.0.3.450, Culture=neutral, PublicKeyToken=dd7449df9ffcc455*kb!http://mindfusion.eu/keyboard/wpf  5      ^ ZMindFusion.Themes.Wpf, Version=1.1.3.450, Culture=neutral, PublicKeyToken=dd7449df9ffcc455.kbThemeshttp://mindfusion.eu/themes/wpf  5      .local"clr-namespace:NetworkService.Views  5	      6	viewModel&clr-namespace:NetworkService.ViewModel  5
      .��5      �   � RowDefinitions  5   
   �� 5      $��70 �6   �� 5      $��30 �6   �� 5      $��62 �6   �� 5      $	��320 �6   �� 5      $��35 �6   �� 5      .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   .+�5      �� $��10q�6D   ��6   ��    Logo �� Source# [  6D   �� 5      $
��Bold"�6   $��Network Services��6*   $��25(�6B   $��30 15 15 15q�6P   5      �� 5   
   $��Medium"�6   $��1��6(   $��2��65   $��Filter��6F   $��20(�6T   �� 5   
   $��Medium"�6   
����������#666666$��Reset��6t   $��1��6�   $��Center=�6�   $��Center=�6�   $��25��6�   $��88��6�   $��	235 5 0 0q�6�   ��6M   �� *ResetFilterCommand+6�   � 5   
   $��2��6    � ColumnDefinitions 5      �� 5       �� 5!      �� 5"      $��20 �6#   �� 5#      $��20 �6#   �� 5$      .+�5&      �� $��0��6   .+�5'      �� $��Medium"�6   $
��Type��60   $��5 0 0 0q�6<   �� 5(      $��20��6   $��0��6�   $��	10 2 10 0q�6�   ��6'   �� *ComboBoxItems+ �� SelectedItem 6M   �� *SelectedEntityTypeToFilter+ �� Mode$ TwoWay=�6�   5)      �� 5*      $��1��6   .+�5+      �� $��Medium"�6   $��ID��60   $��5 0 0 0q�6:   �� 5,      $��20��6   $��0��6a   $��	10 3 10 0q�6q   ��6&   �� *SelectedIdMarginToFilterText+$ TwoWay=�.��6�   .  (System.Windows.Interactivity.Interaction   Triggers 5-      /  )System.Windows.Interactivity.EventTrigger  5.         	EventName$ GotKeyboardFocus��6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5/      6  0System.Windows.Interactivity.InvokeCommandAction  	   Command	 64   �� *KeyBoardFocusedCommand+
   CommandParameter
 50   0   ��  �� ElementName$ search��51      .��52   .   53      54      �� 55      $��2��6   .+�56      	� $��2q�6_    d� 	IsChecked 6   �� *IsLessThanRadioButtonSelected+$ TwoWay=�6_   	� 57      $��2 0 0 0q�6b    6   �� *" IsGreaterThanRadioButtonSelected+$ TwoWay=�6b   	� 58      $��2 2 0 0q�6a    6   �� *!IsEqualsThanRadioButtonSelected+$ TwoWay=�6a   59      �� 5:      $��3��6   .+�5;      �� $
��Bold"�6   <64   �� 5<      $
��Bold"�6   $��>��6.   �� 5=      $
��Bold"�6   $��=��6.   5>      �� 5?      $��4��6   .+�5@      �� $��Medium"�6   
����������#666666$��Apply��6}   $��5 15 5 5q�6�   $��25��6�   ��6U   �� *FilterEntityCommand+6�   �� 5A      $��10 0 0 0q�6   
���  ����6.   �� *FilterErrorMessage+6R   5B      5C      �� 5E   
   $��3��6   y�6   �� *KeyboardNotVisible+._�5F      �� $ Vertical=�6   $��Center=�60   .+�5G      &   System.Windows.Controls.DataGrid  $	��315��6B   $	��300��6N   $��0 10 0 0q�6[      AutoGenerateColumns .     CanUserAddRows .     CanUserReorderColumns .     CanUserResizeColumns .  "   VerticalScrollBarVisibility$ Hidden=�6     	RowHeight$ 20Q�6(     
IsReadOnly .    SelectionMode$ Single=�6I  ��6   �� *EntitiesToShow+ 6m   �� *SelectedEntity+ � 	Resources 5H      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value*  $System.Windows.Controls.DataGridCell �� 
TargetType��%,  '        '  L     �� 5I      "  6    .�5J      ��  � ��$ Center=�6K   5K      �� 5L      "  6    .�5M      ��  � �� 5N   "   �� 5O   &   "  66   .R�5P   *   � #��z��6/   .+�5Q   .   �� $��Center=�6?   $��Center=�6Z   5R   +   5S   '   5U      5V      5W         Columns 5X      0  *System.Windows.Controls.DataGridTextColumn  5Y      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ ID��6-    	  Width5
  /System.Windows.Controls.DataGridLengthConverter$ *
 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Id+6P     5Z      $
 Name��6-   $ *
 6T    6;   �� *Name+6T     5[      $
 Type��6-   $ 90
 6Y    6;   �� *	Type.Name+6Y     5\      $ Value��6-   $ 90
 6V    6<   �� *Value+6V   5^      5_      5`      �� 5b   
   $��4��6   $ 
Horizontal=�6"   .+�5c      �� $��Medium"�6   $	��Add��6,   $��20(�67   $��5q�6E   �� 5d      $��Medium"�6   
���   ����#ff8080$��Delete��6y   $��	212 5 0 5q�6�   $��71��6�   ��6Q   �� *DeleteEntityCommand+6�   5e      � 5f   
   $��5��6    5g      �� 5h      �� 5i      �� 5j      �� 5k      .+�5m      �� $��0��6   .+�5n      �� $��Medium"�6   $��ID��60   $��10 0 0 0q�6:   �� 5o      $��20��6   $��	10 3 10 0q�6Y   ��6&   �� *CurrentEntity.TextId+$ TwoWay=�.��6l    5p        5q      $ GotKeyboardFocus��6)   . 5r        	 64   �� *KeyBoardFocusedCommand+
 5s   0   �� $ search��5t      .��5u   .   5v      �� 5w      $��10 3 0 0q�6   
���  ��$��20��6s   ��6.   �� *$"CurrentEntity.ValidationErrors[Id]+6s   5x      �� 5y      $��1��6   .+�5z      �� $��Medium"�6   $
��Type��60   $��10 0 0 0q�6<   �� 5{      $��0��6~   $��	10 2 10 0q�6�   $��20��6�   ��6   �� *ComboBoxItems+ 6A   �� *CurrentEntityType.Name+$ TwoWay=�6�   �� 5|      $��10 3 0 0q�6   $��20��6.   
���  ����6:   �� **(CurrentEntityType.ValidationErrors[Name]+6t   5}      �� 5~      $��2��6   .+�5      �� $��Medium"�6   $
��Name��60   $��10 0 0 0q�6<   �� 5�      $��20��6   $��0��6W   $��	10 3 10 0q�6g   ��6&   �� *CurrentEntity.Name+$ TwoWay=�.��6z    5�        5�      $ GotKeyboardFocus��6)   . 5�        	 64   �� *KeyBoardFocusedCommand+
 5�   0   �� $ search��5�      .��5�   .   5�      �� 5�      $��10 3 0 0q�6   $��20��6.   
���  ����6:   �� *&$CurrentEntity.ValidationErrors[Name]+6p   5�      �� 5�      $��3��6   .+�5�      �� $��Medium"�6   
����������#666666$��20��6z   $��4��6�   $	��Add��6�   $��	5 18 5 61q�6�   ��6U   �� *AddEntityCommand+6�   5�      5�      �� 5�   
   $��3��6   $��Center=�6E   $��Center=�6b    �� CornerRadius$ 25��6}   $��1q�6�   $��20 10q�6�   y�6   �� *KeyboardVisible+._�5�      �� .+�5�      �� $��X��6   $��20��6G   $��20��6R   $��0 5 10 0q�6^   $��Right=�6p   $��Center=�6�   
���  ����6%   �� *CloseKeyboard+6�   '  !MindFusion.UI.Wpf.VirtualKeyboard  5�         
AutoRepeat .      IsStandAlone  .  !   Stretch$! Uniform=�6N   $��Center=�6`   $��Center=�6}   $��5 0 5 5q�6�   5�      5�      *  $MindFusion.UI.Wpf.ColorSchemeChanger  5�   
   $y�Hidden=�6&   " �� SelectedIndex$" 8��6:   5�      5�      