import * as baseService from './baseService';

export const login = async (username, password) => {
    let fdata = new FormData();
    fdata.append('username', username);
    fdata.append('password', password);
    return await baseService.createFormData('auths/login', fdata);
};
export const refreshToken = async (accessToken, refreshToken) => {
    return await baseService.createJsonData('auths/refresh-token', {accessToken, refreshToken});
};
export const revokeToken = async (userId) => {
    return await baseService.createJsonData(`auths/revoke-token/${userId}`, {});
};
export const register = async (data) => {
    return await baseService.createFormData('auths/register', data);
};
