   M S B A M L   `   `   `  ���� G  CWpfSchemaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WpfSchemaAppWpfSchemaApp       WpfSchemaApp.EditQuestions   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:WpfSchemaApp       � Title$  Edit Questions��$	��600��$	��800��
����� �
�������.���  � ColumnDefinitions �� $	��19* ��� $
��245* �.+��� $
��Left=�$��	25,10,0,0q�$
��Wrap=�$"��Digital Reporting made easy!��$��20(�$��Oblique$�$	��Top=�$	��529��$��2���� -    ��Name$ 
dataStatus��$��Right=�$��0,10,0,0q�$
��Wrap=�$
��test��$��10(�$��Oblique$�$	��Top=�$	��104��$��1���� $
��Left=�$��625,423,0,0q�$
��Wrap=�$V��PAdmin Priviliges enabled. Carefull, you don't want to delete something precious!��$��10(�$��Oblique$�$	��Top=�$	��100��$��67��$��1���� -    ��Name$ BackBtn��$
��Back��$
��Left=�$��625,490,0,0q�$	��Top=�$	��100��$��40��$��1���� $
��Left=�$��	25,57,0,0q�$
��Wrap=�$��
Questions:��$��20(�$��Oblique$�$	��Top=�$	��134�� J� RenderTransformOrigin$ -0.157,0.444&�$��2���� -   $ answerTxtTittle��$
��Left=�$��169,157,0,0q�$
��Wrap=�$��Answers:��$��20(�$��Oblique$�$	��Top=�$	��134��$��1��� -    �Name$ LoadingGrid��$
��Left=�$	��406��$��	25,84,0,0q�$	��Top=�$	��134��
�������$��2��� -   $ ALoadingGrid��$
��Left=�$��82��$��
169,66,0,0q�$	��Top=�$	��367��$��1�� � RowDefinitions �� $	��35* ��� $	��47* �.+��� $
��Left=�$��0,24.264,0,0q�$
��Wrap=�$	��Nr:��$	��Top=�$ -0.312,0.562&�$��1���� -    ��Name$
 tQnr��$
��Left=�$��16��$��30,24.264,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$��30��$ -0.4,-0.375&�$��1���� $
��Left=�$
��Wrap=�$��Question text:��$	��Top=��� -   $ tQtxt��$
��Left=�$��75��$��79,0,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��285��$��2��� -   $ addAltButtonBox��$��536,66,30,421q�$��1��.+��� -	   $ 
saveQueBtn��$
��Save��$
��Left=�$	��Top=�$��70��$��30���� -
   $ 	addAltBtn��$��Add Alt��$
��Left=�$	��Top=�$��70��$��30��$��0,42,0,0q��� -   $ 	newQueBtn��$	��New��$
��Left=�$	��Top=�$��70��$��72��$��75,0,0,0q�� -   $ AlternativGrid1��$	��100��$	��100��$
��Left=�$	��Top=�$��169,189,0,0q�$��1�� �� $	��23* ��� $	��77* � �� $	��37* ��� $	��24* �� -   $ AlternativGrid2��$	��312��$	��100��$
��Left=�$	��Top=�$��274,189,0,0q�$��1��