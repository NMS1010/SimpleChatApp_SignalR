import * as baseService from './baseService'

export const getChats = async (paging) => {
    return await baseService.getData('/chats/get-chats', paging)
}

export const getChat = async (chatId) => {
    return await baseService.getData(`/chats/get-chat/${chatId}`)
}

export const createPrivateChat = async (name, userId) => {
    const formData = new FormData();
    formData.append('Name', name);
    formData.append('UserId', userId);
    return await baseService.createFormData('/chats/create/private', formData)
}