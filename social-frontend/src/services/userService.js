import * as baseService from './baseService'

export const getProfile = async () => {
    return await baseService.getData('/users/profile')
}
export const findUser = async (data) => {
    return await baseService.getData('/users/search', data)
}