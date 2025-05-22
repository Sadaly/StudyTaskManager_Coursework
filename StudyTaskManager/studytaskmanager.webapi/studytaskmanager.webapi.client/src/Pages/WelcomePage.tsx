import { Link } from "react-router-dom";

export default function WelcomePage() {
    return (
        <div className="welcome-container" style={{
            //backgroundColor: "#1a1a2e",
            marginLeft: "150px",  /* Отступ от левого края */
            width: "100%",  /* Ширина 100% от родителя */
            minHeight: "100vh",  /* На всю высоту экрана */
            padding: "2rem",    /* Внутренние отступы */
            boxSizing: "border-box" /* Учёт padding в ширине */
        }}>
            <h1>StudyTaskManager</h1>
            <p>
                Это полнофункциональное веб-приложение для <br />
                управления учебными задачами, ориентированно <br />
                на студентов, преподавателей и учебные группы<br />
            </p>
            <p style={{ marginTop: "10px" }}>
                Исходный код можно просмотреть на <a href="https://github.com/Sadaly/StudyTaskManager">GitHub</a>
            </p>
            <div className="welcome-links" style={{ marginTop: "10px", display: "flex", gap: "1rem" }}>
                <Link to="/login">Вход</Link>
                <Link to="/register">Регистрация</Link>
            </div>
        </div>
    );
}