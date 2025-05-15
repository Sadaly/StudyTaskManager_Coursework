import React from "react";
import axios from "axios";

const LogoutButton: React.FC = () => {
    const handleLogout = async () => {
        try {
            await axios.post("https://localhost:7241/api/Users/Logout", {
                withCredentials: true,
            });

            window.location.href = "/login";
        } catch (error) {
            console.error("Ошибка при выходе", error);
        }
    };

    return (
        <button onClick={handleLogout} style={{ marginTop: "1rem", padding: "0.5rem 1rem" }}>
            Выйти
        </button>
    );
};

export default LogoutButton;
