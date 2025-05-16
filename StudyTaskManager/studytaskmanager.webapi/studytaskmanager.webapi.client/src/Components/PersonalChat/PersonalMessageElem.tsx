import React from 'react';
import { PersonalMessage } from '../../TypesFromTheServer/PersonalMessage';

interface PersonalMessageElemProps {
    message: PersonalMessage;
    senderMy: Boolean;
}

const PersonalMessageElem: React.FC<PersonalMessageElemProps> = ({ message, senderMy }) => {
    // Стили для контейнера сообщения
    const bubbleStyle: React.CSSProperties = {
        position: 'relative', // Для абсолютного позиционирования времени
        textAlign: senderMy ? 'right' : 'left',
        paddingRight: '70px' // Оставляем место для времени справа
    };

    // Стили для времени
    const timeStyle: React.CSSProperties = {
        opacity: 0.7,
        fontSize: '0.8em',
        position: 'absolute',
        right: '0',
        top: '0',
        whiteSpace: 'nowrap' // Чтобы время не переносилось
    };

    return (
        <div style={bubbleStyle}>
            <p>
                {message.content}
                <span style={timeStyle}>
                    {senderMy && message.is_Read_By_Other_User ?
                        ' == ' :
                        ' - '
                    }
                    {new Date(message.dateWriten).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}
                </span>
            </p>
        </div>
    );
};

export default PersonalMessageElem;