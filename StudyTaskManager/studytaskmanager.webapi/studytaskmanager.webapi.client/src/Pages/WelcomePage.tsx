import { Link } from "react-router-dom";

export default function WelcomePage() {
    return (
        <div style={{ backgroundColor: "#1a1a2e" }}>
            <h1>Добро пожаловать!</h1>
            <p>Выберите действие:</p>
            <div>
                <Link to="/login"> Вход </Link>
                <Link to="/register"> Регистрация </Link>
            </div>
        </div>
    );
}