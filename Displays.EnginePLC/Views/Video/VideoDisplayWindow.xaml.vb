Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Threading

Class VideoDisplayWindow

    Private Sub Window_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Top = -500
        Left = 0
        WindowState = Windows.WindowState.Maximized
        VideoDisplayViewModel.Instance.MediaPlayer = MediaUriElement1
    End Sub

End Class
