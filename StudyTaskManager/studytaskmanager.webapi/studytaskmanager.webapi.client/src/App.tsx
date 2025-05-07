import { useEffect, useState } from "react";
import UsersList from "./Components/UsersList";
import { UserResponse } from "./TypesFromTheServer/UserResponse";
import axios from "axios";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import HomePage from "./Pages/HomePage";
import LoginPage from "./Pages/LoginPage";
import RegisterPage from "./Pages/RegisterPage";
import WelcomePage from "./Pages/WelcomePage";
import ProtectedRoute from './components/ProtectedRoute';
import RoleProtectedRoute from './components/RoleProtectedRoute';

function App() {
    const [users, setUsers] = useState<UserResponse[]>([])

    useEffect(() => {
        fetchUsers()
    }, [])

    async function fetchUsers() {
        try {
            const response = await axios.get<UserResponse[]>('https://localhost:7241/api/Users/All')
            setUsers(response.data)
        } catch (e) {
            alert(e)
        }
    }

    return (
        <BrowserRouter>
            {/*<UsersList users={users} />*/}
            <Routes>
                <Route path="/" element={<WelcomePage />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<RegisterPage />} />

                {/* Страница только для админа */}
                {/*<Route element={<RoleProtectedRoute allowedRoles={['Admin']} />}>*/}
                {/*    <Route path="/admin" element={<AdminPage />} />*/}
                {/*</Route>*/}

                {/* Страница для всех авторизованных */}
                <Route element={<ProtectedRoute />}>
                    <Route path="/home" element={<HomePage />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );

}
export default App;