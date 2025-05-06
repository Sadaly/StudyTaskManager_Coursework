import { Link } from "react-router-dom";

export default function RegisterPage() {
    return (
        <div style={{ backgroundColor: "#2d0a0a" }}>
            <h2>Регистрация</h2>
            <form style={{ display: "flex", flexDirection: "column", width: "300px", gap: "10px" }}>
                <input
                    type="text"
                    placeholder="Имя"
                    style={{ padding: "10px", borderRadius: "5px", border: "none" }}
                />
                <input
                    type="email"
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
                        backgroundColor: "#f44336",
                        color: "white",
                        border: "none",
                        borderRadius: "5px",
                        cursor: "pointer"
                    }}
                >
                    Зарегистрироваться
                </button>
            </form>
            <p style={{ marginTop: "20px" }}>
                Уже есть аккаунт? <Link to="/login" style={{ color: "#f44336" }}>Вход</Link>
            </p>
        </div>
    );
}
