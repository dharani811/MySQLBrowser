   M S B A M L   `   `   `  ����?  ;Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Main.Views
Main.Views  5          Main.Views.QueryBox   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:Main.Views     5      -   5        $��queryControl��5   7   .��5	      �   � RowDefinitions  5
   
   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   �� -   5        $��queryAreaBox��5      $
��Left=�6*   . �5      /� .T�5      J� .*�6   .*�5      �� �6   ��  �� Path$ QueryString�5      5      5      �� 5   
   -   5        $��	resultBox��5      $
��Left=�6(   $��2��6>    �� 
IsReadOnly . . �5      /� .T�5      J� .*�6   .*�5      �� �6   �� $ Message�5      5      5       �� 5%   
   $��1��6   $��10,0,10,282q�6$   Select Database6J   �� 5&   
   -   5        $��	dbListBox��5&   ^   $��1��6   $
��Left=�6$    �� SelectedIndex$ 0��6:    J� 	AllowDrop . $��10,32,10,243q�6�   ��6o   �� $ DataBaseList� �� SelectedItem 6�   �� $ CurrentDataBase�6�   �� 5(   
   $��1��6   $��10,174,10,92q�6n   {�6#   �� $ IsCancelEnabled���6N   �� $ Cancel�.��6�   Cancel6�   �� 5)   
   $��1��6   $��10,92,10,174q�6p   {�6#   �� $ IsExecuteEnabled���6O   �� $ Execute�.��6�   	Execute6�   �� 5*   
   $��1��6   $��Clear��6b   $��10,255,10,10q�6r   {�6#   �� *IsClearEnabled+��6H   �� *Clear+6r   5.      5/      