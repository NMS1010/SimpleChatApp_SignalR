import { createAsyncThunk } from '@reduxjs/toolkit';
import * as authService from '../../../services/authService';
import * as userService from '../../../services/userService';
import * as authUtils from '../../../utils/authUtils';

export const login = createAsyncThunk('auth/login', async (data, thunkAPI) => {
    let response = await authService.login(data.username, data.password);
    if (!response || !response?.isSuccess) {
        authUtils.clearToken();
    } else {
        let { accessToken, refreshToken } = response.data;
        localStorage.setItem('accessToken', accessToken);
        localStorage.setItem('refreshToken', refreshToken);
        response = await userService.getProfile();
        if (!response || !response?.isSuccess) {
            authUtils.clearToken();
        }
    }
    return response;
});
export const logout = createAsyncThunk('auth/logout', async (_, thunkAPI) => {
    let userId = authUtils.getUserId();
    const response = await authService.revokeToken(userId);
    authUtils.clearToken();
    return response;
});
export const getCurrentUser = createAsyncThunk('auth/getCurrentUser', async (_, thunkAPI) => {
    const response = await userService.getProfile();
    if (!response?.isSuccess) {
        if (response?.statusCode !== 401) {
            authUtils.clearToken();
        }
    }
    return response;
});
