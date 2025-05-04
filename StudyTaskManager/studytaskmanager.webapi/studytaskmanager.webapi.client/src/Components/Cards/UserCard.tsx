import React from 'react';
import { UserResponse } from '../../TypesFromTheServer/UserResponse';

interface UserCardProps {
    user: UserResponse;
}

const UserCard: React.FC<UserCardProps> = ({ user }) => {
    return (
        <div className="user-card">
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