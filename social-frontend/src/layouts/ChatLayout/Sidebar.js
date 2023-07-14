
import UserChat from '../../pages/client/components/UserChat';
import SearchBar from './components/SearchBar';

const Sidebar = ({ chats, setChosenChat, onChatClick }) => {
    return (
        <div class="flex flex-col w-1/5 border-r-2">
            <SearchBar onChatClick={onChatClick}/>
            {chats.map((c) => {
                return (
                    <UserChat
                        key={c.chatId}
                        chat={c}
                        setChosenChat={setChosenChat}
                    />
                );
            })}
        </div>
    );
};

export default Sidebar;
