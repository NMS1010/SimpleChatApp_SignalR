import { HttpTransportType, HubConnectionBuilder, HubConnectionState, JsonHubProtocol, LogLevel } from '@microsoft/signalr';
import * as authUtils from './authUtils';

const isDev = process.env.NODE_ENV === 'development';

const startSignalRConnection = async (connection) => {
    try {
      await connection.start();
      console.assert(connection.state === HubConnectionState.Connected);
      console.log('SignalR connection established', connection.baseUrl);
    } catch (err) {
      console.assert(connection.state === HubConnectionState.Disconnected);
      console.error('SignalR Connection Error: ', err);
    //   setTimeout(() => startSignalRConnection(connection), 5000);
    }
  };

export const getSignalRConnection = async () => {
    const options = {
        logMessageContent: isDev,
        logger: isDev ? LogLevel.Warning : LogLevel.Error,
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
        accessTokenFactory: () => authUtils.getToken(),
    };
    var connection = new HubConnectionBuilder()
        .withUrl(process.env.REACT_APP_SIGNALR, options)
        .withAutomaticReconnect()
        .withHubProtocol(new JsonHubProtocol())
        .configureLogging(LogLevel.Information)
        .build();

    connection.onclose((error) => {
        if (error) {
            console.log('SignalR: connection was closed due to error.', error);
        } else {
            console.log('SignalR: connection was closed.');
        }
    });
    connection.onreconnecting((error) => {
        console.assert(connection.state === HubConnectionState.Reconnecting);
    });
    connection.onreconnected((connectionId) => {
        console.assert(connection.state === HubConnectionState.Connected);
    });
    await startSignalRConnection(connection);

    return connection;
};
