import * as baseService from './baseService'

export const getMessages = async (paging) => {
    return await baseService.getData(`/messages/get`, paging)
}

export const createPrivateChat = async (name, userId) => {
    const formData = new FormData();
    formData.append('Name', name);
    formData.append('UserId', userId);
    return await baseService.createFormData('/chats/create/private', formData)
}