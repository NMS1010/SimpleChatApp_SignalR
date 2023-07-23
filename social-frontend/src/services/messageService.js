import * as baseService from './baseService'
import * as convertUtils from '../utils/convertUtils'
export const getMessages = async (paging) => {
    return await baseService.getData(`/messages/get`, paging)
}
export const createMessage = async (data) => {
    return await baseService.createFormData(`/messages/create`, convertUtils.objectToFormData(data))
}
export const createPrivateChat = async (name, userId) => {
    const formData = new FormData();
    formData.append('Name', name);
    formData.append('UserId', userId);
    return await baseService.createFormData('/chats/create/private', formData)
}