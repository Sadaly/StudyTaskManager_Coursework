import React from 'react';
import { UserResponse } from '../../TypesFromTheServer/UserResponse';
import { CSSProperties } from 'react'; 

interface UserCardProps {
    user: UserResponse;
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

    return (
        <div className="user-card" style={cardStyle}>
            <h3>{user.username}</h3>
            <p>Email: {user.email}</p>
            <p>Email verified: {user.isEmailVerifed ? 'Yes' : 'No'}</p>
            <p>Phone: {user.phoneNumber}</p>
            <p>Phone verified: {user.isPhoneNumberVerifed ? 'Yes' : 'No'}</p>
            <p>Registered: {user.registrationDate}</p>
        </div>
    );
};

export default UserCard;