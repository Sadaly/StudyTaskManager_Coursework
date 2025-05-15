import React, { FC } from 'react';
import { User } from '../TypesFromTheServer/User';
import UserCard from './Cards/UserCard';

interface UserListProps {
    users: User[]
}

const UserList: FC<UserListProps> = ({ users }) => {
    return (
        <div>
            <h1>Список пользователей</h1>
            {users.map(user => <UserCard user={user} />)}
        </div>
    )
}

export default UserList;