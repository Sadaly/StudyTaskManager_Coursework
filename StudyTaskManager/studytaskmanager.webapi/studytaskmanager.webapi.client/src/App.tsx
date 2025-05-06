import { useEffect, useState } from "react";
import UsersList from "./Components/UsersList";
import { UserResponse } from "./TypesFromTheServer/UserResponse";
import axios from "axios";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import HomePage from "./Pages/HomePage";
import LoginPage from "./Pages/LoginPage";
import RegisterPage from "./Pages/RegisterPage";
import WelcomePage from "./Pages/WelcomePage";

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
                <Route path="/home" element={<HomePage />} />
            </Routes>
        </BrowserRouter>
    );

}
export default App;