const client = {
    home: '/',
    login: '/auth/login',
    register: '/auth/register',
    forbidden: '/forbidden',
    profile: '/user/profile',
    verify: '/register-confirm',
    forgot_password: '/forgot-password',
    reset_password: '/reset-password',
    chat: '/chat'
};
const admin = {
    admin_home: '/admin/',
    admin_users: '/admin/users',
    admin_roles: '/admin/roles',
    admin_profile: '/admin/profile',
};
const routes = {
    ...client,
    ...admin,
};
export default routes;
