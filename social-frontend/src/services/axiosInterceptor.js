import axios from 'axios';
import * as authService from './authService';
import * as authUtil from '../utils/authUtils';
const axiosClient = (navigate) => {
    axios.defaults.baseURL = process.env.REACT_APP_API_ENDPOINT;
    axios.interceptors.request.use(onRequestSuccess);
    axios.interceptors.response.use(onResponseSuccess, async (error) => {
        const { response, config } = error;
        const status = response?.status;
        if (status !== 401 && status !== 403 ) {
            return Promise.reject(error);
        }
        return await refreshToken(error, navigate);
    });
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
}
const handleApiUnauthoried = (navigate) => {
    navigate('/auth/login')
};
const refreshToken = async (error, navigate) => {
    try {
        const { config } = error;
        const response = await authService.refreshToken(localStorage.getItem('accessToken'), 
                            localStorage.getItem('refreshToken'));
        if (!response?.data) {
            authUtil.clearToken();
            handleApiUnauthoried(navigate);
            return Promise.reject(error);
        }
        let {accessToken, refreshToken} = response.data;
        localStorage.setItem('accessToken', accessToken);
        localStorage.setItem('refreshToken', refreshToken);
        return new Promise((resolve, reject) => {
              if (accessToken) {
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