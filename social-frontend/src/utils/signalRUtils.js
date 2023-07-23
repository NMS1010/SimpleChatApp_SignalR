import {
    HttpTransportType,
    HubConnectionBuilder,
    HubConnectionState,
    JsonHubProtocol,
    LogLevel,
} from '@microsoft/signalr';
import * as authUtils from './authUtils';
import * as authServices from '../services/authService';

const isDev = process.env.NODE_ENV === 'development';

const startSignalRConnection = async (connection) => {
    try {
        await connection.start();
        console.assert(connection.state === HubConnectionState.Connected);
        console.log('SignalR connection established', connection.baseUrl);
    } catch (err) {
        await refreshTokenSignalR();
        console.assert(connection.state === HubConnectionState.Disconnected);
        console.error('SignalR Connection Error: ', err);
        setTimeout(() => startSignalRConnection(connection), 5000);
    }
};
const refreshTokenSignalR = async () => {
    if (authUtils.isTokenExpired()) {
        let accessToken = localStorage.getItem('accessToken');
        let refreshToken = localStorage.getItem('refreshToken');
        console.log('refresh signalR')
        let resp = await authServices.refreshToken(accessToken, refreshToken);
        if (resp && resp.isSuccess) {
            localStorage.setItem('accessToken', resp.accessToken);
            localStorage.setItem('refreshToken', resp.refreshToken);

            await startSignalRConnection(buildConnection());
        }
    }
};
const buildConnection = () => {
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
    return connection;
};
export const getSignalRConnection = async () => {
    let connection = buildConnection();

    connection.onclose(async (error) => {
        await refreshTokenSignalR();
        if (error) {
            console.log('SignalR: connection was closed due to error.', error);
        } else {
            console.log('SignalR: connection was closed.');
        }
    });
    connection.onreconnecting(async (error) => {
        await refreshTokenSignalR();
        console.log(error);
    });
    connection.onreconnected((connectionId) => {
        console.log(connectionId);
    });
    await startSignalRConnection(connection);

    return connection;
};
