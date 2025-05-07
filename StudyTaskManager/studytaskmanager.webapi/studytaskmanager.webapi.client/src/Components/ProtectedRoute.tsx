import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';

const isAuthenticated = (): boolean => {
    // Здесь ты можешь использовать локальное хранилище, контекст или запрос к API
    // Пример: проверка наличия куки (если JWT хранится в HttpOnly, надо запрашивать сервер)
    const token = document.cookie.includes('access_token');
    return token;
};

const ProtectedRoute: React.FC = () => {
    return isAuthenticated() ? <Outlet /> : <Navigate to="/login" />;
};

export default ProtectedRoute;
