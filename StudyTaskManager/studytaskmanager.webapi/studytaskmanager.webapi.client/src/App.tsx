import { BrowserRouter, Routes, Route } from "react-router-dom";
import WelcomePage from "./Pages/WelcomePage";
import LoginPage from "./Pages/LoginPage";
import RegisterPage from "./Pages/RegisterPage";
import HomePage from "./Pages/HomePage";
import ProtectedRoute from "./Components/ProtectedRoute";
import HomeLayout from "./Pages/HomeLayout ";
import PersonalChatsPage from "./Pages/PersonalChatsPage";

function App() {
    return (
        <BrowserRouter>
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
                    <Route path="/home" element={<HomeLayout />}>
                        <Route index element={<HomePage />} />
                        <Route path="PersonalChats" element={<PersonalChatsPage />} />
                    </Route>
                </Route>


            </Routes>
        </BrowserRouter>
    );

}
export default App;