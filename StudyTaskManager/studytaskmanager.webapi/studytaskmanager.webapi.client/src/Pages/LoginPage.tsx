import { Link, useNavigate } from "react-router-dom";

export default function LoginPage() {
    const navigate = useNavigate();

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault(); // Предотвращаем перезагрузку страницы
        navigate("/home"); // Переход на /home
    };

    return (
        <div style={{ backgroundColor: "#0a2e38" }}>
            <h2>Вход</h2>
            <form
                onSubmit={handleSubmit} // Обработчик отправки формы
                style={{
                    display: "flex",
                    flexDirection: "column",
                    width: "300px",
                    gap: "10px"
                }}
            >
                <input
                    type="text"
                    placeholder="Email"
                    style={{ padding: "10px", borderRadius: "5px", border: "none" }}
                />
                <input
                    type="password"
                    placeholder="Пароль"
                    style={{ padding: "10px", borderRadius: "5px", border: "none" }}
                />
                <button
                    type="submit"
                    style={{
                        padding: "10px",
                        backgroundColor: "#4CAF50",
                        color: "white",
                        border: "none",
                        borderRadius: "5px",
                        cursor: "pointer"
                    }}
                >
                    Войти
                </button>
            </form>
            <p style={{ marginTop: "20px" }}>
                Нет аккаунта? <Link to="/register" style={{ color: "#4CAF50" }}>Регистрация</Link>
            </p>
        </div>
    );
}