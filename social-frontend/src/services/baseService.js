import axios from "axios";

export const getData = async (url, params = {}) => {
    try {
        const response = await axios.get(url, {
            params,
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                'accept': 'application/json' 
            }
        });
        return response.data;
    } catch (error) {
        return error?.response;
    }
};

export const deleteData = async (url) => {
    try {
        const response = await axios.delete(url);
        return response.data;
    } catch (error) {
        return error?.response;
    }
};
export const createFormData = async (url, formData) => {
    try {
        let response = await axios.postForm(url, formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });
        return response.data;
    } catch (error) {
        return error?.response;
    }
};
export const createJsonData = async (url, obj) => {
    try {
        let response = await axios.postForm(
            url,
            { ...obj },
            {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            },
        );
        return response.data;
    } catch (error) {
        return error?.response;
    }
};
export const updateFormData = async (url, formData) => {
    try {
        let response = await axios.putForm(url, formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });
        return response.data;
    } catch (error) {
        return error?.response;
    }
};
export const updateJsonData = async (url, obj) => {
    try {
        let response = await axios.putForm(
            url,
            { ...obj },
            {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            },
        );
        return response.data;
    } catch (error) {
        return error?.response;
    }
};
