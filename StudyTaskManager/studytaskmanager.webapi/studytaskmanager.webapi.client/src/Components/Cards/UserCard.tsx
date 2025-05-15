import React from 'react';
import { User } from '../../TypesFromTheServer/User';
import { CSSProperties } from 'react';

interface UserCardProps {
    user: User;
}

const UserCard: React.FC<UserCardProps> = ({ user }) => {

    const cardStyle: CSSProperties = {
        border: '1px solid #ccc',
        borderRadius: '8px',
        padding: '16px',
        margin: '8px',
        boxShadow: '0 2px 4px rgba(0,0,0,0.1)',
        textAlign: 'left',
    };

    // Форматирование даты регистрации
    const formatDate = (dateString: string) => {
        const date = new Date(dateString);
        return date.toLocaleDateString('ru-RU', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
    };

    return (
        <div className="user-card" style={cardStyle}>
            <h3>{user.username}</h3>
            <p><strong>ID пользователя:</strong> {user.userId}</p>
            <p><strong>Электронная почта:</strong> {user.email}</p>
            <p><strong>Почта подтверждена:</strong> {user.isEmailVerifed ? 'Да' : 'Нет'}</p>
            <p><strong>Телефон:</strong> {user.phoneNumber || 'Не указан'}</p>
            <p><strong>Телефон подтверждён:</strong> {user.phoneNumber ? (user.isPhoneNumberVerifed ? 'Да' : 'Нет') : 'Не применимо'}</p>
            <p><strong>Дата регистрации:</strong> {formatDate(user.registrationDate)}</p>
            <p><strong>Роль в системе:</strong> {user.systemRoleId || 'Не назначена'}</p>
        </div>
    );
};

export default UserCard;