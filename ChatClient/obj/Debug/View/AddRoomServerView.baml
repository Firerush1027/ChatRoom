   M S B A M L   `   `   `  ????E  AChatClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ChatClient
ChatClient  5   	   1clr-namespace:ChatClient.ViewChatClient.View  5   	   /clr-namespace:ChatClient.VMLChatClient.VML  5   	   '    !ChatClient.View.AddRoomServerView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:ChatClient     5   	   /viewsclr-namespace:ChatClient.View     5   	   ,vmlclr-namespace:ChatClient.VML     5   	   -   5          ?Name$  AddDialogWindow??5   	   %   ChatClient.VML.ViewModelLocator   AutoHookedUpViewModel .  ? Title$  Connect Room Server Dialog??5   	   $??385.828??6,   $??328.354??6=   $??Arial Rounded MT Bold)?6M   $??16(?6p   .??5      ? $??0,0,2,4q?6   .+?5   
   ?? $??Connect to Chatroom??6   $??56,23,59,216q?6.    J? RenderTransformOrigin$ -0.334,-0.276&?6D   $	??185??6j   $??Center=?6v   ?? 5   
   $??IP??6   $
??Left=?6   $??	21,76,0,0q?68   $	??Top=?6K   $??39??6c   $??28??6n   ?? 5   
   $
??Port??6   $
??Left=?6   $??
21,134,0,0q?6:   $	??Top=?6N   $ 0.321,0.495&?6f   ?? 5   
   $
??Left=?6+   $??28??6F   $??	77,80,0,0q?6R    ?? TextWrapping$
 Wrap=?6e   $	??Top=?6y   $	??187??6?   ??6   ?? *	InputIP+6?   ?? 5   
   $
??Left=?6-   $??28??6H   $??
77,135,0,0q?6T   $
 Wrap=?6h   $	??Top=?6|   $	??187??6?   ??6   ?? *	InputPort+6?   ?? 5   
   $??Connect to new chat room??6p   $
??Left=?6?   $??10q?6?   $??
46,276,0,0q?6?   $	??Top=?6?   $	??228??6?   ??6   ?? *AddServerEventCmd+??67   ??  ?? ElementName$ AddDialogWindow??6?   ?? 5   
   -   5         ??Name$ 
error_text??5      $"??This chat room is not exist.??6%   $
??Left=?6L   $??
21,229,0,0q?6g   $	??Top=?6{   $	??253??6?   
??????????	#FFDC0000y?6?   ?? *PromptVisibility+?? 5   
   $
??Name??6   $
??Left=?6   $??
21,180,0,0q?6:   $	??Top=?6N   $ 0.321,0.495&?6f   ?? 5   
   $
??Left=?6-   $??28??6H   $??
76,181,0,0q?6T   $
 Wrap=?6h   $	??Top=?6|   $	??187??6?   ??6   ?? *	InputName+6?   5      5      