const UserChat = ({setChosenChat, chat}) => {
    const onChoosen = () => {
        setChosenChat(chat?.chatId)
    }
    return (
        <div onClick={onChoosen} class="flex flex-row py-4 px-2 justify-center items-center border-b-2 hover:bg-slate-300 hover:cursor-pointer transition-all ">
            <div class="w-1/4">
                <img
                    src={chat?.avatar}
                    class="object-cover h-12 w-12 rounded-full"
                    alt=""
                />
            </div>
            <div class="w-full text-left">
                <div class="text-lg font-semibold">{chat?.name}</div>
                <span class="text-gray-500">{chat?.lastMessage}</span>
            </div>
        </div>
    );
};

export default UserChat;
