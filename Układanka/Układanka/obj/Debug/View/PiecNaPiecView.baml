   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      b ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1lMclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.PlatformGalaSoft.MvvmLight.Command 5      E AUkładanka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Układanka.ViewUkładanka.View 5	      $   Układanka.View.PiecNaPiecView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      ]cmdMclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.Platform     5      /localclr-namespace:Układanka.View     5	        N� Title$  
Układanka��5      $	��670��6   $	��850��6&    � UseLayoutRounding .  � DataContext 5      �� *Piec+   Locator �� Source# [  .��5      ]� 
�������.+�5   
   � -   5         �Name$ GridMain��5      $
��auto��6!   $
��Left=�6.    � RowDefinitions 5      �� 5      $��50 �6    6/   �� 5      $��* �6    6-   .+�5      �� 	 �� X1$ 0��6   	 �� Y1$	 100��6   	 �� X2$	 850��6#   		 �� Y2$		 100��6,   
�������$��5��6D   �� 5      $	��130��6   $��30��6$   $��18(�60   $
��Left=�6>   $��30 0 0 0q�6Y   $��0��6k   Liczba kliknięć:6�   �� 5      $	��130��6   $��30��6"   $��18(�6.   $��Center=�6<   $��	0 0 400 0q�6Y   {�.  $��0��5   (   ��65   �� *	MyCounter+6R   �� 5      $��50��6   $��30��6#   $��18(�6/   $��Center=�6=   $��0 0 50 0q�5      $��0��6*   Czas:6>   �� 5      $	��130��6   $��30��6"   $��18(�6.   $��Center=�6<   $��	140 0 0 0q�6Y   {�.  $��0��5    (   ��65   �� *CurrentTime+6T   �� 5"      $��Disabled=�5#       $��Disabled=�6V   
�������$��1��5$   ,   ��5"      �� *
GameList+��5%      �� 5&      .R�5'      � $	��Top=�6!   
 � ColumnDefinitions
 5(      �� 5)   "   $
��auto �63   �� 5*   "   $
��auto �63   �� 5+   "   $
��auto �63   �� 5,   "   $
��auto �63   �� 5-   "   $
��auto �63    5/      �� 50   "   $
��auto �60   �� 51   "   $
��auto �60   �� 52   "   $
��auto �60   �� 53   "   $
��auto �60   �� 54   "   $
��auto �60   56      57      ��59      �� 5:       �� 
TargetType" ��6   �5;      �� 5<       �� Property � �� �� Value 69   �� *Row+�� 5=       � �� 6<   �� *Col+�� 5>       � ��
 �����6;   �� 5?       � |� .  6:   5A      ��5D      �� 5E      .R�5F      �� $	��155��69   $	��118��6E   $
��Fill=�6R   ��6    �� *Image+.   (System.Windows.Interactivity.Interaction   Triggers 5G      /   )System.Windows.Interactivity.EventTrigger  5H   "      	EventName$ MouseLeftButtonUp��61   .   (System.Windows.Interactivity.TriggerBase   Actions. 5I   &   /  )GalaSoft.MvvmLight.Command.EventToCommand     Command 69   �� *DataContext.MouseClicked+ �� RelativeSource �� *FindAncestor+ �� AncestorType" N�   CommandParameter 6�   �� 5J   #   5L      5M      5O      5P      5R       5S        5T   
   $ Loaded��6   . 5U         6!   �� *OnLoad+5V      5X      