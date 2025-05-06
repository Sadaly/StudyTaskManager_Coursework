import React, { FC } from 'react';
import { UserResponse } from '../TypesFromTheServer/UserResponse';
import UserCard from './Cards/UserCard';

interface UserListProps {
    users: UserResponse[]
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