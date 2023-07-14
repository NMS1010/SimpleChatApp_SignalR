import {
    Login, Home, Register, Chat
} from '../pages/client';
import {  ChatLayout } from '../layouts';
import config from '../configs/index';
//not login
const publicRoutes = [
    { path: config.routes.login, component: Login, layout: null, private: false, roles: [] },
    { path: config.routes.register, component: Register, layout: null, private: false, roles: [] },
];

//must login
const privateRoutes = [
    { path: config.routes.home, component: Home, layout: ChatLayout, private: true, roles: ['customer'] },
    { path: config.routes.chat, component: Chat, layout: ChatLayout, private: true, roles: ['customer'] },
];

const routes = [...publicRoutes, ...privateRoutes];
export default routes;
