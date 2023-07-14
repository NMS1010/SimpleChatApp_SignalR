const Message = ({isMe, message, avatar}) => {
    return (
        <div class="chat-message">
            <div class={`flex items-end ${isMe && 'justify-end'}`}>
                <div class="flex flex-col space-y-2 text-xs max-w-xs mx-2 order-2 items-start">
                    <div>
                        <span class={`px-4 py-2 rounded-lg inline-block  ${isMe ? 'bg-blue-600 text-white rounded-br-none' : 'bg-gray-300 text-gray-600 rounded-bl-none'} `}>
                            {message}
                        </span>
                    </div>
                </div>
                <img
                    src={avatar}
                    alt="My profile"
                    class={`w-6 h-6 rounded-full ${isMe ? 'order-2' :'order-1'}`}
                />
            </div>
        </div>
    );
};

export default Message;
