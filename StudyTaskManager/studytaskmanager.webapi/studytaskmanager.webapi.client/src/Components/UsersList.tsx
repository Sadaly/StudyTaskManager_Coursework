import React, { FC } from 'react';
import { UserResponse } from '../TypesFromTheServer/UserResponse';
import UserCard from './Cards/UserCard';

interface UserListProps {
    users: UserResponse[]
}

const UserList: FC<UserListProps> = ({ users }) => {
    return (
        <div>
            {users.map(user => <UserCard user={user} />)}
        </div>
    )
}

export default UserList;