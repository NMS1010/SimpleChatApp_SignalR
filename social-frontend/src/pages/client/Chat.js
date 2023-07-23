import { useEffect, useRef, useState } from 'react';
import Message from './components/Message';
import * as messageService from '../../services/messageService';
import * as signalRUtil from '../../utils/signalRUtils';
import * as authUtils from '../../utils/authUtils';
import { useSelector } from 'react-redux';
import AudioRecord from './components/Microphone';
import Audio from './components/AudioRecord';
import ChatForm from './components/ChatForm';
const Chat = ({ chat }) => {
    const [audio, setAudio] = useState(null);
    const [connection, setConnection] = useState(null);
    const [loading, setLoading] = useState(false);
    const [messages, setMessages] = useState([]);
    const [text, setText] = useState('');
    const divRef = useRef(null);
    let { currentUser } = useSelector((state) => state?.auth);

    useEffect(() => {
        if (divRef.current) divRef.current.scrollIntoView({ behavior: 'smooth' });
    });
    const onChange = (e) => {
        setText(e.target.value);
    };

    const onSend = async (cancleRecord) => {
        let file = null;
        if (audio) {
            let audioBlob = await fetch(audio).then((r) => r.blob());
            file = new File([audioBlob], 'audiofile.mp3', { type: 'audio/mp3', lastModified: new Date().getTime() });
        }
        if (file) {
            await messageService.createMessage({
                text: text,
                chatId: chat.chatId,
                roomId: chat.roomId,
                status: 1,
                file: file,
            });
        } else {
            await connection.invoke('SendMessage', {
                text: text,
                chatId: chat.chatId,
                roomId: chat.roomId,
                status: 1,
            });
        }
        setText('');
        cancleRecord();
    };
    useEffect(() => {
        const fetchChat = async () => {
            setLoading(true);
            const resp = await messageService.getMessages({ chatId: chat?.chatId, pageIndex: 1, pageSize: 100 });
            setLoading(false);
            if (!resp?.isSuccess) {
            } else {
                setMessages(resp.data.items);
            }
        };
        fetchChat();
    }, [chat]);
    useEffect(() => {
        (async () => {
            setLoading(true);
            const connection = await signalRUtil.getSignalRConnection();
            connection.on('ReceiveMessage', (user, message) => {
                message.isMe = user === currentUser.userId;
                setMessages((messages) => [...messages, message]);
                setText('');
            });
            await connection.invoke('JoinRoom', chat.roomId);
            setLoading(false);
            setConnection(connection);
        })();
    }, []);
    return (
        <div class="flex-1 justify-between flex flex-col h-screen">
            <div class="flex sm:items-center justify-between p-3 border-b-2 border-gray-200">
                <div class="relative flex items-center space-x-4">
                    <div class="relative">
                        <span class="absolute text-green-500 right-0 bottom-0">
                            <svg width="20" height="20">
                                <circle cx="8" cy="8" r="8" fill="currentColor"></circle>
                            </svg>
                        </span>
                        <img src={chat?.avatar} alt="" class="w-10 sm:w-16 h-10 sm:h-16 rounded-full" />
                    </div>
                    <div class="flex flex-col leading-tight">
                        <div class="text-xl mt-1 flex items-center">
                            <span class="text-gray-700 mr-3">{chat?.name}</span>
                        </div>
                    </div>
                </div>
                <div class="flex items-center space-x-2">
                    <button
                        type="button"
                        class="inline-flex items-center justify-center rounded-lg border h-10 w-10 transition duration-500 ease-in-out text-gray-500 hover:bg-gray-300 focus:outline-none"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            stroke="currentColor"
                            class="h-6 w-6"
                        >
                            <path
                                stroke-linecap="round"
                                stroke-linejoin="round"
                                stroke-width="2"
                                d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"
                            ></path>
                        </svg>
                    </button>
                    <button
                        type="button"
                        class="inline-flex items-center justify-center rounded-lg border h-10 w-10 transition duration-500 ease-in-out text-gray-500 hover:bg-gray-300 focus:outline-none"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            stroke="currentColor"
                            class="h-6 w-6"
                        >
                            <path
                                stroke-linecap="round"
                                stroke-linejoin="round"
                                stroke-width="2"
                                d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z"
                            ></path>
                        </svg>
                    </button>
                    <button
                        type="button"
                        class="inline-flex items-center justify-center rounded-lg border h-10 w-10 transition duration-500 ease-in-out text-gray-500 hover:bg-gray-300 focus:outline-none"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            stroke="currentColor"
                            class="h-6 w-6"
                        >
                            <path
                                stroke-linecap="round"
                                stroke-linejoin="round"
                                stroke-width="2"
                                d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9"
                            ></path>
                        </svg>
                    </button>
                </div>
            </div>
            {loading ? (
                <div class="flex justify-center space-x-2 animate-pulse">
                    <div class="w-3 h-3 bg-gray-500 rounded-full"></div>
                    <div class="w-3 h-3 bg-gray-500 rounded-full"></div>
                    <div class="w-3 h-3 bg-gray-500 rounded-full"></div>
                </div>
            ) : (
                <div
                    id="messages"
                    class="h-full justify-end space-y-4 p-3 overflow-y-auto scrollbar-thumb-blue scrollbar-thumb-rounded scrollbar-track-blue-lighter scrollbar-w-2 scrolling-touch"
                >
                    {messages.map((m) => {
                        return <Message message={m} key={m.messageId} />;
                    })}
                    <div ref={divRef} />
                </div>
            )}
            <ChatForm audio={audio} setAudio={setAudio} text={text} onChange={onChange} onSend={onSend} />
        </div>
    );
};

export default Chat;
