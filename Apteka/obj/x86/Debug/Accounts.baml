   M S B A M L   `   `   `  ����
  Apteka    Apteka.Accounts   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Konta użytkowników��5   	   $	��300��6$   $	��339��61    � 
ResizeMode$ NoResize=�6=    � WindowStartupLocation$ CenterScreen=�6S   .��5      � $	��373��6   $	��217��6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��	KontaGrid��5   j      AutoGenerateColumns .  $	��151��6/   $
��Left=�6<   $��	18,20,0,0q�6W   $	��Top=�6{   $	��208��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Nazwa uzytkownika��6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ��  �� Path$ Nazwa_uzytkownika�6P     5	      $ Hasło��6D    6%   �� $ Haslo�6D     5
      $ Prawo��6D    6%   �� $ Prawo�6D   5      �� 5   
   -   5        $	��Add��5   [   $
��Nowy��6   $��41��6    $
��Left=�6,   $��
232,20,0,0q�6G   $	��Top=�6f   $��75��6~   6�   �� 5   
   -   5        $
��Edit��5   \   $��Zmień��6   $��41��6!   $
��Left=�6-   $��
232,67,0,0q�6H   $	��Top=�6h   $��75��6�   6�   �� 5   
   -   5        $��Delete��5   \   $��Usuń��6   $��41��6    $
��Left=�6,   $��232,114,0,0q�6G   $	��Top=�6j   $��75��6�   6�   �� 5   
   -   5        $��Zamknij��5   _   $��Zamknij��6   $��23��6#   $
��Left=�6/   $��232,184,0,0q�6J   $	��Top=�6n   $��75��6�   6�   �� 5   
   -   5        $��label1��5   h   $��A - Administrator��6   $��28��6,   $
��Left=�68   $��134,166,0,0q�6S   $	��Top=�6v   �� 5   
   -   5        $��label2��5   e   $��U - Użytkownik��6   $��28��6)   $
��Left=�65   $��134,179,0,0q�6P   $	��Top=�6s   5      5      