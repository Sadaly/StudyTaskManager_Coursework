import React, { useEffect, useState } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import axios from 'axios';

interface RoleProtectedRouteProps {
    allowedRoles: string[];
}

const RoleProtectedRoute: React.FC<RoleProtectedRouteProps> = ({ allowedRoles }) => {
    const [isAllowed, setIsAllowed] = useState<boolean | null>(null);

    useEffect(() => {
        const checkRole = async () => {
            try {
                const res = await axios.get('https://localhost:7241/api/Users/me', {
                    withCredentials: true,
                });

                const userRole = res.data.role;
                setIsAllowed(allowedRoles.includes(userRole));
            } catch {
                setIsAllowed(false);
            }
        };

        checkRole();
    }, [allowedRoles]);

    if (isAllowed === null) return <div>Загрузка...</div>;

    return isAllowed ? <Outlet /> : <Navigate to="/unauthorized" />;
};

export default RoleProtectedRoute;
