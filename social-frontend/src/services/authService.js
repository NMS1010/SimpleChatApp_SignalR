import * as baseService from './baseService';

export const login = async (username, password) => {
    let formData = new FormData();
    formData.append('username', username);
    formData.append('password', password);
    return await baseService.createFormData('users/login', formData);
};
export const refreshToken = async (accessToken, refreshToken) => {
    let formData = new FormData();
    formData.append('accessToken', accessToken);
    formData.append('refreshToken', refreshToken);
    return await baseService.createFormData('users/refresh-token', formData);
};
export const revokeToken = async (userId) => {
    return await baseService.createData(`users/revoke-token/${userId}`, {});
};
export const register = async (formData) => {
    return await baseService.createFormData('users/register', formData);
};
