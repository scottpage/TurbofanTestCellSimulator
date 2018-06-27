Public Delegate Sub ConfigurationChangedEventHandler(sender As Object, e As ConfigurationChangedEventArgs)

Public Interface INotifyConfigurationChanged

    Event ConfigurationChanged As ConfigurationChangedEventHandler

End Interface
