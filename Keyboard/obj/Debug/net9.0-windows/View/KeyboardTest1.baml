   M S B A M L   `   `   `  ����C  ?Keyboard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Keyboard.ViewModelsKeyboard.ViewModels  5      !    Keyboard.View.KeyboardTest1   5      W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      8
viewmodels!clr-namespace:Keyboard.ViewModels     5        � Title$  KeyboardTest1��5	      $
��1080��5
      $	��450��5       � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�5       � 	Resources 5         KeyboardButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  DualSymbolButtonTemplate	 �� RelativeSource
 �� AncestorType
 � Tag �� Orientation  SimpleButtonTemplate��%  &	        &	 �    7   &	 +    7   �� 5   
   " ��6,   .�5      ��  � ��$ 50��6&   �� 5       � ��$ 50��6'   �� 5       � ��$ 4q�6'   �� 5       � ��$ Segoe UI)�6+   �� 5       � ��$ 22(�6)   �� 5       � ��$ SemiBold"�6+   �� 5       � �� �#F2F2F26+   �� 5       � �� �#DDDDDD6,   �� 5       � ��$ 1q�60   �� 5       � �� �#2226+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5    !   #��z��5!   !   $ 8��5"   !   ._�5#      �� $��Center=�6/   $��Center=�6L   5$      5%      5'      5(      �� 5*   
   .R�5+      �� 8��  ��5,      �� *DataContext.KeyPressCommand+	 �� 

 Window��5-      �� *Tag+ 5/      �� *Tag+.��50      �� $ Vertical=�6   .+�51      �� $��Center=�6    $��12(�6=   ��6K   �� *	Shifted+�� 52      $��Center=�6    $��16(�6=   $
��Bold"�6K   ��6]   �� *Normal+53      54      55      �� 57   
   .R�58      �� 8��  ��59      �� *DataContext.KeyPressCommand+	 �� 

 Window��5:      �� *Normal+��5;      �� *Normal+5<      5=      5>      .��5@      � .+�5A   
   (�  (� Stretch$ Uniform=�6   .��5B      �� $��10q�6   $ Vertical=�6%   .+�5C      �� $	��0,4q�6   #��[ 5D      ��5E      �� *NumberRowKeys+��5F      �� 5G      .R�5H      �� $ 
Horizontal=�6)   5I      5K      �� 5M      $	��0,4q�6   #��[ 5N      ��5O      �� *QwertyRowKeys+��5P      �� 5Q      .R�5R      �� $ 
Horizontal=�6)   5S      5U      �� 5W      $	��0,4q�6   #��[ 5X      ��5Y      �� *ControlRowKeys+��5Z      �� 5[      .R�5\      �� $��Center=�6)   $ 
Horizontal=�6F   5]      5_      5`      5a      5b      5c      