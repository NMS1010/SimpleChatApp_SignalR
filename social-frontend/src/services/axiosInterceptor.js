import axios from 'axios';
import * as authService from './authService';
import * as authUtil from '../utils/authUtils';

const axiosClient = () => {
    const axiosClient = axios.create({
        baseURL: process.env.REACT_APP_API_ENDPOINT,
    });
    axiosClient.interceptors.request.use(onRequestSuccess);
    axiosClient.interceptors.response.use(onResponseSuccess, onResponseError);
    return axiosClient;
};
const onRequestSuccess = (config) => {
    const token = localStorage.getItem('accessToken');
    if (token) {
        config.headers = {
            Authorization: 'Bearer ' + token,
        };
    }

    return config;
};
const onResponseSuccess = (response) => {
    return response;
};
const onResponseError = async (error) => {
    const { response, config } = error;
    const status = response?.status;
    if (status !== 401 && status !== 403 ) {
        return Promise.reject(error);
    }
    return await refreshToken(error);
};
const refreshToken = async (error) => {
    try {
        const response = await authService.refreshToken(localStorage.getItem('accessToken'), 
                            localStorage.getItem('refreshToken'));
        if (!response?.data) {
            authUtil.clearToken();
            return Promise.reject(error);
        }
        let {accessToken, refreshToken} = response.data;
        localStorage.setItem('accessToken', accessToken);
        localStorage.setItem('refreshToken', refreshToken);
        return new Promise((resolve, reject) => {
              if (token) {
                config.headers.Authorization = `Bearer ${accessToken}`;
                resolve(axios(config));
              }
              reject(error);
          });
    } catch (error) {
        return Promise.reject(error);
    }
};
export default axiosClient;