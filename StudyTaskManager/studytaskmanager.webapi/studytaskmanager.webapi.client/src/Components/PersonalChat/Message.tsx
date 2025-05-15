import React from 'react';
import { PersonalMessage } from '../../TypesFromTheServer/PersonalMessage';

interface MessageBubbleProps {
    message: PersonalMessage;
    align: 'left' | 'right';
}

const Message: React.FC<MessageBubbleProps> = ({ message, align }) => {
    // Стили для контейнера сообщения
    const bubbleStyle: React.CSSProperties = {
        textAlign: align === 'right' ? 'right' : 'left'
    };

    // Стили для времени
    const timeStyle: React.CSSProperties = {
        opacity: 0.7,
        fontSize: '0.8em',
        textAlign: align === 'right' ? 'right' : 'left',
        display: 'block',
        marginTop: '4px',
    };

    return (
        <div style={bubbleStyle}>
            <p>{message.content}</p>
            <div style={timeStyle}>
                {new Date(message.dateWriten).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}
                {message.is_Read_By_Other_User && align === 'right' && ' =='}
                {!message.is_Read_By_Other_User && align === 'right' && ' -'}
            </div>
        </div>
    );
};

export default Message;