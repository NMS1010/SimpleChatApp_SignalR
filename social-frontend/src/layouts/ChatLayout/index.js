import { useEffect, useState } from 'react';
import Footer from './Footer';
import Header from './Header';
import Sidebar from './Sidebar';
import * as chatService from '../../services/chatService';
import Loading from '../../components/Loading';
import Chat from '../../pages/client/Chat'
const ChatLayout = ({ children }) => {
    const [chats, setChats] = useState([]);
    const [loading, setLoading] = useState(false);
    const [chosenChat, setChosenChat] = useState(-1);
    const [loadChat, setLoadChat] = useState(false);
    useEffect(() => {
        const fetch = async () => {
            setLoading(true);
            const resp = await chatService.getChats({ PageIndex: 1, PageSize: 10 });
            if (!resp?.isSuccess) {
                setLoading(true);
            } else {
                setLoading(false);
                setChats(resp.data.items);
            }
        };
        fetch();
    }, [loadChat]);
    const onChatClick = async (user) => {
        const resp = await chatService.createPrivateChat(`${user?.firstName} ${user?.lastName}`, user.userId);
        if(!resp?.isSuccess){

        }else{
            setLoadChat(true)
        }
    }
    return loading ? (
        <Loading />
    ) : (
        <div className="flex flex-col" style={{ backgroundColor: '#f5f5f5' }}>
            <div className="flex flex-row justify-between bg-white">
                <Sidebar onChatClick={onChatClick} setChosenChat={setChosenChat} chats={chats} />
                <div className="relative flex-grow">
                    {chosenChat == -1 ? (
                        <div className="flex-1 justify-between flex flex-col h-screen">empty</div>
                    ) : (
                        <Chat chat={chats.find(c => c.chatId === chosenChat)}/>
                    )}
                </div>
            </div>
        </div>
    );
};

export default ChatLayout;
