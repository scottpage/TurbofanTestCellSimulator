Public Class ServiceAnnouncementMonitor

    Public Event Online As EventHandler
    Public Event Offline As EventHandler

    Private WithEvents _AnnouncementHost As ServiceHost
    Private WithEvents _AnnouncementService As AnnouncementService
    Private _ImplementationContractType As Type

    Public Sub New(implementationContractType As Type)
        _ImplementationContractType = implementationContractType
    End Sub

    Public Property IsOnline As Boolean

    Public Sub WaitForAnnouncements()
        _AnnouncementService = New AnnouncementService
        _AnnouncementHost = New ServiceHost(_AnnouncementService)
        _AnnouncementHost.Description.Behaviors.Add(New ServiceDiscoveryBehavior)
        _AnnouncementHost.AddServiceEndpoint(New UdpAnnouncementEndpoint())
        _AnnouncementHost.BeginOpen(Nothing, Nothing)
    End Sub

    Public Sub StopWaitingForAnnouncements()
        If _AnnouncementHost IsNot Nothing Then
            _AnnouncementHost.Close()
            _AnnouncementHost = Nothing
            _AnnouncementService = Nothing
        End If
    End Sub

    Private Sub _AnnoncementService_OfflineAnnouncementReceived(sender As Object, e As System.ServiceModel.Discovery.AnnouncementEventArgs) Handles _AnnouncementService.OfflineAnnouncementReceived
        If e.EndpointDiscoveryMetadata.ContractTypeNames.FirstOrDefault(Function(c) c.Name.Equals(_ImplementationContractType.Name)) IsNot Nothing Then
            StopWaitingForAnnouncements()
            IsOnline = False
        End If
    End Sub

    Private Sub _AnnoncementService_OnlineAnnouncementReceived(sender As Object, e As System.ServiceModel.Discovery.AnnouncementEventArgs) Handles _AnnouncementService.OnlineAnnouncementReceived
        If e.EndpointDiscoveryMetadata.ContractTypeNames.FirstOrDefault(Function(c) c.Name.Equals(_ImplementationContractType.Name)) IsNot Nothing Then
            IsOnline = True
        End If
    End Sub

End Class
