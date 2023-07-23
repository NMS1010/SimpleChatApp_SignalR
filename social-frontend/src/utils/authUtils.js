import jwtDecode from 'jwt-decode';

export const getUserId = () => {
    let token = localStorage.getItem('accessToken');
    let jwtDecodeObj = jwtDecode(token);
    let nameIdentifier = Object.keys(jwtDecodeObj).find((val) => val.includes('nameidentifier'));
    let userId = jwtDecodeObj[nameIdentifier];
    return userId;
};
export const clearToken = () => {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
};
export const isTokenStoraged = () => {
    return localStorage.getItem('accessToken') && localStorage.getItem('refreshToken');
};

export const getToken = () => {
    return localStorage.getItem('accessToken');
};

export const isTokenExpired = () => {
    let token = localStorage.getItem('accessToken');
    let jwtDecodeObj = jwtDecode(token);
    let currentDate = new Date();
    if (jwtDecodeObj.exp * 1000 < currentDate.getTime()) {
        return true;
    }
    return false;
};
